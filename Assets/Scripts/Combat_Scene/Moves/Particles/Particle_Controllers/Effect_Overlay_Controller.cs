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

    public override IEnumerator Activate()
    {
        foreach (MoveResult result in targets)
        {
            int target = result.targetNum;
            Vector2 pos = Combat_UI_Commands.GetPlayerPosition(target).position;
            GameObject tempParticle = Instantiate(overlayObject, pos, Quaternion.identity);
            tempParticle.GetComponent<Particle_Animation>().ParticleInitialize(anim, new List<float>(flashTimes), result, 5.0f, this);
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
}
