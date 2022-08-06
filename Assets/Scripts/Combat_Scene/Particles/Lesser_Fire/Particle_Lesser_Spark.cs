using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Lesser_Spark : Particle_Dad
{
    protected override void ParticleDestroy()
    {
        Destroy(gameObject);
    }

    protected override bool ParticleEOL()
    {
        return Particle_Math.CheckApproach(goalPosition, transform.position, 0.5f, ref approachVelocity, ref goalDistance);
    }

    protected override void ParticleUpdate()
    {
        moveDirection = Particle_Math.LerpTowardsPoint(goalPosition, transform.position, moveDirection, turnSpeed);
        turnSpeed = Mathf.Min(turnSpeed + 0.0006f, 0.99f);
        transform.position += (Vector3)(moveSpeed * moveDirection.normalized);
    }
}
