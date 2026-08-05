using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board_Effect_Particle_Controller : Particle_Controller_Dad
{
    private const float minAngle = 25;
    private const float maxAngle = 60;
    private const float lifeSpan = 5;

    // The particle gameobject
    [SerializeField]
    private GameObject bulletParticle;

    private List<(Vector2Int pos, TColor color)> changes;

    public override IEnumerator Activate()
    {
        foreach ((Vector2Int pos, TColor color) change in changes)
        {
            Vector2 tilePos = GameManager.instance.combat.board.GetTile(change.pos).transform.position;
            Vector2 currentPos = transform.position;

            float initialAngle = ((Mathf.Rad2Deg * Mathf.Atan2(tilePos.y - currentPos.y, tilePos.x - currentPos.x)) + 360) % 360;
            Vector2 spawnAngle = DecideAngle(initialAngle);

            GameObject tempParticle = Instantiate(bulletParticle, currentPos, Quaternion.identity);
            tempParticle.GetComponent<Particle_Change_Tile_Chaser>().ParticleInitialize(tilePos, change.pos, 0.1f, 1.002f, spawnAngle, 0.09f, 0.4f, change.color, lifeSpan, this);
        }

        yield return new WaitForSeconds(0.0f);
    }

    public override void Cleanup()
    {
        father.RemoveController(this);
        GameManager.instance.fx.RemoveParticleManager(gameObject);
        Destroy(gameObject);
    }

    public override bool ControllerActive()
    {
        return particleList.Count > 0;
    }

    private Vector2 DecideAngle(float initialAngle)
    {
        int angleDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        float newAngle = initialAngle + (Random.Range(minAngle, maxAngle) * angleDirection);
        return new Vector2(Mathf.Cos(Mathf.Deg2Rad * newAngle), Mathf.Sin(Mathf.Deg2Rad * newAngle));
    }

    public void Setup(List<(Vector2Int pos, TColor color)> changes, Move_Dad papa, List<MoveResult> targets)
    {
        this.changes = changes;
        base.Setup(papa, targets);
    }
}
