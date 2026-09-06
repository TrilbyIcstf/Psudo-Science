using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dungeon_Board_Controller : MonoBehaviour
{
    [SerializeField]
    private Encounter testEncounter;

    private Dungeon_Board_Generation generator;

    private DungeonTile[,] tileMap;
    private List<Vector2Int> floorTiles;
    private Vector2Int playerPos;

    private GameObject playerPiece;

    private List<DungeonEnemy> enemyPieces = new List<DungeonEnemy>();

    private DungeonBoardState boardState = DungeonBoardState.Neutral;

    private void Awake()
    {
        Setup();
    }

    public void Setup()
    {
        generator = GetComponent<Dungeon_Board_Generation>();
        GameManager.instance.dungeon.Board = this;
        tileMap = generator.GenerateTestBoard();
        GetFloorTiles();
        ResetTileInteraction();

        playerPos = new Vector2Int(5, 3);
        playerPiece = generator.GeneratePlayer();
        PositionPlayerPiece();
        TileMap(playerPos).TileScript.Interactable = true;

        enemyPieces.Add(generator.GenerateEnemy(new Vector2Int(5, 2), testEncounter, ChessPiece.KNIGHT));
        enemyPieces.Add(generator.GenerateEnemy(new Vector2Int(6, 2), testEncounter, ChessPiece.BISHOP));
        PositionEnemyPieces();
    }

    public void SelectTile(Vector2Int tilePos) 
    {
        switch(boardState)
        {
            case DungeonBoardState.Neutral:
                SetupPlayerMovement();
                break;
            case DungeonBoardState.PlayerSelectingMovement:
                MovePlayer(tilePos);
                break;
        }
    }

    private void SetupPlayerMovement()
    {
        boardState = DungeonBoardState.PlayerSelectingMovement;
        ResetTileInteraction();
        foreach (Vector2Int pos in QueenMovementOptions(playerPos))
        {
            TileMap(pos).TileScript.HighlightTile();
            TileMap(pos).TileScript.Interactable = true;
        }
        Vector3 playerPiecePos = playerPiece.transform.position;
        playerPiecePos.y += 0.3f;
        playerPiece.transform.position = playerPiecePos;
    }

    private void MovePlayer(Vector2Int pos)
    {
        ResetTileInteraction();
        playerPos = pos;
        PositionPlayerPiece();

        MoveEnemies();

        boardState = DungeonBoardState.Neutral;
        TileMap(pos).TileScript.Interactable = true;
    }

    private void MoveEnemies()
    {
        for (int i = 0; i < enemyPieces.Count; i++)
        {
            DungeonEnemy enemy = enemyPieces[i];
            enemy.Pos = DecideEnemyMovement(enemy);
            enemyPieces[i] = enemy;
        }
        PositionEnemyPieces();
    }

    public HashSet<Vector2Int> QueenMovementOptions(Vector2Int pos)
    {
        HashSet<Vector2Int> options = new HashSet<Vector2Int>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                Vector2Int tempPos = pos;
                Vector2Int direction = new Vector2Int(x, y);
                tempPos += direction;

                while (IsWalkableTile(tempPos))
                {
                    options.Add(tempPos);
                    if (EnemyOnTile(tempPos)) { break; }

                    tempPos += direction;
                }
            }
        }

        return options;
    }

    public HashSet<Vector2Int> KnightMovementOptions(Vector2Int pos)
    {
        HashSet<Vector2Int> options = new HashSet<Vector2Int>();
        for (int i = 1; i < 3; i++)
        {
            for (int j = -1; j <= 1; j = j + 2)
            {
                for (int k = -1; k <= 1; k = k + 2)
                {
                    Vector2Int move = new Vector2Int(i * j, (3 - i) * k);
                    Vector2Int tempPos = pos + move;
                    if (IsWalkableTile(tempPos, false))
                    {
                        options.Add(tempPos);
                    }
                }
            }
        }

        return options;
    }

    public HashSet<Vector2Int> RookMovementOptions(Vector2Int pos)
    {
        HashSet<Vector2Int> options = new HashSet<Vector2Int>();
        for (int x = -1; x <= 1; x = x + 2)
        {
            Vector2Int tempPos = pos;
            Vector2Int direction = new Vector2Int(x, 0);
            tempPos += direction;
            while (IsWalkableTile(tempPos))
            {
                options.Add(tempPos);
                tempPos += direction;
            }
        }

        for (int y = -1; y <= 1; y = y + 2)
        {
            Vector2Int tempPos = pos;
            Vector2Int direction = new Vector2Int(0, y);
            tempPos += direction;
            while (IsWalkableTile(tempPos, false))
            {
                options.Add(tempPos);
                tempPos += direction;
            }
        }

        return options;
    }

    public HashSet<Vector2Int> BishopMovementOptions(Vector2Int pos)
    {
        HashSet<Vector2Int> options = new HashSet<Vector2Int>();
        for (int x = -1; x <= 1; x = x + 2)
        {
            for (int y = -1; y <= 1; y = y + 2)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                Vector2Int tempPos = pos;
                Vector2Int direction = new Vector2Int(x, y);
                tempPos += direction;
                while (IsWalkableTile(tempPos, false))
                {
                    options.Add(tempPos);
                    tempPos += direction;
                }
            }
        }

        return options;
    }

    public HashSet<Vector2Int> KingMovementOptions(Vector2Int pos)
    {
        HashSet<Vector2Int> options = new HashSet<Vector2Int>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                {
                    continue;
                }
                Vector2Int tempPos = pos;
                Vector2Int direction = new Vector2Int(x, y);
                tempPos += direction;
                if (IsWalkableTile(tempPos, false))
                {
                    options.Add(tempPos);
                }
            }
        }

        return options;
    }

    private Vector2Int DecideEnemyMovement(DungeonEnemy enemy)
    {
        (Vector2Int nextPos, _, _) = DecideEnemyMovementRec(enemy, enemy.Pos);
        return nextPos;
    }

    private (Vector2Int nextPos, Dictionary<Vector2Int, int> pathList, bool foundPlayer) DecideEnemyMovementRec(DungeonEnemy enemy, Vector2Int fromPos, int dist = 0, Dictionary<Vector2Int, int> pathList = null)
    {
        dist++;
        if (pathList == null)
        {
            pathList = new Dictionary<Vector2Int, int>();
        } else if (pathList.ContainsKey(playerPos) && pathList[playerPos] <= dist)
        {
            return (fromPos, pathList, false);
        }

        HashSet<Vector2Int> movementOptions = EnemyMovementOptions(enemy, fromPos);
        movementOptions = movementOptions.OrderBy(pos => GetManhattanDistanceFromPlayer(pos)).ToHashSet();

        Vector2Int nextPos = fromPos;
        bool foundPlayer = false;
        foreach (Vector2Int pos in movementOptions)
        {
            if (!pathList.ContainsKey(pos) || pathList[pos] > dist)
            {
                pathList[pos] = dist;
                if (pos == playerPos)
                {
                    return (pos, pathList, true);
                }

                bool viablePath;
                (_, pathList, viablePath) = DecideEnemyMovementRec(enemy, pos, dist, pathList);
                if (viablePath)
                {
                    foundPlayer = true;
                    nextPos = pos;
                }
            }
        }

        return (nextPos, pathList, foundPlayer);
    }

    private int GetManhattanDistanceFromPlayer(Vector2Int pos)
    {
        return Mathf.Abs(pos.x - playerPos.x) + Mathf.Abs(pos.y - playerPos.y);
    }

    private HashSet<Vector2Int> EnemyMovementOptions(DungeonEnemy enemy, Vector2Int pos)
    {
        switch (enemy.Piece)
        {
            case ChessPiece.BISHOP:
                return BishopMovementOptions(pos);
            case ChessPiece.KNIGHT:
                return KnightMovementOptions(pos);
            case ChessPiece.ROOK:
                return RookMovementOptions(pos);
            case ChessPiece.KING:
                return KingMovementOptions(pos);
            default:
                return new HashSet<Vector2Int>();
        }
    }
    public bool IsWalkableTile(Vector2Int pos, bool throughEnemies = true)
    {
        if (pos.x >= tileMap.GetLength(0) || pos.x < 0 || pos.y >= tileMap.GetLength(1) || pos.y < 0)
        {
            return false;
        }
        if (EnemyOnTile(pos))
        {
            return throughEnemies;
        }

        return tileMap[pos.x, pos.y].Type == DungeonTileType.FLOOR;
    }

    public bool EnemyOnTile(Vector2Int pos)
    {
        foreach (DungeonEnemy enemy in enemyPieces)
        {
            if (enemy.Pos == pos)
            {
                return true;
            }
        }
        return false;
    }

    private void PositionPlayerPiece()
    {
        Vector3 queenPos = TileMap(playerPos).Tile.transform.position;
        queenPos.y += TileMap(playerPos).TileScript.HalfHeight();
        playerPiece.transform.position = queenPos;
    }

    private void PositionEnemyPieces()
    {
        foreach (DungeonEnemy e in enemyPieces)
        {
            Vector3 ePos = TileMap(e.Pos).Tile.transform.position;
            ePos.y += TileMap(e.Pos).TileScript.HalfHeight();
            e.EnemyObject.transform.position = ePos;
        }
    }

    public DungeonTile TileMap(Vector2Int pos)
    {
        return tileMap[pos.x, pos.y];
    }

    private void GetFloorTiles()
    {
        floorTiles = new List<Vector2Int>();
        for (int i = 0; i < tileMap.GetLength(0); i++)
        {
            for (int j = 0; j < tileMap.GetLength(1); j++)
            {
                if (tileMap[i, j].Type == DungeonTileType.FLOOR)
                {
                    floorTiles.Add(new Vector2Int(i, j));
                }
            }
        }
    }

    private void ResetTileInteraction()
    {
        foreach (Vector2Int pos in floorTiles)
        {
            TileMap(pos).TileScript.Interactable = false;
            TileMap(pos).TileScript.UnhighlightTile();
        }
    }
}
