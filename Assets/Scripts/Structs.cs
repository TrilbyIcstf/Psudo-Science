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

public struct EnemyMoveIntent
{
    private EnemyMoveName move;

    private MoveType type;

    private TargetingType targetingType;
    private int targets;

    private float potency;

    public EnemyMoveIntent(EnemyMoveName move, MoveType type, TargetingType targetingType, int targets, float potency)
    {
        this.move = move;
        this.type = type;
        this.targetingType = targetingType;
        this.targets = targets;
        this.potency = potency;
    }

    public EnemyMoveName Move { get => move; set => move = value; }
    public MoveType Type { get => type; set => type = value; }
    public TargetingType TargetingType { get => targetingType; set => targetingType = value; }
    public int Targets { get => targets; set => targets = value; }
    public float Potency { get => potency; set => potency = value; }
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
    public int SignedAmount { get => Mathf.Abs(amount) * (isIncrease ? 1 : -1); }
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

[System.Serializable]
public struct DungeonEnemy
{
    [SerializeField]
    private Vector2Int pos;
    [SerializeField]
    private Encounter encounter;
    [SerializeField]
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

public struct DungeonBoardMovement
{
    private Vector2Int destination;
    private bool jump;
    private float speed;

    public DungeonBoardMovement(Vector2Int destination, bool jump, float speed)
    {
        this.destination = destination;
        this.jump = jump;
        this.speed = speed;
    }

    public Vector2Int Destination { get => destination; set => destination = value; }
    public bool Jump { get => jump; set => jump = value; }
    public float Speed { get => speed; set => speed = value; }
}

[System.Serializable]
public struct DoorConnection
{
    [SerializeField]
    private Vector2Int mapPos;
    [SerializeField]
    private Vector2Int exitPos;
    [SerializeField]
    private bool dungeonExit;

    public DoorConnection(Vector2Int mapPos, Vector2Int exitPos, bool dungeonExit = false)
    {
        this.mapPos = mapPos;
        this.exitPos = exitPos;
        this.dungeonExit = dungeonExit;
    }

    public Vector2Int MapPos { get => mapPos; }
    public Vector2Int ExitPos { get => exitPos; }
    public bool DungeonExit { get => dungeonExit; }
}

public struct SavedBoardState
{
    private List<DungeonEnemy> enemies;
    private Vector2Int playerPos;

    public SavedBoardState(List<DungeonEnemy> enemies, Vector2Int playerPos)
    {
        this.enemies = enemies;
        this.playerPos = playerPos;
    }

    public List<DungeonEnemy> Enemies { get => enemies; set => enemies = value; }
    public Vector2Int PlayerPos { get => playerPos; set => playerPos = value; }
}