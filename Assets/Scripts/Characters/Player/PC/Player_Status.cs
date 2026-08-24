using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks variables of the player that will often change during combat.
/// </summary>
public class Player_Status : Character_Status
{
    public const int REVIVECAP = 100;

    private int currentHealth;
    private bool isDead = false;

    private float reviveProgress = 0;

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

    // Get/Set
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public float ReviveProgress { get => reviveProgress; set => reviveProgress = value; }
    public Dictionary<StatusEffect, int> StatusEffects { get => statusEffects; set => statusEffects = value; }
    public bool KO { get => currentHealth > 0; }
}
