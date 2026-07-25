using System.Collections.Generic;
using UnityEngine;

public abstract class Generic_Enemy_Attack_Move : Enemy_Move
{
    // Move Effects
    public override List<MoveResult> ResultsCalc(Enemy_Information ei, List<int> targets)
    {
        List<MoveResult> results = new List<MoveResult>();
        foreach (int target in targets)
        {
            results.Add(PotencyCalc(ei, target));
        }
        return results;
    }

    public override MoveResult PotencyCalc(Enemy_Information ei, int target)
    {
        return new MoveResult(potency, Target.PC, target);
    }

    public override bool ApplyMove(Enemy_Information ei, List<MoveResult> results)
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
