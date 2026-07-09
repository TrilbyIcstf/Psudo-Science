using System.Collections.Generic;
using UnityEngine;

public class Lesser_Heal_Move : Move_Dad
{
    private bool moveStarted = false;

    // Move Effects
    public override bool ApplyMove(Player_Information pi, List<MoveResult> results, Move_Information mi)
    {
        MoveResult result = results[0];
        GameManager.instance.party.SingleHeal(result.targetNum, (int)result.potency);
        Combat_UI_Commands.RefreshHealthBars();
        return true;
    }

    public override List<MoveResult> ResultsCalc(Player_Information pi, int target, Move_Information mi)
    {
        List<MoveResult> results = new List<MoveResult>();
        results.Add(PotencyCalc(pi, GameManager.instance.party.MostDamaged(), mi));
        return results;
    }

    public override MoveResult PotencyCalc(Player_Information pi, int target, Move_Information mi)
    {
        return new MoveResult(mi.Potency, Target.PC, target);
    }

    // Particles/Animations
    public override void StartMove(PC user, List<MoveResult> results)
    {
        GameObject tempParticleController = Instantiate(mainParticleController);
        Vector2 targetPos = Combat_UI_Commands.GetPlayerPosition(results[0].targetNum).position;

        tempParticleController.GetComponent<Lesser_Heal_Particle_Controller>().Setup(targetPos, this, results);
        GameManager.instance.fx.AddParticleManager(tempParticleController);
        moveStarted = true;
    }

    public override void EndMove(PC user) { }

    public override bool IsMoveFinished()
    {
        return moveStarted && particleControllerList.Count <= 0;
    }
}
