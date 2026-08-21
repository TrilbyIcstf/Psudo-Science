using System.Collections.Generic;
using UnityEngine;

public abstract class Generic_Enemy_Attack_Move : Enemy_Move
{
    // Move Effects
    public override List<MoveResult> ResultsCalc(Enemy_Stats ei, List<int> targets, float potency)
    {
        List<MoveResult> results = new List<MoveResult>();
        foreach (int target in targets)
        {
            results.Add(TargetCalc(ei, target, potency));
        }
        return results;
    }

    public override MoveResult TargetCalc(Enemy_Stats ei, int target, float potency)
    {
        float adjustedPotency = potency / 100;
        Player_Information pi = GameManager.instance.party.GetPlayer(target);
        float result = (ei.Power * 2) - (pi.Defense * 0.5f);
        result = result * adjustedPotency;
        result = Mathf.Max(1, result);

        return new MoveResult(result, Target.PC, target);
    }

    public override bool ApplyMove(Enemy_Stats ei, List<MoveResult> results)
    {
        foreach (MoveResult result in results)
        {
            int target = result.targetNum;
            float damage = result.potency;
            GameManager.instance.combat.ProcessEnemyAttackDamage(target, (int)damage);
            Combat_UI_Commands.RefreshHealthBars();
        }
        return true;
    }
}
