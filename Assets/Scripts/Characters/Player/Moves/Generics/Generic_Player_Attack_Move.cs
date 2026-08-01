using System.Collections.Generic;
using UnityEngine;

public abstract class Generic_Player_Attack_Move : Player_Move
{
    // Move Effects
    public override List<MoveResult> ResultsCalc(Player_Information pi, int target, Move_Information mi)
    {
        List<MoveResult> results = new List<MoveResult>();
        results.Add(PotencyCalc(pi, target, mi));
        return results;
    }

    public override MoveResult PotencyCalc(Player_Information pi, int target, Move_Information mi)
    {
        float potency = mi.Potency * Combat_Commands.GetBoost();
        return new MoveResult(potency, Target.ENEMY, target);
    }

    public override bool ApplyMove(Player_Information pi, List<MoveResult> results, Move_Information mi)
    {
        MoveResult result = results[0];
        GameManager.instance.combat.ProcessPlayerAttackDamage(result.targetNum, (int)result.potency);
        return true;
    }
}
