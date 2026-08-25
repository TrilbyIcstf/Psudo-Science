using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Move : Move_Dad
{
    [SerializeField]
    protected float potency;

    // Section for handling move effects
    public abstract List<MoveResult> ResultsCalc(Enemy_Stats ei, List<int> targets, float potency);
    public abstract MoveResult TargetCalc(Enemy_Stats ei, int target, float potency);
    public abstract bool ApplyMove(Enemy_Stats ei, List<MoveResult> results);

    protected int BasicDamageCalc(float potency, int offense, int defense, Effectiveness effectiveness)
    {
        return base.BasicDamageCalc(potency, offense, defense, effectiveness, Target.ENEMY);
    }

    protected int DamageCalc(float potency, int offense, float offenseRatio, int defense, float defenseRatio, Effectiveness effectiveness)
    {
        return base.DamageCalc(potency, offense, offenseRatio, defense, defenseRatio, effectiveness, Target.ENEMY);
    }
}
