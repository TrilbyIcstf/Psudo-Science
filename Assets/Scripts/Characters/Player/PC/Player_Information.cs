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
    // Information on the represented character
    [Header("Character Info")]
    [SerializeField] private string CharacterFirstName = "John";
    [SerializeField] private string CharacterLastName = "Doe";
    [SerializeField] private PlayerClass PlayerClass = PlayerClass.HERO;

    // Information on character's combat stats
    [Header("Stats")]
    [SerializeField] private int Level = 1;
    [SerializeField] private int MaxHealthStat = 100;
    [SerializeField] private int AttackStat = 10;
    [SerializeField] private int DefenseStat = 10;
    [SerializeField] private int MagicStat = 10;
    [SerializeField] private int MagDefenseStat = 10;

    // Public getters
    public string GetFirstName()
    {
        return CharacterFirstName;
    }

    public string GetLastName()
    {
        return CharacterLastName;
    }

    public string GetName()
    {
        return CharacterFirstName + " " + CharacterLastName;
    }

    public PlayerClass GetClass()
    {
        return PlayerClass;
    }

    public int GetBaseMaxHealth()
    {
        return MaxHealthStat;
    }

    public int GetBaseAttack()
    {
        return AttackStat;
    }

    public int GetBaseDefense()
    {
        return DefenseStat;
    }

    public int GetBaseMagic()
    {
        return MagicStat;
    }

    public int GetBaseMagDefense()
    {
        return MagDefenseStat;
    }

    // Public setters
    public void SetName(string _FName, string _LName)
    {
        CharacterFirstName = _FName;
        CharacterLastName = _LName;
    }

    public void SetClass(PlayerClass _Val)
    {
        PlayerClass = _Val;
    }

    public void LevelUp(int _LVL, int _MHP, int _ATK, int _DEF, int _MAT, int _MDF)
    {
        Level += _LVL;
        MaxHealthStat += _MHP;
        AttackStat += _ATK;
        DefenseStat += _DEF;
        MagicStat += _MAT;
        MagDefenseStat += _MDF;
    }

    public void SetLevel(int _Val)
    {
        Level = _Val;
    }

    public void SetHealth(int _Val)
    {
        MaxHealthStat = _Val;
    }

    public void SetAttack(int _Val)
    {
        AttackStat = _Val;
    }

    public void SetDefense(int _Val)
    {
        DefenseStat = _Val;
    }

    public void SetMagic(int _Val)
    {
        MagicStat = _Val;
    }

    public void SetMagDefense(int _Val)
    {
        MagDefenseStat = _Val;
    }
}
