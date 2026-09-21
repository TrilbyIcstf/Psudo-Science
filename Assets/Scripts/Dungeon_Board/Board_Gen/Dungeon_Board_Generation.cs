using UnityEngine;

public class Dungeon_Board_Generation : MonoBehaviour
{
    [SerializeField]
    private Dungeon_Tileset tileset;
    
    public DungeonTile[,] GenerateTestBoard()
    {
        int x = 12;
        int y = 12;
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
                else if ((i + (j * x)) % 21 == 0)
                {
                    tempTile = Instantiate(tileset.EmptyFloorTile, transform);
                    tempTile.name = "Blank_" + i + "_" + j;

                    tempTile.transform.position = spawnPos;
                    tempTile.GetComponent<Board_Tile_Interact>().Setup(new Vector2Int(i, j));
                    dungeonMap[i, j] = new DungeonTile(tempTile, DungeonTileType.BLANK);
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

    public DungeonTile[,] GenerateBoard(Dungeon_Board_Layout layout)
    {
        Texture2D mapImage = layout.MapImage;

        int width = mapImage.width;
        int height = mapImage.height;

        float tileWidth = tileset.WhiteFloorTiles[0].GetComponent<MeshFilter>().sharedMesh.bounds.size.x;

        float startingX = gameObject.transform.position.x;
        startingX -= (((float)width / 2) * tileWidth);
        startingX += (tileWidth / 2);
        float startingY = gameObject.transform.position.y;
        startingY -= (((float)height / 2) * tileWidth);
        startingY += (tileWidth / 2);

        Color[] pixels = mapImage.GetPixels();
        DungeonTile[,] dungeonMap = new DungeonTile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int index = y * width + x;

                Color pixelColor = pixels[index];

                Vector3 spawnPos = new Vector3(startingX + (tileWidth * x), 0, startingY + (tileWidth * y));

                dungeonMap[x, y] = GenerateTile(x, y, pixelColor, spawnPos);
            }
        }

        return dungeonMap;
    }

    private DungeonTile GenerateTile(int x, int y, Color pixel, Vector3 spawnPos)
    {
        int tileType = (int)(pixel.r * 255);
        int tileVarient = (int)(pixel.g * 255);

        GameObject tempTile;

        if (tileType == 0)
        {
            tempTile = Instantiate(tileset.EmptyFloorTile, transform);
            tempTile.name = "Blank_" + x + "_" + y;

            tempTile.transform.position = spawnPos;
            tempTile.GetComponent<Board_Tile_Interact>().Setup(new Vector2Int(x, y));
            return new DungeonTile(tempTile, DungeonTileType.BLANK);
        } else if (tileType == 1)
        {
            if ((x + y) % 2 == 0)
            {
                tempTile = Instantiate(tileset.WhiteFloorTiles[tileVarient], transform);
            }
            else
            {
                tempTile = Instantiate(tileset.BlackFloorTiles[tileVarient], transform);
            }
            tempTile.name = "Floor_" + x + "_" + y;

            tempTile.transform.position = spawnPos;
            tempTile.GetComponent<Board_Tile_Interact>().Setup(new Vector2Int(x, y));
            return new DungeonTile(tempTile, DungeonTileType.FLOOR);
        } else if (tileType == 2)
        {
            if ((x + y) % 2 == 0)
            {
                tempTile = Instantiate(tileset.WhiteWallTiles[tileVarient], transform);
            }
            else
            {
                tempTile = Instantiate(tileset.BlackWallTiles[tileVarient], transform);
            }
            tempTile.name = "Wall_" + x + "_" + y;

            tempTile.transform.position = spawnPos;
            tempTile.GetComponent<Board_Tile_Interact>().Setup(new Vector2Int(x, y));
            return new DungeonTile(tempTile, DungeonTileType.WALL);
        } else if (tileType == 3)
        {
            if ((x + y) % 2 == 0)
            {
                tempTile = Instantiate(tileset.WhiteFloorTiles[tileVarient], transform);
            }
            else
            {
                tempTile = Instantiate(tileset.BlackFloorTiles[tileVarient], transform);
            }
            tempTile.name = "Door_" + x + "_" + y;

            tempTile.transform.position = spawnPos;
            tempTile.GetComponent<Board_Tile_Interact>().Setup(new Vector2Int(x, y));
            return new DungeonTile(tempTile, DungeonTileType.DOOR);
        }

        throw new System.ApplicationException("Invalid Tile Type In Generation!!");
    }

    public GameObject GeneratePlayer()
    {
        return Instantiate(tileset.WhiteChessPieces.GetValue(ChessPiece.QUEEN), transform);
    }

    public DungeonEnemy GenerateEnemy(Vector2Int pos, Encounter encounter, ChessPiece type)
    {
        GameObject tempPiece = Instantiate(tileset.BlackChessPieces.GetValue(type), transform);
        return new DungeonEnemy(pos, encounter, type, tempPiece);
    }
}
