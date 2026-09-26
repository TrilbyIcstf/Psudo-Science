using UnityEngine;

public class Particle_Particle_System : Particle_Dad
{
    private MoveResult results;
    private Target type;

    private ParticleSystem ps;
    private bool started = false;

    public void ParticleInitialize(float lifeSpan, Particle_Controller_Dad papa, MoveResult results, Target type)
    {
        this.results = results;
        this.type = type;
        ps = GetComponent<ParticleSystem>();
        base.ParticleInitialize(lifeSpan, papa);
    }

    protected override void ParticleDestroy()
    {
        father.RemoveParticle(gameObject);
        Destroy(gameObject);
    }

    protected override bool ParticleEOL()
    {
        return (started && !ps.isPlaying) || age >= lifeSpan;
    }

    protected override void ParticleUpdate() {
        if (ps != null)
        {
            if (!started && ps.isPlaying)
            {
                started = true;

                BarChangeDetails barDetails = new BarChangeDetails((int)results.Potency, results.Effectiveness, false);
                ApplyVisualDamage(results.TargetNum, type, barDetails);
                father.SendAnimation(new AnimDetails(CombatAnimation.ColorFlash, type, results.TargetNum, null, Color.red));
            }
        }
    }
}
