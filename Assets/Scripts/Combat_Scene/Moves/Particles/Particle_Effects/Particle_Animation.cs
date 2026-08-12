using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Particle_Animation : Particle_Dad
{
    [SerializeField] 
    private Color damageColor;

    private MoveResult moveResult;
    private Animator anim;
    private List<(float time, int potency)> flashTimes;

    private AnimatorOverrideController overrideController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = overrideController;
    }

    public void ParticleInitialize(AnimationClip overrideAnim, List<(float time, int potency)> flashTimes, MoveResult moveResult, float lifeSpan, Particle_Controller_Dad papa)
    {
        this.moveResult = moveResult;
        anim = GetComponent<Animator>();
        this.flashTimes = flashTimes;
        RegisterNextDamage();
        overrideController["Placeholder_Anim"] = overrideAnim;
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
        if (flashTimes.Any(n => n.time <= age))
        {
            father.SendAnimation(new AnimDetails(CombatAnimation.ColorFlash, moveResult.targetType, moveResult.targetNum, null, damageColor));
            GameManager.instance.combat.combatUI.PlayerUI[moveResult.targetNum].HealthScript.ApplyChange(gameObject);

            flashTimes.RemoveAll(n => n.time <= age);
            RegisterNextDamage();
        }
    }

    private void RegisterNextDamage()
    {
        if (flashTimes.Count > 0)
        {
            int nextDamage = -flashTimes.OrderBy(t => t.time).FirstOrDefault().potency;
            GameManager.instance.combat.combatUI.PlayerUI[moveResult.targetNum].HealthScript.RegisterChange(gameObject, nextDamage);
        }
    }
}
