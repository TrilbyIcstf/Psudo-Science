using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            var damageList = DamageList(flashTimes.Count, result.potency);
            List<(float time, int damage)> zipList = flashTimes.Zip(damageList, (time, damage) => (time, damage)).ToList();
            tempParticle.GetComponent<Particle_Animation>().ParticleInitialize(anim, zipList, result, 5.0f, this);
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

    private List<int> DamageList(int size, float potency)
    {
        List<int> list = new List<int>();
        float remainder = potency % size;
        int splitPotency = Mathf.FloorToInt(potency / size);
        
        for (int i = 0; i < size; i++)
        {
            list.Add(splitPotency + (i < remainder ? 1 : 0));
        }

        return list;
    }
}
