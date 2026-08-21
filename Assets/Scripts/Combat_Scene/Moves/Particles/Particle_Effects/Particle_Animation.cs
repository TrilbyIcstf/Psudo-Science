using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Particle_Animation : Particle_Dad
{
    [SerializeField] 
    private Color damageColor;

    private MoveResult moveResult;
    private Animator anim;
    private Dictionary<float, int> damageTimes;
    private Dictionary<float, AnimDetails> animationTimes;

    private AnimatorOverrideController overrideController;

    private Target type;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = overrideController;
    }

    public void ParticleInitialize(AnimationClip overrideAnim, Dictionary<float, int> flashTimes, Dictionary<float, AnimDetails> animationTimes, MoveResult moveResult, float lifeSpan, Particle_Controller_Dad papa)
    {
        this.moveResult = moveResult;
        anim = GetComponent<Animator>();
        this.damageTimes = flashTimes;
        this.animationTimes = animationTimes;
        overrideController["Placeholder_Anim"] = overrideAnim;
        type = moveResult.targetType;
        base.ParticleInitialize(lifeSpan, papa);
        anim.SetTrigger("Play");
    }

    protected override void ParticleDestroy()
    {
        father.RemoveParticle(gameObject);
        Destroy(gameObject);
    }

    protected override bool ParticleEOL()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName("Particle_Anim") && stateInfo.normalizedTime >= 1.0f && !anim.IsInTransition(0);
    }

    protected override void ParticleUpdate() {
        List<float> passDamageTimes = damageTimes.Keys.Where(n => n <= age).ToList();
        if (passDamageTimes.Count > 0)
        {
            int totalDamage = 0;
            foreach (float time in passDamageTimes)
            {
                totalDamage += damageTimes[time];
                damageTimes.Remove(time);
            }

            if (type == Target.PC)
            {
                GameManager.instance.combat.combatUI.PlayerUI[moveResult.targetNum].HealthScript.RegisterChange(gameObject, -totalDamage);
                GameManager.instance.combat.combatUI.PlayerUI[moveResult.targetNum].HealthScript.ApplyChange(gameObject);
            } else if (type == Target.ENEMY)
            {
                GameManager.instance.combat.GetEnemy(moveResult.targetNum).RegisterDisplayDamage(gameObject, totalDamage);
                GameManager.instance.combat.GetEnemy(moveResult.targetNum).ApplyDisplayDamage(gameObject);
            }
        }

        List<float> passAnimationTimes = animationTimes.Keys.Where(n => n <= age).ToList();
        foreach (float time in passAnimationTimes)
        {
            AnimDetails anim = animationTimes[time];
            anim.targetType = moveResult.targetType;
            anim.target = moveResult.targetNum;
            father.SendAnimation(anim);
            animationTimes.Remove(time);
        }
    }
}
