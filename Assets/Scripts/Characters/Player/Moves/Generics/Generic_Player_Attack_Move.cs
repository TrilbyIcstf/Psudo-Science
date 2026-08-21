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
        float result = 0;

        if (mi.Type == MoveType.PHYSICAL)
        {
            result = (pi.Power * 2) - (targetStats.Defense * 0.5f);
        } else if (mi.Type == MoveType.MAGICAL)
        {
            result = (pi.Intelligence * 2) - (targetStats.Resistance * 0.5f);
        }
        result = result * mi.AdjustedPotency;

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
