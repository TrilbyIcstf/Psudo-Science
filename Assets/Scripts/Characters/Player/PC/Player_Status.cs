using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks variables of the player that will often change during combat.
/// </summary>
public class Player_Status
{
    private int currentHealth;

    private HashSet<StatusEffect> statusEffects = new HashSet<StatusEffect>();

    private int healthBuff = 0;
    private int attackBuff = 0;
    private int defenseBuff = 0;
    private int magicBuff = 0;
    private int magDefenseBuff = 0;

    private float healthMult = 1;
    private float attackMult = 1;
    private float defenseMult = 1;
    private float magicMult = 1;
    private float magDefenseMult = 1;

    public Player_Status(int _health)
    {
        currentHealth = _health;
    }

    public Player_Status(Player_Status _oldStatus, int _health, bool cleanse)
    {
        if (_health > 0)
        {
            currentHealth = _health;
        } else
        {
            currentHealth = _oldStatus.CurrentHealth;
        }

        if (!cleanse)
        {
            statusEffects = _oldStatus.StatusEffects;
        }
    }

    /// <summary>
    /// Adds the passed status effect to the character.
    /// </summary>
    /// <param name="se">
    /// The status effect to add.
    /// </param>
    /// <returns>
    /// True if the status effect was not already present, false otherwise.
    /// </returns>
    public bool AddStatusEffect(StatusEffect se)
    {
        return statusEffects.Add(se);
    }

    /// <summary>
    /// Removes the passed status effect from the character.
    /// </summary>
    /// <param name="se">
    /// The status effect to remove.
    /// </param>
    /// <returns>
    /// True if status effect was present, false otherwise.
    /// </returns>
    public bool RemoveStatusEffect(StatusEffect se)
    {
        return statusEffects.Remove(se);
    }

    /// <summary>
    /// Checks if passed in status effect is present.
    /// </summary>
    /// <param name="se">
    /// The status effect to check for.
    /// </param>
    /// <returns>
    /// True if status effect was present, false otherwise.
    /// </returns>
    public bool HasStatusEffect(StatusEffect se)
    {
        return statusEffects.Contains(se);
    }

    // Get/Set
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public HashSet<StatusEffect> StatusEffects { get => statusEffects; set => statusEffects = value; }
    public bool KO { get => currentHealth > 0; }
    public int HealthBuff { get => healthBuff; set => healthBuff = value; }
    public int AttackBuff { get => attackBuff; set => attackBuff = value; }
    public int DefenseBuff { get => defenseBuff; set => defenseBuff = value; }
    public int MagicBuff { get => magicBuff; set => magicBuff = value; }
    public int MagDefenseBuff { get => magDefenseBuff; set => magDefenseBuff = value; }
    public float HealthMult { get => healthMult; set => healthMult = value; }
    public float AttackMult { get => attackMult; set => attackMult = value; }
    public float DefenseMult { get => defenseMult; set => defenseMult = value; }
    public float MagicMult { get => magicMult; set => magicMult = value; }
    public float MagDefenseMult { get => magDefenseMult; set => magDefenseMult = value; }
}
