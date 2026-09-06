using UnityEngine;

public class Dungeon_Board_Generation : MonoBehaviour
{
    [SerializeField]
    private Dungeon_Tileset tileset;
    
    public DungeonTile[,] GenerateTestBoard()
    {
        int x = 8;
        int y = 8;
        float tileWidth = tileset.WhiteFloorTiles[0].GetComponent<MeshFilter>().sharedMesh.bounds.size.x;

        float startingX = gameObject.transform.position.x;
        startingX -= (((float)x / 2) * tileWidth);
        startingX += (tileWidth / 2);
        float startingY = gameObject.transform.position.y;
        startingY += (((float)y / 2) * tileWidth);
        startingY -= (tileWidth / 2);

        DungeonTile[,] dungeonMap = new DungeonTile[x,y];

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                Vector3 spawnPos = new Vector3(startingX + (tileWidth * i), 0, startingY - (tileWidth * j));

                GameObject tempTile;
                if ((i + (j * x)) % 5 == 0)
                {
                    if ((i + j) % 2 == 0)
                    {
                        tempTile = Instantiate(tileset.WhiteWallTiles[0], transform);
                    }
                    else
                    {
                        tempTile = Instantiate(tileset.BlackWallTiles[0], transform);
                    }
                    tempTile.name = "Wall_" + i + "_" + j;

                    tempTile.transform.position = spawnPos;
                    tempTile.GetComponent<Board_Tile_Interact>().Setup(new Vector2Int(i, j));
                    dungeonMap[i, j] = new DungeonTile(tempTile, DungeonTileType.WALL);
                }
                else
                {
                    if ((i + j) % 2 == 0)
                    {
                        tempTile = Instantiate(tileset.WhiteFloorTiles[0], transform);
                    }
                    else
                    {
                        tempTile = Instantiate(tileset.BlackFloorTiles[0], transform);
                    }
                    tempTile.name = "Floor_" + i + "_" + j;

                    tempTile.transform.position = spawnPos;
                    tempTile.GetComponent<Board_Tile_Interact>().Setup(new Vector2Int(i, j));
                    dungeonMap[i, j] = new DungeonTile(tempTile, DungeonTileType.FLOOR);
                }
            }
        }

        return dungeonMap;
    }

    public GameObject GeneratePlayer()
    {
        return Instantiate(tileset.ChessPieces.GetValue(ChessPiece.QUEEN), transform);
    }

    public DungeonEnemy GenerateEnemy(Vector2Int pos, Encounter encounter, ChessPiece type)
    {
        GameObject tempPiece = Instantiate(tileset.ChessPieces.GetValue(type), transform);
        return new DungeonEnemy(pos, encounter, type, tempPiece);
    }
}
