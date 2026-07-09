using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Spray_Particle_Controller : Particle_Controller_Dad
{
    // The particle gameobject
    [SerializeField]
    private GameObject bulletParticle;

    [SerializeField]
    private Color bulletColor;

    // Variables for spawning the particles
    private const int numberToSpawn = 4;
    private const float spawnDelay = 0.1f;
    private const float minAngle = 25;
    private const float maxAngle = 60;
    private const float lifeSpan = 5;
    private Vector2 spawnPosition;
    private Vector2 goalPosition;
    private float potency;

    public override IEnumerator Activate()
    {
        float initialAngle = ((Mathf.Rad2Deg * Mathf.Atan2(goalPosition.y - spawnPosition.y, goalPosition.x - spawnPosition.x)) + 360) % 360;
        Vector2[] directions = MakeSpawnAngleArray(initialAngle);

        int remainder = (int)potency % numberToSpawn;
        int damage = Mathf.FloorToInt(potency / numberToSpawn);

        for (int i = 0; i < numberToSpawn; i++)
        {
            int tempDamage = damage + (i < remainder ? 1 : 0);
            GameObject tempParticle = Instantiate(bulletParticle, spawnPosition, Quaternion.identity);
            tempParticle.GetComponent<Particle_Chaser>().ParticleInitialize(goalPosition, targets[0].targetNum, 0.2f, 1.001f, directions[i], 0.05f, 0.7f, tempDamage, lifeSpan, this);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public override void Cleanup()
    {
        father.RemoveController(this);
        GameManager.instance.fx.RemoveParticleManager(gameObject);
        Destroy(gameObject);
    }

    public void Setup(Vector2 sp, Vector2 gp, Move_Dad papa, List<MoveResult> targets, float potency)
    {
        spawnPosition = sp;
        goalPosition = gp;
        this.potency = potency;
        bulletParticle.GetComponent<SpriteRenderer>().color = bulletColor;
        base.Setup(papa, targets);
    }

    private Vector2[] MakeSpawnAngleArray(float initialAngle)
    {
        Vector2[] angleArray = new Vector2[numberToSpawn];
        int angleDirection = 1;
        float angleAdjust = Vector2.Distance(spawnPosition, goalPosition) <= 7.5f ? 2 : 1;

        for (int i = 0; i < numberToSpawn; i++)
        {
            float newAngle = initialAngle + ((Random.Range(minAngle, maxAngle) / angleAdjust) * angleDirection);
            angleArray[i] = new Vector2(Mathf.Cos(Mathf.Deg2Rad * newAngle), Mathf.Sin(Mathf.Deg2Rad * newAngle));
            angleDirection *= -1;
        }

        return angleArray;
    }

    public override bool ControllerActive()
    {
        return particleList.Count > 0;
    }
}
