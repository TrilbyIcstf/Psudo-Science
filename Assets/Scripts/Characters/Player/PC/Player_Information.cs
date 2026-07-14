using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object to hold information on each party member.
/// </summary>
[CreateAssetMenu(fileName = "Player Information", menuName = "ScriptableObjects/New PC", order = 1)]
[System.Serializable]
public class Player_Information : ScriptableObject
{
    [Header("Game Position")]
    [SerializeField] public int position = 0;

    // Information on the represented character
    [Header("Character Info")]
    [SerializeField] private string characterFirstName = "Jane";
    [SerializeField] private string characterLastName = "Doe";
    [SerializeField] private PlayerClass playerClass = PlayerClass.HERO;

    // Information on character's combat stats
    [Header("Stats")]
    [SerializeField] private int levelStat = 1;
    [SerializeField] private int maxHealthStat = 100;
    [SerializeField] private int attackStat = 10;
    [SerializeField] private int defenseStat = 10;
    [SerializeField] private int magicStat = 10;
    [SerializeField] private int magDefenseStat = 10;

    [Header("Equipment")]
    [SerializeField] private Equip_Information eqWeapon;
    [SerializeField] private Equip_Information eqHelmet;
    [SerializeField] private Equip_Information eqArmor;
    [SerializeField] private Equip_Information eqPant;
    [SerializeField] private Equip_Information eqAcc1;
    [SerializeField] private Equip_Information eqAcc2;

    [Header("Status")]
    [SerializeField] private Player_Status status = new Player_Status(1);

    public void LevelUp(int _LVL, int _MHP, int _ATK, int _DEF, int _MAT, int _MDF)
    {
        levelStat += _LVL;
        maxHealthStat += _MHP;
        attackStat += _ATK;
        defenseStat += _DEF;
        magicStat += _MAT;
        magDefenseStat += _MDF;
    }

    public void Heal(int heal)
    {
        int newHealth = Mathf.Min(status.CurrentHealth + heal, MaxHealth);
        status.CurrentHealth = newHealth;
    }

    public void Damage(int damage)
    {
        int newHealth = Mathf.Max(status.CurrentHealth - damage, 0);
        status.CurrentHealth = newHealth;
    }

    public void resetStatus(int health, bool cleanse = false)
    {
        health = Mathf.Clamp(health, 0, EquipMaxHealth);
        status = new Player_Status(status, health, cleanse);
    }

    public void resetStatus(bool heal = true, bool cleanse = false)
    {
        status = new Player_Status(status, heal ? EquipMaxHealth : status.CurrentHealth, cleanse);
    }

    // Get/Set
    public int Position { get => position; }
    public string FirstName { get => characterFirstName; set => characterFirstName = value; }
    public string LastName { get => characterLastName; set => characterLastName = value; }
    public string FullName { get => characterFirstName + " " + characterLastName; }
    public PlayerClass Job { get => playerClass; set => playerClass = value; }
    public int Level { get => levelStat; set => levelStat = value; }
    public int BaseMaxHealth { get => maxHealthStat; set => maxHealthStat = value; }
    public int BaseAttack { get => attackStat; set => attackStat = value; }
    public int BaseDefense { get => defenseStat; set => defenseStat = value; }
    public int BaseMagic { get => magicStat; set => magicStat = value; }
    public int BaseMagDefense { get => magDefenseStat; set => magDefenseStat = value; }
    public int EquipMaxHealth { get => maxHealthStat + eqWeapon.Health + eqHelmet.Health + eqArmor.Health + eqPant.Health + eqAcc1.Health + eqAcc2.Health; }
    public int EquipAttack { get => attackStat + eqWeapon.Attack + eqHelmet.Attack + eqArmor.Attack + eqPant.Attack + eqAcc1.Attack + eqAcc2.Attack; }
    public int EquipDefense { get => defenseStat + eqWeapon.Defense + eqHelmet.Defense + eqArmor.Defense + eqPant.Defense + eqAcc1.Defense + eqAcc2.Defense; }
    public int EquipMagic { get => magicStat + eqWeapon.Magic + eqHelmet.Magic + eqArmor.Magic + eqPant.Magic + eqAcc1.Magic + eqAcc2.Magic; }
    public int EquipMagDefense { get => magDefenseStat + eqWeapon.MagDefense + eqHelmet.MagDefense + eqArmor.MagDefense + eqPant.MagDefense + eqAcc1.MagDefense + eqAcc2.MagDefense; }
    public int MaxHealth { get => Mathf.CeilToInt((EquipMaxHealth + status.HealthBuff) * status.HealthMult); }
    public int CurrentHealth { get => status.CurrentHealth; }
    public int CurrentDamage { get => MaxHealth - status.CurrentHealth; }
    public int Attack { get => Mathf.CeilToInt((EquipAttack + status.AttackBuff) * status.AttackMult); }
    public int Defense { get => Mathf.CeilToInt((EquipDefense + status.DefenseBuff) * status.DefenseMult); }
    public int Magic { get => Mathf.CeilToInt((EquipMagic + status.MagicBuff) * status.MagicMult); }
    public int MagDefense { get => Mathf.CeilToInt((EquipMagDefense + status.MagDefenseBuff) * status.MagDefenseMult); }
    public Equip_Information Weapon { get => eqWeapon; set => eqWeapon = value; }
    public Equip_Information Helmet { get => eqHelmet; set => eqHelmet = value; }
    public Equip_Information Armor { get => eqArmor; set => eqArmor = value; }
    public Equip_Information Pant { get => eqPant; set => eqPant = value; }
    public Equip_Information Acc1 { get => eqAcc1; set => eqAcc1 = value; }
    public Equip_Information Acc2 { get => eqAcc2; set => eqAcc2 = value; }
    public Player_Status Status { get => status; set => status = value; }
}
