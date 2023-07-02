using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesser_Fire_Move : Move_Dad
{
    private bool attackStarted = false;
    private GameObject target;
    private BodyPart targetPart = BodyPart.BODY;

    public void Setup(GameObject tar)
    {
        target = tar;
    }

    public override void StartAttack(PC user)
    {
        mainParticleController = Instantiate(mainParticleController);
        mainParticleController.GetComponent<Lesser_Fire_Particle_Controller>().Setup(Combat_UI_Commands.GetPlayerPosition(user).position, (Vector2)Combat_Commands.GetTargetedBodyPart(targetPart), this);
        GameManager.instance.fx.AddParticleManager(mainParticleController);
        attackStarted = true;
    }

    public override bool MoveFinished()
    {
        return attackStarted && particleControllerList.Count <= 0;
    }
}
