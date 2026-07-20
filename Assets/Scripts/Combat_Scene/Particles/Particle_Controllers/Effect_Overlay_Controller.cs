using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Overlay_Controller : Particle_Controller_Dad
{
    [SerializeField]
    private GameObject overlayObject;
    [SerializeField]
    private AnimationClip anim;
    [SerializeField]
    private List<float> flashTimes;

    private Vector2 pos;

    public override IEnumerator Activate()
    {
        foreach (MoveResult result in targets)
        {
            GameObject tempParticle = Instantiate(overlayObject, pos, Quaternion.identity);
            tempParticle.GetComponent<Particle_Animation>().ParticleInitialize(anim, flashTimes, result, 5.0f, this);
        }

        yield return new WaitForSeconds(0.0f);
    }

    public override void Cleanup()
    {
        father.RemoveController(this);
        GameManager.instance.fx.RemoveParticleManager(gameObject);
        Destroy(gameObject);
    }

    public override bool ControllerActive()
    {
        return particleList.Count > 0;
    }

    public void Setup(Vector2 pos, Move_Dad papa, List<MoveResult> targets)
    {
        this.pos = pos;
        base.Setup(papa, targets);
    }
}
