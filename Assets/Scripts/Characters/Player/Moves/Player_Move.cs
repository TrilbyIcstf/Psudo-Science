using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Move : Move_Dad
{
    [SerializeField]
    protected Move_Information moveInfo;
    public Move_Information MoveInfo { get => moveInfo; }

    // Section for handling move effects
    public abstract List<MoveResult> ResultsCalc(Player_Information pi, int target, Move_Information mi);
    public abstract MoveResult TargetCalc(Player_Information pi, int target, Move_Information mi);
    public abstract bool ApplyMove(Player_Information pi, List<MoveResult> results, Move_Information mi);

    protected int BasicDamageCalc(float potency, int offense, int defense, Effectiveness effectiveness)
    {
        return base.BasicDamageCalc(potency, offense, defense, effectiveness, Target.PC);
    }

    protected int DamageCalc(float potency, int offense, float offenseRatio, int defense, float defenseRatio, Effectiveness effectiveness)
    {
        return base.DamageCalc(potency, offense, offenseRatio, defense, defenseRatio, effectiveness, Target.PC);
    }
}
