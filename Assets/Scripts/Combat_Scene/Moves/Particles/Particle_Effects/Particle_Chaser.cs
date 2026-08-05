using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Particle_Chaser : Particle_Dad
{
    public GameObject onDestroyParticleSystem;

    protected override bool ParticleEOL()
    {
        return (GoalCheck() || AgeCheck());
    }

    protected override void ParticleUpdate()
    {
        transform.position += (Vector3)(moveSpeed * moveDirection.normalized);
        moveSpeed *= moveAccel;
        moveDirection = Particle_Math.LerpTowardsPoint(goalPosition, transform.position, moveDirection, turnSpeed);
        turnSpeed = Mathf.Min((turnSpeed + 0.0006f) * 1.012f, 0.999f);
    }

    private bool GoalCheck()
    {
        return Particle_Math.CheckApproach(goalPosition, transform.position, targetDistance, moveSpeed, moveDirection);
    }
}
