using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Lesser_Spark : Particle_Dad
{
    private int target;

    public void ParticleInitialize(Vector2 goal, int target, float startSpeed, float startAccel, Vector2 startDirection, float startTurnSpeed, float targetDist, Particle_Controller_Dad papa)
    {
        lifeSpan = 5;
        this.target = target;
        base.ParticleInitialize(goal, startSpeed, startAccel, startDirection, startTurnSpeed, targetDist, papa);
    }

    protected override void ParticleDestroy()
    {
        //float angle = (Mathf.Atan2(goalPosition.y - transform.position.y, goalPosition.x - transform.position.x) * Mathf.Rad2Deg) - 90;
        float angle = (Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg) - 90;
        if (angle < 0)
        {
            angle += 360;
        }
        Debug.Log(angle);
        father.SendAnimation(EnemyAnimation.SmallRecoil, target, angle);

        father.RemoveParticle(gameObject);
        Destroy(gameObject);
    }

    protected override bool ParticleEOL()
    {
        return (GoalCheck() || AgeCheck());
    }

    protected override void ParticleUpdate()
    {
        age += Time.deltaTime;
        transform.position += (Vector3)(moveSpeed * moveDirection.normalized);
        moveSpeed *= moveAccel;
        moveDirection = Particle_Math.LerpTowardsPoint(goalPosition, transform.position, moveDirection, turnSpeed);
        turnSpeed = Mathf.Min((turnSpeed + 0.0006f) * 1.012f, 0.999f);
    }

    private bool GoalCheck()
    {
        return Particle_Math.CheckApproach(goalPosition, transform.position, targetDistance, moveSpeed, moveDirection);
    }

    private bool AgeCheck()
    {
        return age > lifeSpan;
    }
}
