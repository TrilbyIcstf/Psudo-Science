using UnityEngine;

public class Particle_Animation : Particle_Dad
{
    [SerializeField] 
    private Color damageColor;

    private MoveResult moveResult;
    private Animator anim;
    private string animName;

    [SerializeField]
    private AnimationClip testAnim;
    private AnimatorOverrideController overrideController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        overrideController = new AnimatorOverrideController(anim.runtimeAnimatorController);
        anim.runtimeAnimatorController = overrideController;
        overrideController["Placeholder_Anim"] = testAnim;
        anim.SetTrigger("Play");
    }

    public void ParticleInitialize(MoveResult moveResult, float lifeSpan, Particle_Controller_Dad papa)
    {
        this.moveResult = moveResult;
        anim = GetComponent<Animator>();
        base.ParticleInitialize(lifeSpan, papa);
    }

    protected override void ParticleDestroy()
    {
        father.RemoveParticle(gameObject);
        Destroy(gameObject);
    }

    protected override bool ParticleEOL()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        return false;
    }

    protected override void ParticleUpdate() { }
}
