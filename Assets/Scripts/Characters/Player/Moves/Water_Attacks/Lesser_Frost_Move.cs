using System.Collections.Generic;
using UnityEngine;

public class Lesser_Frost_Move : Generic_Attack_Move
{
    private bool attackStarted = false;
    private BodyPart targetPart = BodyPart.BODY;

    // Particles/Animations
    public override void StartMove(PC user, List<MoveResult> results)
    {
        GameObject tempParticleController = Instantiate(mainParticleController);
        tempParticleController.GetComponent<Bullet_Spray_Particle_Controller>().Setup(Combat_UI_Commands.GetPlayerPosition(user).position, (Vector2)Combat_Commands.GetTargetedBodyPart(targetPart), this, results, results[0].potency);
        GameManager.instance.fx.AddParticleManager(tempParticleController);
        attackStarted = true;
    }

    public override void EndMove(PC user) { }

    public override bool IsMoveFinished()
    {
        return attackStarted && particleControllerList.Count <= 0;
    }
}
