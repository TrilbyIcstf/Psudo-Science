using System.Collections.Generic;
using UnityEngine;

public class Character_Status
{
    protected bool isAlive = true;
    public bool IsAlive { get => isAlive; set => isAlive = value; }

    protected Dictionary<StatusEffect, int> statusEffects = new Dictionary<StatusEffect, int>();

    public int GetAdjustedPower(int initial)
    {
        float flatBoost = 0;
        float mult = 1.0f;

        foreach (var se in statusEffects)
        {
            switch (se.Key)
            {
                case StatusEffect.MINORPOWERUP:
                    mult += 0.25f;
                    break;
                case StatusEffect.MIDPOWERUP:
                    mult += 0.5f;
                    break;
                case StatusEffect.MAJORPOWERUP:
                    mult += 0.75f;
                    break;
            }
        }

        return Mathf.CeilToInt((initial * mult) + flatBoost);
    }

    public int GetAdjustedInt(int initial)
    {
        float flatBoost = 0;
        float mult = 1.0f;

        foreach (var se in statusEffects)
        {
            switch (se.Key)
            {
                case StatusEffect.MINORPOWERUP:
                    mult += 0.25f;
                    break;
                case StatusEffect.MIDPOWERUP:
                    mult += 0.5f;
                    break;
                case StatusEffect.MAJORPOWERUP:
                    mult += 0.75f;
                    break;
            }
        }

        return Mathf.CeilToInt((initial * mult) + flatBoost);
    }

    /// <summary>
    /// Adds the passed status effect to the character.
    /// </summary>
    public bool AddStatusEffect(StatusEffect se, int duration)
    {
        if (statusEffects.ContainsKey(se))
        {
            statusEffects[se] = duration;
            return true;
        }
        else
        {
            statusEffects.Add(se, duration);
            return false;
        }
    }

    public void CountDownStatus()
    {
        List<StatusEffect> tempList = new List<StatusEffect>(statusEffects.Keys);
        foreach (StatusEffect se in tempList)
        {
            statusEffects[se] -= 1;
            if (statusEffects[se] <= 0)
            {
                RemoveStatusEffect(se);
            }
        }
    }

    /// <summary>
    /// Removes the passed status effect from the character.
    /// </summary>
    public bool RemoveStatusEffect(StatusEffect se)
    {
        return statusEffects.Remove(se);
    }

    /// <summary>
    /// Checks if passed in status effect is present.
    /// </summary>
    public bool HasStatusEffect(StatusEffect se)
    {
        return statusEffects.ContainsKey(se);
    }

    public int StatusDuration(StatusEffect se)
    {
        if (HasStatusEffect(se))
        {
            return statusEffects[se];
        }
        return 0;
    }
}
