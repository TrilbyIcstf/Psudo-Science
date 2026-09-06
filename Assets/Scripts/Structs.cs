using System.Collections.Generic;
using UnityEngine;

public struct QueuedMove
{
    public GameObject move;

    public PC user;

    public QueuedMove(GameObject move, PC user)
    {
        this.move = move;
        this.user = user;
    }
}

public struct QueuedEnemyMove
{
    public GameObject move;

    public int user;

    public TargetingType targetingType;
    public int targets;

    public float potency;

    public QueuedEnemyMove(GameObject move, int user, TargetingType targetingType, int targets, float potency)
    {
        this.move = move;
        this.user = user;
        this.targetingType = targetingType;
        this.targets = targets;
        this.potency = potency;
    }
}

public struct MoveResult
{
    private float potency;

    private Target targetType;
    private int targetNum;

    private Effectiveness effectiveness;

    public MoveResult(float potency, Target targetType, int targetNum, Effectiveness effectiveness = Effectiveness.NEUTRAL)
    {
        this.potency = potency;
        this.targetType = targetType;
        this.targetNum = targetNum;
        this.effectiveness = effectiveness;
    }

    public float Potency { get => potency; set => potency = value; }
    public Target TargetType { get => targetType; set => targetType = value; }
    public int TargetNum { get => targetNum; set => targetNum = value; }
    public Effectiveness Effectiveness { get => effectiveness; set => effectiveness = value; }
}

public struct AnimDetails
{
    public CombatAnimation anim;
    public Target targetType;
    public int target;
    public float? rotation;
    public Color? color;

    public AnimDetails(CombatAnimation anim, Target targetType, int target, float? rotation, Color? color)
    {
        this.anim = anim;
        this.targetType = targetType;
        this.target = target;
        this.rotation = rotation;
        this.color = color;
    }
}

public struct BarChangeDetails
{
    private int amount;
    private Effectiveness effectiveness;
    private bool isIncrease;

    public BarChangeDetails(int amount, Effectiveness effectiveness, bool isIncrease)
    {
        this.amount = amount;
        this.effectiveness = effectiveness;
        this.isIncrease = isIncrease;
    }

    public int Amount { get => amount; }
    public Effectiveness Effectiveness { get => effectiveness; }
    public bool IsIncrease { get => isIncrease; }
}

public struct DungeonTile
{
    private GameObject tile;
    private DungeonTileType type;
    private Board_Tile_Interact tileScript;

    public DungeonTile(GameObject tile, DungeonTileType type)
    {
        this.tile = tile;
        this.type = type;
        tileScript = this.tile.GetComponent<Board_Tile_Interact>();
    }

    public GameObject Tile { get => tile; }
    public DungeonTileType Type { get => type; }
    public Board_Tile_Interact TileScript { get => tileScript; }
}

public struct DungeonEnemy
{
    private Vector2Int pos;
    private Encounter encounter;
    private ChessPiece piece;
    private GameObject enemyObject;

    public DungeonEnemy(Vector2Int pos, Encounter encounter, ChessPiece piece, GameObject enemyObject)
    {
        this.pos = pos;
        this.encounter = encounter;
        this.piece = piece;
        this.enemyObject = enemyObject;
    }

    public Vector2Int Pos { get => pos; set => pos = value; }
    public Encounter Encounter { get => encounter; }
    public ChessPiece Piece { get => piece; }
    public GameObject EnemyObject { get => enemyObject; }
}