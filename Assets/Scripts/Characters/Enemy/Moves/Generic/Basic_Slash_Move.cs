using System.Collections.Generic;
using UnityEngine;

public class Basic_Slash_Move : Generic_Enemy_Attack_Move
{
    private bool attackStarted = false;

    // Particles/Animations
    public override void StartMove(int user, List<MoveResult> results)
    {
        GameObject tempParticleController = Instantiate(mainParticleController);
        tempParticleController.GetComponent<Effect_Overlay_Controller>().Setup(this, results);
        GameManager.instance.fx.AddParticleManager(tempParticleController);

        attackStarted = true;
    }

    public override void EndMove(int user) { }

    public override bool IsMoveFinished()
    {
        return attackStarted && particleControllerList.Count <= 0;
    }
}
