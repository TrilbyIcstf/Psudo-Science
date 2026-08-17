using System.Collections.Generic;
using UnityEngine;

public abstract class Generic_Player_Attack_Move : Player_Move
{
    // Move Effects
    public override List<MoveResult> ResultsCalc(Player_Information pi, int target, Move_Information mi)
    {
        List<MoveResult> results = new List<MoveResult>();
        results.Add(TargetCalc(pi, target, mi));
        return results;
    }

    public override MoveResult TargetCalc(Player_Information pi, int target, Move_Information mi)
    {
        Enemy_Stats targetStats = GameManager.instance.combat.GetEnemy(target).GetStats();
        float result = mi.AdjustedPotency * (pi.Intelligence * 2);
        result = result - (targetStats.MagDefense / 2);
        result = result * Combat_Commands.GetBoost();
        result = Mathf.Max(1, result);

        return new MoveResult(result, Target.ENEMY, target);
    }

    public override bool ApplyMove(Player_Information pi, List<MoveResult> results, Move_Information mi)
    {
        MoveResult result = results[0];
        GameManager.instance.combat.ProcessPlayerAttackDamage(result.targetNum, (int)result.potency);
        return true;
    }
}
