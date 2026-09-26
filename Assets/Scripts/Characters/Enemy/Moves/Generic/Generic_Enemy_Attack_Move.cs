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
        List<Element> weakness = pi.Weakness;
        List<Element> strength = pi.Strength;
        Effectiveness effectiveness = GetElement().Evaluate(weakness, strength);

        float result = 0;
        if (GetMoveType() == MoveType.PHYSICAL)
        {
            result = BasicDamageCalc(adjustedPotency, ei.Power, pi.Defense, effectiveness);
        }
        else if (GetMoveType() == MoveType.MAGICAL)
        {
            result = BasicDamageCalc(adjustedPotency, ei.Intelligence, pi.Resistance, effectiveness);
        }

        return new MoveResult(result, Target.PC, target, effectiveness);
    }

    public override bool ApplyMove(Enemy_Stats ei, List<MoveResult> results)
    {
        foreach (MoveResult result in results)
        {
            int target = result.TargetNum;
            float damage = result.Potency;
            GameManager.instance.combat.ProcessEnemyAttackDamage(target, (int)damage);
            Combat_UI_Commands.RefreshHealthBars();
        }
        return true;
    }
}
