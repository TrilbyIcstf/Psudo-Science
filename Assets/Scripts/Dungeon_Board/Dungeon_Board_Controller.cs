using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dungeon_Board_Controller : MonoBehaviour
{
    private const float ENEMYMOVEMENTSPEED = 3.0f;
    private const float ENEMYMOVEMENTDELAY = 0.25f;
    private const float JUMPHEIGHT = 2.0f;

    private Dungeon_Board_Generation generator;

    private DungeonTile[,] tileMap;
    private List<Vector2Int> floorTiles;
    private DoorConnectionDictionary doorTiles;
    private Vector2Int playerPos;

    private GameObject playerPiece;

    private List<DungeonEnemy> enemyPieces = new List<DungeonEnemy>();

    private DungeonBoardState boardState = DungeonBoardState.Neutral;

    private void Awake()
    {
        generator = GetComponent<Dungeon_Board_Generation>();
        GameManager.instance.dungeon.SetupDungeonBoard(this);
    }

    public void FreshSetup(Dungeon_Board_Layout layout, Vector2Int playerStart)
    {
        tileMap = generator.GenerateBoard(layout);
        GetFloorTiles();
        doorTiles = layout.DoorConnections;
        ResetTileInteraction();

        playerPos = playerStart;
        playerPiece = generator.GeneratePlayer();
        PositionPlayerPiece();
        TileMap(playerPos).TileScript.Interactable = true;


        foreach (DungeonEnemy enemy in layout.BaseEnemies)
        {
            enemyPieces.Add(generator.GenerateEnemy(enemy.Pos, enemy.Encounter, enemy.Piece));
        }
        PositionEnemyPieces();
    }

    public void ResumeSetup(Dungeon_Board_Layout layout, SavedBoardState state)
    {
        tileMap = generator.GenerateBoard(layout);
        GetFloorTiles();
        doorTiles = layout.DoorConnections;
        ResetTileInteraction();

        playerPos = state.PlayerPos;
        playerPiece = generator.GeneratePlayer();
        PositionPlayerPiece();
        TileMap(playerPos).TileScript.Interactable = true;


        foreach (DungeonEnemy enemy in state.Enemies)
        {
            enemyPieces.Add(generator.GenerateEnemy(enemy.Pos, enemy.Encounter, enemy.Piece));
        }
        PositionEnemyPieces();
    }

    private bool ShouldBeginEncounter()
    {
        return enemyPieces.Any(p => p.Pos == playerPos);
    }

    private void BeginEncounter()
    {
        List<DungeonEnemy> remainingEnemies = enemyPieces.Where(e => e.Pos != playerPos).ToList();

        SavedBoardState state = new SavedBoardState(remainingEnemies, playerPos);
        GameManager.instance.dungeon.SavedBoard = state;

        Encounter encounter = GetEnemyAt(playerPos).Encounter;
        GameManager.instance.transition.TransitionToEncounter(encounter);
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
        foreach (Vector2Int pos in QueenMovementOptions(playerPos, false))
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

        if (ShouldBeginEncounter())
        {
            BeginEncounter();
        } else if (doorTiles.ContainsKey(playerPos))
        {
            DoorConnection dc = doorTiles.GetValue(playerPos);
            if (dc.DungeonExit)
            {
                Application.Quit();
            }
            GameManager.instance.dungeon.MoveThroughDoor(dc);
        } else
        {
            StartCoroutine(MoveEnemies());
        }
    }

    private IEnumerator MoveEnemies()
    {
        boardState = DungeonBoardState.EnemyMovement;

        for (int i = 0; i < enemyPieces.Count; i++)
        {
            DungeonEnemy enemy = enemyPieces[i];
            Vector2Int enemyMovement = DecideEnemyMovement(enemy);

            if (enemyMovement != enemy.Pos)
            {
                List<DungeonBoardMovement> movementInstructions = enemy.Piece == ChessPiece.KNIGHT ? KnightMovementInstructions(enemy, enemyMovement) : new List<DungeonBoardMovement>() { new DungeonBoardMovement(enemyMovement, false, ENEMYMOVEMENTSPEED) };

                foreach (DungeonBoardMovement instruction in movementInstructions)
                {
                    Vector3 startingPos = TileMap(enemy.Pos).Tile.transform.position;
                    Vector3 endingPos = TileMap(instruction.Destination).Tile.transform.position;
                    Vector3 piecePos = enemy.EnemyObject.transform.position;
                    float startingHeight = piecePos.y;

                    float dist = Vector3.Distance(startingPos, endingPos);
                    float speed = ENEMYMOVEMENTSPEED / dist;
                    float progress = 0;
                    yield return new WaitUntil(() =>  {
                        progress += speed * Time.deltaTime;
                        piecePos.x = Mathf.Lerp(startingPos.x, endingPos.x, progress);
                        piecePos.z = Mathf.Lerp(startingPos.z, endingPos.z, progress);
                        if (instruction.Jump)
                        {
                            piecePos.y = startingHeight + (Mathf.Sin(progress * Mathf.PI) * JUMPHEIGHT);
                        }

                        enemy.EnemyObject.transform.position = piecePos;

                        return progress >= 1;
                    });

                    enemy.Pos = instruction.Destination;
                }

                enemyPieces[i] = enemy;
                yield return new WaitForSeconds(ENEMYMOVEMENTDELAY);
            }

            if (enemyMovement == playerPos)
            {
                break;
            }
        }

        if (ShouldBeginEncounter())
        {
            BeginEncounter();
        } else
        {
            PositionEnemyPieces();
            boardState = DungeonBoardState.Neutral;
            TileMap(playerPos).TileScript.Interactable = true;
        }
    }

    public HashSet<Vector2Int> QueenMovementOptions(Vector2Int pos, bool isEnemy = true)
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

                while (IsWalkableTile(tempPos, !isEnemy))
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

    private List<DungeonBoardMovement> KnightMovementInstructions(DungeonEnemy enemy, Vector2Int movement)
    {
        if (!IsHorsieViable(enemy.Pos, movement))
        {
            return new List<DungeonBoardMovement>();
        }

        List<DungeonBoardMovement> instructions = new List<DungeonBoardMovement>();

        int xSign = (int)Mathf.Sign(movement.x - enemy.Pos.x);
        int ySign = (int)Mathf.Sign(movement.y - enemy.Pos.y);

        Vector2Int option1 = new Vector2Int(movement.x, enemy.Pos.y);
        Vector2Int option2 = new Vector2Int(enemy.Pos.x, movement.y);

        if (Random.Range(0, 2) == 0)
        {
            Vector2Int tempVar = option1;
            option1 = option2;
            option2 = tempVar;
        }

        if (IsWalkableTile(option1, false))
        {
            instructions.Add(new DungeonBoardMovement(option1, false, ENEMYMOVEMENTSPEED));
            instructions.Add(new DungeonBoardMovement(movement, false, ENEMYMOVEMENTSPEED));
        } else if (IsWalkableTile(option2, false))
        {
            instructions.Add(new DungeonBoardMovement(option2, false, ENEMYMOVEMENTSPEED));
            instructions.Add(new DungeonBoardMovement(movement, false, ENEMYMOVEMENTSPEED));
        } else
        {
            instructions.Add(new DungeonBoardMovement(movement, true, ENEMYMOVEMENTSPEED));
        }

        return instructions;
    }

    private bool IsHorsieViable(Vector2Int start, Vector2Int end)
    {
        return GetManhattanDistance(start, end) == 3 && start.x != end.x && start.y != end.y;
    }

    public HashSet<Vector2Int> RookMovementOptions(Vector2Int pos)
    {
        HashSet<Vector2Int> options = new HashSet<Vector2Int>();
        for (int x = -1; x <= 1; x = x + 2)
        {
            Vector2Int tempPos = pos;
            Vector2Int direction = new Vector2Int(x, 0);
            tempPos += direction;
            while (IsWalkableTile(tempPos, false))
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
        return GetManhattanDistance(pos, playerPos);
    }

    private int GetManhattanDistance(Vector2Int start, Vector2Int end)
    {
        return Mathf.Abs(start.x - end.x) + Mathf.Abs(start.y - end.y);
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
            case ChessPiece.QUEEN:
                return QueenMovementOptions(pos);
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
        if (EnemyOnTile(pos) || TileMap(pos).Type == DungeonTileType.DOOR)
        {
            return throughEnemies;
        }

        return floorTiles.Contains(pos);
        //return tileMap[pos.x, pos.y].Type == DungeonTileType.FLOOR;
    }

    public DungeonEnemy GetEnemyAt(Vector2Int pos)
    {
        foreach (DungeonEnemy enemy in enemyPieces)
        {
            if (enemy.Pos == pos)
            {
                return enemy;
            }
        }

        throw new System.ApplicationException("No enemy at requested position!");
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

        foreach (Vector2Int pos in doorTiles.Keys())
        {
            TileMap(pos).TileScript.Interactable = false;
            TileMap(pos).TileScript.UnhighlightTile();
        }
    }
}
