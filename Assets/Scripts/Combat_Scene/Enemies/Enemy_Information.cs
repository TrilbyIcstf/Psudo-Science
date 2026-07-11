using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scriptable object to hold information on enemies in combat
/// </summary>
[CreateAssetMenu(fileName = "Enemy Information", menuName = "ScriptableObjects/New Enemy", order = 3)]
[System.Serializable]
public class Enemy_Information : ScriptableObject
{
    // Info on the enemy's type
    [Header("Enemy Info")]
    [SerializeField] private string enemyName;
    [SerializeField] private Bestiary enemyType;

    // The enemy's basic stats
    [Header("Stats")]
    [SerializeField] private int levelStat = 1;
    [SerializeField] private int maxHealthStat = 100;
    [SerializeField] private int attackStat = 10;
    [SerializeField] private int defenseStat = 10;
    [SerializeField] private int magicStat = 10;
    [SerializeField] private int magDefenseStat = 10;

    // The image of the enemy
    [Header("Image")]
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private Vector2 spriteSize;
    [SerializeField] private float healthBarHeight;

    public int Level{ get => levelStat; set => levelStat = value; }
    public int MaxHealth { get => maxHealthStat; set => maxHealthStat = value; }
    public int Attack { get => attackStat; set => attackStat = value; }
    public int Defense { get => defenseStat; set => defenseStat = value; }
    public int Magic { get => magicStat; set => magicStat = value; }
    public int MagDefense { get => magDefenseStat; set => magDefenseStat = value; }
    public string EnemyName { get => enemyName; set => enemyName = value; }
    public Bestiary EnemyType { get => enemyType; set => enemyType = value; }
    public Sprite EnemySprite { get => enemySprite; set => enemySprite = value; }
    public Vector2 SpriteSize { get => spriteSize; set => spriteSize = value; }
    public float HealthBarHeight { get => healthBarHeight; set => healthBarHeight = value; }
}
