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

    public int GetAdjustedPower(int initial)
    {
        float flatBoost = 0;
        float mult = 1.0f;

        foreach (var se in statusEffects)
        {
            switch(se.Key)
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

    // Get/Set
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool IsDead { get => isDead; set => isDead = value; }
    public float ReviveProgress { get => reviveProgress; set => reviveProgress = value; }
    public Dictionary<StatusEffect, int> StatusEffects { get => statusEffects; set => statusEffects = value; }
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
