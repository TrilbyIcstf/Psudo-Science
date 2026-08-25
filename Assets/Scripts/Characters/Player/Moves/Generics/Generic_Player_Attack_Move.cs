using System.Collections.Generic;
using UnityEngine;

public abstract class Generic_Player_Attack_Move : Player_Move
{
    // Move Effects
    public override List<MoveResult> ResultsCalc(Player_Information pi, int target, Move_Information mi)
    {
        return new List<MoveResult>() { TargetCalc(pi, target, mi) };
    }

    public override MoveResult TargetCalc(Player_Information pi, int target, Move_Information mi)
    {
        Enemy_Stats targetStats = GameManager.instance.combat.GetEnemy(target).GetStats();
        List<Element> weakness = targetStats.Weakness;
        List<Element> strength = targetStats.Strength;
        Effectiveness effectiveness = mi.Element.Evaluate(weakness, strength);
        
        float result = 0;

        if (mi.Type == MoveType.PHYSICAL)
        {
            result = BasicDamageCalc(mi.AdjustedPotency, pi.Power, targetStats.Defense, effectiveness);
        } else if (mi.Type == MoveType.MAGICAL)
        {
            result = BasicDamageCalc(mi.AdjustedPotency, pi.Intelligence, targetStats.Resistance, effectiveness);
        }

        return new MoveResult(result, Target.ENEMY, target, effectiveness);
    }

    public override bool ApplyMove(Player_Information pi, List<MoveResult> results, Move_Information mi)
    {
        foreach (MoveResult result in results)
        {
            GameManager.instance.combat.ProcessPlayerAttackDamage(result.TargetNum, (int)result.Potency);
        }
        return true;
    }
}
