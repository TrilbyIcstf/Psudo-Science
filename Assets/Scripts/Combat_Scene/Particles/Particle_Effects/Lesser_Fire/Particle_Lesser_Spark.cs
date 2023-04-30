using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Lesser_Spark : Particle_Dad
{
    public override void ParticleInitialize(Vector2 goal, float startSpeed, float startAccel, Vector2 startDirection, float startTurnSpeed, float targetDist, Particle_Controller_Dad papa)
    {
        lifeSpan = 5;
        base.ParticleInitialize(goal, startSpeed, startAccel, startDirection, startTurnSpeed, targetDist, papa);
    }

    protected override void ParticleDestroy()
    {
        father.RemoveParticle(gameObject);
        Destroy(gameObject);
    }

    protected override bool ParticleEOL()
    {
        bool goalCheck = Particle_Math.CheckApproach(goalPosition, transform.position, targetDistance, moveSpeed, moveDirection);
        bool ageCheck = age > lifeSpan;
        return (goalCheck || ageCheck);
    }

    protected override void ParticleUpdate()
    {
        age += Time.deltaTime;
        transform.position += (Vector3)(moveSpeed * moveDirection.normalized);
        moveSpeed *= moveAccel;
        moveDirection = Particle_Math.LerpTowardsPoint(goalPosition, transform.position, moveDirection, turnSpeed);
        turnSpeed = Mathf.Min((turnSpeed + 0.0006f) * 1.012f, 0.999f);
    }
}
