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
}
