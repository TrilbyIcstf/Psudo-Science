using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesser_Fire_Move : Move_Dad
{
    private bool attackStarted = false;
    private BodyPart targetPart = BodyPart.BODY;


    // Move Effects
    public override List<MoveResult> ResultsCalc(Player_Information pi, int target, Move_Information mi)
    {
        List<MoveResult> results = new List<MoveResult>();
        results.Add(PotencyCalc(pi, target, mi));
        return results;
    }

    public override MoveResult PotencyCalc(Player_Information pi, int target, Move_Information mi)
    {
        return new MoveResult(mi.Potency, Target.ENEMY, target);
    }

    public override bool ApplyMove(Player_Information pi, List<MoveResult> results, Move_Information mi)
    {
        MoveResult result = results[0];
        GameManager.instance.combat.ProcessPlayerAttackDamage(result.targetNum, (int)result.potency);
        return true;
    }

    // Particles/Animations
    public override void StartMove(PC user, List<MoveResult> results)
    {
        GameObject tempParticleController = Instantiate(mainParticleController);
        tempParticleController.GetComponent<Lesser_Fire_Particle_Controller>().Setup(Combat_UI_Commands.GetPlayerPosition(user).position, (Vector2)Combat_Commands.GetTargetedBodyPart(targetPart), this, results, results[0].potency);
        GameManager.instance.fx.AddParticleManager(tempParticleController);
        attackStarted = true;
    }

    public override void EndMove(PC user) { }

    public override bool IsMoveFinished()
    {
        return attackStarted && particleControllerList.Count <= 0;
    }
}
