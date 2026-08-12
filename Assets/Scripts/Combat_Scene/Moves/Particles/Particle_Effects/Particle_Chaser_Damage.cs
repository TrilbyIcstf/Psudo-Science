using UnityEngine;

public class Particle_Chaser_Damage : Particle_Chaser
{
    private int damage;
    private int target;
    [SerializeField] private Color damageColor;

    public void ParticleInitialize(Vector2 goal, int target, float startSpeed, float startAccel, Vector2 startDirection, float startTurnSpeed, float targetDist, int damage, float lifeSpan, Particle_Controller_Dad papa)
    {
        papa.RegisterTempDamage(gameObject, damage, target);

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
        father.SendAnimation(new AnimDetails(CombatAnimation.ColorFlash, Target.ENEMY, target, null, damageColor));
        //father.SendTempDamage(damage, target);
        father.ApplyTempDamage(gameObject, target);
        if (onDestroyParticleSystem != null)
        {
            GameObject particleSystem = Instantiate(onDestroyParticleSystem, transform.position, Quaternion.identity);
        }

        father.RemoveParticle(gameObject);
        Destroy(gameObject);
    }
}
