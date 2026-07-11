using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats
{
    // The enemy's basic stats
    private int levelStat;
    private int maxHealthStat;
    private int currentHealthStat;
    private int attackStat;
    private int defenseStat;
    private int magicStat;
    private int magDefenseStat;

    private int maxHealthBoost = 0;
    private int attackBoost = 0;
    private int defenseBoost = 0;
    private int magicBoost = 0;
    private int magDefenseBoost = 0;
    private int actSpeedBoost = 0;

    public Enemy_Stats(Enemy_Information baseStats)
    {
        this.levelStat = baseStats.Level;
        this.maxHealthStat = baseStats.MaxHealth;
        this.currentHealthStat = baseStats.MaxHealth;
        this.attackStat = baseStats.Attack;
        this.defenseStat = baseStats.Defense;
        this.magicStat = baseStats.Magic;
        this.magDefenseStat = baseStats.MagDefense;
    }

    public int DealDamage(int amount)
    {
        currentHealthStat -= amount;
        return currentHealthStat;
    }

    public int Level { get => levelStat; set => levelStat = value; }
    public int MaxHealth { get => maxHealthStat; set => maxHealthStat = value; }
    public int CurrentHealth { get => currentHealthStat; set => currentHealthStat = Mathf.Min(value, maxHealthStat + maxHealthBoost); }
    public int Attack { get => attackStat; set => attackStat = value; }
    public int Defense { get => defenseStat; set => defenseStat = value; }
    public int Magic { get => magicStat; set => magicStat = value; }
    public int MagDefense { get => magDefenseStat; set => magDefenseStat = value; }
}
