using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class to be overriden by various equipment ability scripts.
/// </summary>
public class Equip_Ability
{
    public virtual bool Activate(CombatTiming CT)
    {
        return false;
    }
}
