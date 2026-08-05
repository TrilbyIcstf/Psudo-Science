using UnityEngine;

public class Particle_Float : Particle_Dad
{
    public void ParticleInitialize(Vector2 goal, float startSpeed, float startAccel, float lifeSpan, Particle_Controller_Dad papa)
    {
        moveAccel = startAccel;
        moveDirection = goal - (Vector2)transform.position;
        base.ParticleInitialize(goal, startSpeed, lifeSpan, papa);
    }

    protected override void ParticleDestroy()
    {
        father.RemoveParticle(gameObject);
        Destroy(gameObject);
    }

    protected override bool ParticleEOL()
    {
        return AgeCheck();
    }

    protected override void ParticleUpdate()
    {
        transform.position += (Vector3)(moveSpeed * moveDirection.normalized);
        moveSpeed *= moveAccel;
    }
}
