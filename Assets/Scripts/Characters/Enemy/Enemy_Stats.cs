using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : Character_Status
{
    // The enemy's basic stats
    private int levelStat;
    private int maxHealthStat;
    private int currentHealthStat;
    private int attackStat;
    private int defenseStat;
    private int magicStat;
    private int magDefenseStat;

    public Enemy_Stats(Enemy_Information baseStats)
    {
        this.levelStat = baseStats.Level;
        this.maxHealthStat = baseStats.MaxHealth;
        this.currentHealthStat = baseStats.MaxHealth;
        this.attackStat = baseStats.Power;
        this.defenseStat = baseStats.Defense;
        this.magicStat = baseStats.Intelligence;
        this.magDefenseStat = baseStats.Resistance;
    }

    public int DealDamage(int amount)
    {
        currentHealthStat -= amount;
        return currentHealthStat;
    }

    public int Level { get => levelStat; set => levelStat = value; }
    public int MaxHealth { get => maxHealthStat; set => maxHealthStat = value; }
    public int CurrentHealth { get => currentHealthStat; set => currentHealthStat = Mathf.Min(value, maxHealthStat); }
    public int Power { get => GetAdjustedPower(attackStat); set => attackStat = value; }
    public int Defense { get => defenseStat; set => defenseStat = value; }
    public int Intelligence { get => GetAdjustedInt(magicStat); set => magicStat = value; }
    public int Resistance { get => magDefenseStat; set => magDefenseStat = value; }
    public Dictionary<StatusEffect, int> StatusEffects { get => statusEffects; }
}
