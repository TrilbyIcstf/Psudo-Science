using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Move : Move_Dad
{
    [SerializeField]
    protected float potency;

    // Section for handling move effects
    public abstract List<MoveResult> ResultsCalc(Enemy_Information ei, List<int> targets);
    public abstract MoveResult PotencyCalc(Enemy_Information ei, int target);
    public abstract bool ApplyMove(Enemy_Information ei, List<MoveResult> results);
}
