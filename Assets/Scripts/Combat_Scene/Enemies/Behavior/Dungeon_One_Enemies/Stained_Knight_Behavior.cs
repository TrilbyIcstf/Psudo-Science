using System.Collections.Generic;
using UnityEngine;

public class Stained_Knight_Behavior : Behavior_Dad
{
    public override (GameObject, List<int>, int) MakeMove()
    {
        switch (varient)
        {
            case 0: return (gameObject, new List<int>() { 1 }, 4);
            case 1: return (gameObject, new List<int>() { 0, 2 }, 8);
            default: throw new System.NotImplementedException();
        }
        throw new System.NotImplementedException();
    }

    protected override int GetBaseSpeed()
    {
        switch (varient)
        {
            case 0: return 5;
            case 1: return 8;
            default: return 0;
        }
    }
}
