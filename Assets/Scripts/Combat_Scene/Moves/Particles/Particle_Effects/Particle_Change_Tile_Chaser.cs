using UnityEngine;

public class Particle_Change_Tile_Chaser : Particle_Chaser
{
    private Vector2Int target;
    private TColor color;

    public void ParticleInitialize(Vector2 goal, Vector2Int target, float startSpeed, float startAccel, Vector2 startDirection, float startTurnSpeed, float targetDist, TColor color, float lifeSpan, Particle_Controller_Dad papa)
    {
        this.target = target;
        this.color = color;
        base.ParticleInitialize(goal, startSpeed, startAccel, startDirection, startTurnSpeed, targetDist, lifeSpan, papa);
    }

    protected override void ParticleDestroy()
    {
        GameManager.instance.combat.board.GetTile(target).GetComponent<Tile_Interact>().SetVisualColor(color);

        if (onDestroyParticleSystem != null)
        {
            GameObject particleSystem = Instantiate(onDestroyParticleSystem, transform.position, Quaternion.identity);
        }

        father.RemoveParticle(gameObject);
        Destroy(gameObject);
    }
}
