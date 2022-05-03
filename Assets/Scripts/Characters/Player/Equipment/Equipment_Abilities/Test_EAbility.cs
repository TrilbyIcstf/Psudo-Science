using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_EAbility : Equip_Ability
{
    public override bool Activate(CombatTiming CT)
    {
        if (CT == CombatTiming.COMBATSTART)
        {
            Debug.Log("Test ability fired!");
            return true;
        }
        return false;
    }
}
