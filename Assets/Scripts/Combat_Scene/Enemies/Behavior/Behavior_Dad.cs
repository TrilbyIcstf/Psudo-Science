using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behavior_Dad : MonoBehaviour
{
    [SerializeField]
    protected int varient = 0;

    protected int step = 0;

    public int BaseSpeed { get => GetBaseSpeed(); }

    public abstract (GameObject, List<int>, int) MakeMove();

    protected abstract int GetBaseSpeed();

    public void SetVarient(int varient)
    {
        this.varient = varient;
    }
}
