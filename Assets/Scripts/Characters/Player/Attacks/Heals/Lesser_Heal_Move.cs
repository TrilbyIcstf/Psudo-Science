using System.Collections.Generic;
using UnityEngine;

public class Lesser_Heal_Move : Move_Dad
{
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
        
    }

    public override void EndMove(PC user) { }

    public override bool IsMoveFinished()
    {
        return true;
    }
}
