using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behavior_Dad : MonoBehaviour
{
    [SerializeField]
    protected int varient = 0;

    protected int step = 0;

    public int BaseSpeed { get => GetBaseSpeed(); }

    /// <summary>
    /// Creates a move for the enemy to use upon its cooldown reaching 0
    /// </summary>
    /// <returns>
    /// GameObject: The move prefab
    /// TargetingType: The logic used for targeting
    /// int: The number of targets
    /// int: The new cooldown to set the enemy to
    /// float: The potency of the move
    /// </returns>
    public abstract (GameObject, TargetingType, int, int, float) MakeMove();

    protected abstract int GetBaseSpeed();

    public virtual List<int> CustomTargeting()
    {
        return new List<int>();
    }

    public void SetVarient(int varient)
    {
        this.varient = varient;
    }
}
