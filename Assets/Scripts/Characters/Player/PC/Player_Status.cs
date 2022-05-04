using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks variables of the player that will often change during combat.
/// </summary>
public class Player_Status
{
    private int currentHealth;

    private bool poisoned = false;
    private bool burned = false;
    private bool confused = false;

    private int healthBuff = 0;
    private int attackBuff = 0;
    private int defenseBuff = 0;
    private int magicBuff = 0;
    private int magDefenseBuff = 0;

    private float healthMult = 1;
    private float attackMult = 1;
    private float defenseMult = 1;
    private float magicMult = 1;
    private float magicDefMult = 1;

    public Player_Status(int _health)
    {
        currentHealth = _health;
    }

    // Get/Set
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public bool Poisoned { get => poisoned; set => poisoned = value; }
    public bool Burned { get => burned; set => burned = value; }
    public bool Confused { get => confused; set => confused = value; }
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
    public float MagicDefMult { get => magicDefMult; set => magicDefMult = value; }
}
