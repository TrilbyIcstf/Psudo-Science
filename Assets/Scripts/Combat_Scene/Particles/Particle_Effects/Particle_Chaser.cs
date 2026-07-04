using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Chaser : Particle_Dad
{
    private int target;
    public GameObject onDestroyParticleSystem;

    private int damage;

    [SerializeField] private Color damageColor;

    public void ParticleInitialize(Vector2 goal, int target, float startSpeed, float startAccel, Vector2 startDirection, float startTurnSpeed, float targetDist, int damage, float lifeSpan, Particle_Controller_Dad papa)
    {
        this.target = target;
        this.damage = damage;
        base.ParticleInitialize(goal, startSpeed, startAccel, startDirection, startTurnSpeed, targetDist, lifeSpan, papa);
    }

    protected override void ParticleDestroy()
    {
        float angle = (Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg) - 90;
        if (angle < 0)
        {
            angle += 360;
        }
        father.SendAnimation(AnimDetails.Anim(EnemyAnimation.ColorFlash, target, damageColor));
        father.SendTempDamage(damage, target);
        GameObject particleSystem = Instantiate(onDestroyParticleSystem, transform.position, Quaternion.identity);

        father.RemoveParticle(gameObject);
        Destroy(gameObject);
    }

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
