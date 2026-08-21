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
    
    private List<float> damageTimes;
    private Dictionary<float, AnimDetails> animationTimes;

    public override IEnumerator Activate()
    {
        foreach (MoveResult result in targets)
        {
            int target = result.targetNum;
            Target type = result.targetType;
            Vector2 pos;
            if (type == Target.ENEMY)
            {
                pos = Combat_UI_Commands.GetEnemyPosition(target);
            } else
            {
                pos = Combat_UI_Commands.GetPlayerPosition(target).position;
            }
            GameObject tempParticle = Instantiate(overlayObject, pos, Quaternion.identity);
            var damageList = DamageList(damageTimes.Count, result.potency);
            Dictionary<float, int> zipDictionary = damageTimes.Zip(damageList, (time, damage) => new { time, damage }).ToDictionary(x => x.time, x => x.damage);
            tempParticle.GetComponent<Particle_Animation>().ParticleInitialize(anim, zipDictionary, animationTimes, result, 5.0f, this);
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

    public void Setup(Move_Dad papa, List<MoveResult> targets, List<float> damageTimes, Dictionary<float, AnimDetails> animationTimes)
    {
        this.damageTimes = damageTimes;
        this.animationTimes = animationTimes;
        base.Setup(papa, targets);
    }
}
