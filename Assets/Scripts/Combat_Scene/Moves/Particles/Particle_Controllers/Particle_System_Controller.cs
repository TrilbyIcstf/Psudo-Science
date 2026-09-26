using System.Collections;
using UnityEngine;

public class Particle_System_Controller : Particle_Controller_Dad
{
    [SerializeField]
    private GameObject particleObject;

    [SerializeField]
    private Color particleColor;

    public override IEnumerator Activate()
    {
        foreach (MoveResult result in targets)
        {
            int target = result.TargetNum;
            Target type = result.TargetType;
            Vector2 pos;
            if (type == Target.ENEMY)
            {
                pos = Combat_UI_Commands.GetEnemyPosition(target);
            }
            else
            {
                pos = Combat_UI_Commands.GetPlayerPosition(target).position;
            }
            GameObject tempParticle = Instantiate(particleObject, pos, Quaternion.identity);
            var pMain = tempParticle.GetComponent<ParticleSystem>().main;
            pMain.startColor = particleColor;
            tempParticle.GetComponent<Particle_Particle_System>().ParticleInitialize(5.0f, this, result, Target.PC);
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
