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
    /// List<int>: A list of targets
    /// int: The new cooldown to set the enemy to
    /// </returns>
    public abstract (GameObject, List<int>, int) MakeMove();

    protected abstract int GetBaseSpeed();

    public void SetVarient(int varient)
    {
        this.varient = varient;
    }
}
