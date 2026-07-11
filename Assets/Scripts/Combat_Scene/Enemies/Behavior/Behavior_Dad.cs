using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Dad : MonoBehaviour
{
    [SerializeField]
    protected int varient = 0;

    public int BaseSpeed { get => GetBaseSpeed(); }

    public virtual void MakeAttack()
    {
        Debug.Log("Behavior Dad Attack");
    }

    private int GetBaseSpeed()
    {
        switch (varient)
        {
            case 0: return 1;
            case 1: return 2;
            default: return 0;
        }
    }

    public void SetVarient(int varient)
    {
        this.varient = varient;
    }
}
