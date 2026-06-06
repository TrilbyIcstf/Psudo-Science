using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for controlling flow of gameplay during combat. Plays as a tile matching game on an 8x8 board with 4+ chains
/// needed for a match.
/// </summary>
public class Board_Controller : MonoBehaviour
{
    // Grabbing the main camera
    private Camera mainCam;

    // The position of each of the four edges of the board
    private static float leftMost = -2.45f;
    private static float rightMost = 2.45f;
    private static float topMost = 0.5f;
    private static float bottomMost = -3.7f;

    // Size of the board
    public int boardWidth = 8;
    public int boardHeight = 8;

    // An array of each tile on the board. X = Column, Y = Row
    private GameObject[,] board;

    // The center spot of the board
    public Transform centerPos;

    // The position of the top-left most tile, used for positioning objects on the board
    private Vector3 startingPos = new Vector3(leftMost, topMost, 0.0f);

    // Parent object for the tiles
    public GameObject tileDad;

    // Prefab for tile burst particle effects
    public GameObject tileBurst;

    // The distances between each tile
    private float xGap = 0.72f;
    private float yGap = 0.68f;

    // [0         , 1           , 2           , 3         , 4          , 5         , 6          ]
    // [Blue piece, Orange piece, Purple piece, Pink piece, Green piece, Grey piece, Black piece]
    // Prefabs of each of the tile types
    public GameObject[] pieces = new GameObject[7];
    private TColor[] pieceColors = new TColor[] { TColor.BLUE, TColor.ORANGE, TColor.PURPLE, TColor.PINK, TColor.GREEN, TColor.GREY, TColor.BLACK };

    // The amount of each color collected in a match
    private float[] matchCounter = new float[7];

    public GameObject fallingTile;

    // Checks if the mouse is currently clicking on a tile
    private bool mouseDown = false;

    // Locks to prevent to player from interacting with the board
    private bool mouseLock = false;
    private int fallingTileLock = 0;

    // Number of matches made in a row
    private int matchCombo = 0;

    // Position of the mouse when it clicked on the tile
    private Vector3 mousePos;

    // Determines if the mouse is attempting to move pieces verticaly or horizontaly
    private bool movingHorizontal = false;
    private bool movingVertical = false;

    // Which tile is currently being clicked [column, row]
    private int[] activeTile = new int[2];

    // Random numbers used to determine the fall speed rng of each row
    private float[] rowSpeedBonus;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.combat.SetBoard(this);
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        board = new GameObject[boardWidth, boardHeight];
        startingPos = new Vector3(centerPos.position.x - (((boardWidth / 2) - 0.5f) * xGap), centerPos.position.y + (((boardHeight / 2) - 0.5f) * yGap), 0.0f);

        // Loops through each spot in the board array
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                // Uses a random number to determine which piece should be chosen
                int randomPiece = Random.Range(0, 7);

                // Creates a new instance of the chosen piece
                GameObject newPiece = Instantiate(pieces[randomPiece], new Vector3(startingPos.x + (xGap * i), startingPos.y - (yGap * j), startingPos.z), Quaternion.identity);
                newPiece.transform.parent = tileDad.transform;

                // Assigns that piece it's position on the board
                Tile_Interact tileScript = newPiece.GetComponent(typeof(Tile_Interact)) as Tile_Interact;
                tileScript.SetPlacement(i, j);

                // Adds piece to board array
                board[i, j] = newPiece;
            }
        }

        // Bool used to check that there are no matches generated at the start
        bool checkMatch = false;

        do
        {
            checkMatch = false;
            ChainReset();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    // Having each tile check how many tiles of the same color it is connected to
                    CheckChainStart(i, j);

                    // If the checked tile is part of a 4+ chain, randomizes the tile and restarts the for loop
                    if (board[i, j].GetComponent<Tile_Interact>().GetChain() >= 4)
                    {
                        checkMatch = true;
                        Destroy(board[i, j]);

                        // Uses a random number to determine which piece should be chosen
                        int randomPiece = Random.Range(0, 7);

                        // Creates a new instance of the chosen piece
                        GameObject newPiece = Instantiate(pieces[randomPiece], new Vector3(startingPos.x + (xGap * i), startingPos.y - (yGap * j), startingPos.z), Quaternion.identity);
                        newPiece.transform.parent = tileDad.transform;

                        // Assigns that piece it's position on the board
                        Tile_Interact tileScript = newPiece.GetComponent(typeof(Tile_Interact)) as Tile_Interact;
                        tileScript.SetPlacement(i, j);

                        // Adds piece to board array
                        board[i, j] = newPiece;

                        break;
                    }
                }
                if (checkMatch)
                {
                    break;
                }
            }
        } while (checkMatch);

        rowSpeedBonus = new float[boardWidth];
        for (int i = 0; i < boardWidth; i++)
        {
            rowSpeedBonus[i] = Random.Range(0.9f, 1.4f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Checking if mouse has been released after being pressed
        if (mouseDown && !Input.GetMouseButton(0))
        {
            // Resets variables for moving pieces
            mouseDown = false;

            // Holds the total distance the mouse has moved while holding a tile, and how many spaces on the board that translates to
            float totDist = 0;
            float spacesMoved = 0;

            if (movingHorizontal)
            {
                // Assigns total distance based on mouse's start position vs. current position, then divides by distance
                // between tiles to determine how many total spaces have been moved
                totDist = mainCam.ScreenToWorldPoint(Input.mousePosition).x - mainCam.ScreenToWorldPoint(mousePos).x;
                spacesMoved = (totDist / xGap) % 8;
                spacesMoved = Mathf.Round(spacesMoved);

                // For each space moved, shifts the row/column by 1
                // First, it reassigns each tile to the correct part of the board array
                for (int i = 1; i <= Mathf.Abs(spacesMoved); i++)
                {
                    if (spacesMoved > 0)
                    {
                        RowRight(activeTile[1]);
                    }
                    else
                    {
                        RowLeft(activeTile[1]);
                    }
                }

                // Will then assign the new position to each tile object, and update their transform position accordingly
                for (int i = 0; i < 8; i++)
                {
                    int tilePos = board[i, activeTile[1]].GetComponent<Tile_Interact>().GetPosX();

                    // Statements checking if the tile has wrapped around the board
                    tilePos = (tilePos + (int) spacesMoved) % 8;
                    if (tilePos < 0)
                    {
                        tilePos = 8 + tilePos;
                    }

                    board[i, activeTile[1]].GetComponent<Tile_Interact>().SetPosX(tilePos);

                    board[i, activeTile[1]].transform.position = new Vector3(startingPos.x + (xGap * board[i, activeTile[1]].GetComponent<Tile_Interact>().GetPosX()), board[i, activeTile[1]].transform.position.y, startingPos.z);

                    board[i, activeTile[1]].GetComponent<Tile_Interact>().PositionUpdate();
                }
            }
            else if (movingVertical)
            {
                // Assigns total distance based on mouse's start position vs. current position, then divides by distance
                // between tiles to determine how many total spaces have been moved
                totDist = mainCam.ScreenToWorldPoint(Input.mousePosition).y - mainCam.ScreenToWorldPoint(mousePos).y;
                spacesMoved = (totDist / yGap) % 8;
                spacesMoved = Mathf.Round(spacesMoved);

                // For each space moved, shifts the row/column by 1
                // First, it reassigns each tile to the correct part of the board array
                for (int i = 1; i <= Mathf.Abs(spacesMoved); i++)
                {
                    if (spacesMoved > 0)
                    {
                        ColumnUp(activeTile[0]);
                    }
                    else
                    {
                        ColumnDown(activeTile[0]);
                    }
                }

                // Will then assign the new position to each tile object, and update their transform position accordingly
                for (int i = 0; i < 8; i++)
                {
                    int tilePos = board[activeTile[0], i].GetComponent<Tile_Interact>().GetPosY();

                    // Statements checking if the tile has wrapped around the board
                    tilePos = (tilePos - (int)spacesMoved) % 8;
                    if (tilePos < 0)
                    {
                        tilePos = 8 + tilePos;
                    }

                    board[activeTile[0], i].GetComponent<Tile_Interact>().SetPosY(tilePos);

                    board[activeTile[0], i].transform.position = new Vector3(board[activeTile[0], i].transform.position.x, startingPos.y - (yGap * board[activeTile[0], i].GetComponent<Tile_Interact>().GetPosY()), startingPos.z);

                    board[activeTile[0], i].GetComponent<Tile_Interact>().PositionUpdate();
                }
            }

            movingHorizontal = false;
            movingVertical = false;

            if (spacesMoved != 0)
            {
                // Checking if any chains of 4+ have been created, and resolving them accordingly
                ResolveChains();
            }
        }

        // Checks if mouse is being pressed on a tile
        if (mouseDown)
        {
            // These check if the mouse has left the "dead zone" after initially clicking on a tile to determine if
            // the tile should be moved horizontaly or verticaly
            if (!movingHorizontal && Mathf.Abs(mousePos.x - Input.mousePosition.x) / Screen.width >= 0.01f && !movingVertical)
            {
                movingHorizontal = true;
            }
            else if (!movingVertical && Mathf.Abs(mousePos.y - Input.mousePosition.y) / Screen.height >= 0.01f && !movingHorizontal)
            {
                movingVertical = true;
            }

            // Has each tile in a row or column move a distance based on how far the mouse has been moved
            if (movingHorizontal)
            {
                float startX = mainCam.ScreenToWorldPoint(mousePos).x;
                float endX = mainCam.ScreenToWorldPoint(Input.mousePosition).x;

                for (int i = 0; i < 8; i++)
                {
                    Tile_Interact tileScript = board[i, activeTile[1]].GetComponent<Tile_Interact>();
                    tileScript.MoveHorizontal(endX - startX);
                }
            }
            else if (movingVertical)
            {
                float startY = mainCam.ScreenToWorldPoint(mousePos).y;
                float endY = mainCam.ScreenToWorldPoint(Input.mousePosition).y;

                for (int i = 0; i < 8; i++)
                {
                    Tile_Interact tileScript = board[activeTile[0], i].GetComponent<Tile_Interact>();
                    tileScript.MoveVertical(endY - startY);
                }
            }
        }
    }

    // Tells the script a tile has been clicked, and assigns the active tile
    public void ClickTile(int posX, int posY)
    {
        // Prevents a tile from being clicked if their is a lock on mouse controls
        if (!mouseLock && !Combat_Commands.GetMoveQueueLock())
        {
            mouseDown = true;
            mousePos = Input.mousePosition;

            activeTile[0] = posX;
            activeTile[1] = posY;
        }
        
    }

    // Shifts all tiles in a row of the board array right by 1
    private void RowRight(int row)
    {
        GameObject dummy;
        dummy = board[7, row];
        for (int i = 7; i >=1; i--)
        {
            board[i, row] = board[i - 1, row];
        }
        board[0, row] = dummy;
    }
    // Shifts all tiles in a row of the board array left by 1
    private void RowLeft(int row)
    {
        GameObject dummy;
        dummy = board[0, row];
        for (int i = 0; i <= 6; i++)
        {
            board[i, row] = board[i + 1, row];
        }
        board[7, row] = dummy;
    }
    // Shifts all tiles in a column of the board array up by 1
    private void ColumnUp(int column)
    {
        GameObject dummy;
        dummy = board[column, 0];
        for (int i = 0; i <= 6; i++)
        {
            board[column, i] = board[column, i + 1];
        }
        board[column, 7] = dummy;
    }
    // Shifts all tiles in a column of the board array down by 1
    private void ColumnDown(int column)
    {
        GameObject dummy;
        dummy = board[column, 7];
        for (int i = 7; i >= 1; i--)
        {
            board[column, i] = board[column, i - 1];
        }
        board[column, 0] = dummy;
    }

    // Will check how many tiles of the same color a tile is connected to, and assign that value as its "chain"
    private int CheckChainStart(int x, int y)
    {
        // Making sure the tile isn't null
        if (board[x, y] != null)
        {
            // Checks if the tile has been previously visited as part of a chain
            if (!board[x, y].GetComponent<Tile_Interact>().GetVisited())
            {
                // Sets visited to true
                board[x, y].GetComponent<Tile_Interact>().SetVisited(true);

                // For each unvisited connected tile (up, down, left, right) checks if it is the same color, then starts a
                // check chain on it and adds its chain value to this after it finishes
                if (CheckMatch(x + 1, y, board[x, y].GetComponent<Tile_Interact>().GetColor()) && 
                    !board[x + 1, y].GetComponent<Tile_Interact>().GetVisited())
                {
                    board[x, y].GetComponent<Tile_Interact>().AddChain(CheckChain(x + 1, y));
                }
                if (CheckMatch(x, y + 1, board[x, y].GetComponent<Tile_Interact>().GetColor()) &&
                    !board[x, y + 1].GetComponent<Tile_Interact>().GetVisited())
                {
                    board[x, y].GetComponent<Tile_Interact>().AddChain(CheckChain(x, y + 1));
                }
                if (CheckMatch(x - 1, y, board[x, y].GetComponent<Tile_Interact>().GetColor()) &&
                    !board[x - 1, y].GetComponent<Tile_Interact>().GetVisited())
                {
                    board[x, y].GetComponent<Tile_Interact>().AddChain(CheckChain(x - 1, y));
                }
                if (CheckMatch(x, y - 1, board[x, y].GetComponent<Tile_Interact>().GetColor()) &&
                    !board[x, y - 1].GetComponent<Tile_Interact>().GetVisited())
                {
                    board[x, y].GetComponent<Tile_Interact>().AddChain(CheckChain(x, y - 1));
                }

                // After checking each tile, will send its chain value to all connected tiles so that they match
                ChainPulse(x, y, board[x, y].GetComponent<Tile_Interact>().GetChain());

                // Returns the chain value for testing purposes
                return board[x, y].GetComponent<Tile_Interact>().GetChain();
            }
        }
        return 0;
    }

    // Recursive function called by CheckChainStart, only difference is that this won't call ChainPulse at the end
    private int CheckChain(int x, int y)
    {
        // Making sure the tile isn't null
        if (board[x, y] != null)
        {
            // Checks if the tile has been previously visited as part of a chain
            if (!board[x, y].GetComponent<Tile_Interact>().GetVisited())
            {
                // Sets visited to true
                board[x, y].GetComponent<Tile_Interact>().SetVisited(true);

                // For each unvisited connected tile (up, down, left, right) checks if it is the same color, then starts a
                // check chain on it and adds its chain value to this after it finishes
                if (CheckMatch(x + 1, y, board[x, y].GetComponent<Tile_Interact>().GetColor()) &&
                    !board[x + 1, y].GetComponent<Tile_Interact>().GetVisited())
                {
                    board[x, y].GetComponent<Tile_Interact>().AddChain(CheckChain(x + 1, y));
                }
                if (CheckMatch(x, y + 1, board[x, y].GetComponent<Tile_Interact>().GetColor()) &&
                    !board[x, y + 1].GetComponent<Tile_Interact>().GetVisited())
                {
                    board[x, y].GetComponent<Tile_Interact>().AddChain(CheckChain(x, y + 1));
                }
                if (CheckMatch(x - 1, y, board[x, y].GetComponent<Tile_Interact>().GetColor()) &&
                    !board[x - 1, y].GetComponent<Tile_Interact>().GetVisited())
                {
                    board[x, y].GetComponent<Tile_Interact>().AddChain(CheckChain(x - 1, y));
                }
                if (CheckMatch(x, y - 1, board[x, y].GetComponent<Tile_Interact>().GetColor()) &&
                    !board[x, y - 1].GetComponent<Tile_Interact>().GetVisited())
                {
                    board[x, y].GetComponent<Tile_Interact>().AddChain(CheckChain(x, y - 1));
                }

                return board[x, y].GetComponent<Tile_Interact>().GetChain();
            }
        }
        return 0;
    }

    // For each connected tile of the same color with a lower chain value, this will recursively set its chain value to match
    // and continue checking for more tiles in the chain
    private void ChainPulse(int x, int y, int chain)
    {
        // Making sure the tile isn't null
        if (board[x, y] != null)
        {
            // Sets the tile's chain to equal the passed in value (will always be >=)
            board[x, y].GetComponent<Tile_Interact>().SetChain(chain);

            // For each connected tile (up, down, left, right) checks if it is the same color and has a lower chain value,
            // and then visits it to increase the chain value and continue searching for connected tiles
            if (CheckMatch(x + 1, y, board[x, y].GetComponent<Tile_Interact>().GetColor()) &&
                board[x + 1, y].GetComponent<Tile_Interact>().GetChain() < chain)
            {
                ChainPulse(x + 1, y, chain);
            }
            if (CheckMatch(x, y + 1, board[x, y].GetComponent<Tile_Interact>().GetColor()) &&
                board[x, y + 1].GetComponent<Tile_Interact>().GetChain() < chain)
            {
                ChainPulse(x, y + 1, chain);
            }
            if (CheckMatch(x - 1, y, board[x, y].GetComponent<Tile_Interact>().GetColor()) &&
                board[x - 1, y].GetComponent<Tile_Interact>().GetChain() < chain)
            {
                ChainPulse(x - 1, y, chain);
            }
            if (CheckMatch(x, y - 1, board[x, y].GetComponent<Tile_Interact>().GetColor()) &&
                board[x, y - 1].GetComponent<Tile_Interact>().GetChain() < chain)
            {
                ChainPulse(x, y - 1, chain);
            }
        }
    }

    // Starts a chain check on each tile, then finds each tile with a chain of 4+ and destroys it to resolve its color's
    // appropriate effect, then populates the board with new tiles
    private void ResolveChains()
    {
        // Reseting the chain value on each tile
        ChainReset();

        // Setting the chain value of each tile
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                CheckChainStart(i, j);
            }
        }

        // Checking if a 4+ chain has been found
        bool matched = false;

        // Checking the chain value of each tile
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                // If a 4+ chain is found, will flag that a match was made, destroy the tile, and assign points
                if (board[i, j] != null & board[i, j].GetComponent<Tile_Interact>().GetChain() >= 4)
                {
                    mouseLock = true;
                    matched = true;

                    TColor tempColor = board[i, j].GetComponent<Tile_Interact>().GetColor();

                    float pointVal = board[i, j].GetComponent<Tile_Interact>().GetChain() * (1.0f + (0.5f * matchCombo));

                    // Determines number of points added to color, based on length of match chain
                    matchCounter[(int)tempColor] += pointVal;

                    // Spawning particles for the destroyed tile
                    GameObject TempParticles = Instantiate(tileBurst, board[i, j].transform.position, Quaternion.identity);
                    TempParticles.GetComponent<Tile_Burst>().Activate(tempColor, 3, pointVal);

                    Destroy(board[i, j]);
                    board[i, j] = null;
                }
            }
        }

        // Assigns points, then calls a function which will populate the board with new tiles
        if (matched)
        {
            foreach (TColor c in pieceColors)
            {
                if (matchCounter[(int)c] > 0)
                {
                    GainPoints(c, matchCounter[(int)c]);
                }
            }

            matchCounter = new float[7];

            matchCombo++;
            TileFall();
        }
        else
        {
            StartCoroutine(GameManager.instance.combat.WaitToStartQueue());

            matchCombo = 0;
            mouseLock = false;
        }
    }

    // Checks for null tiles, and will drop tiles down to fill those spots, as well as spawn new ones in
    private void TileFall()
    {
        for (int i = 0; i < 8; i++)
        {
            int blankSpots = 0;
            for (int j = 7; j >= 0; j--)
            {
                if (board[i, j] == null)
                {
                    blankSpots++;
                }
                else if (blankSpots > 0)
                {
                    board[i, j].GetComponent<Tile_Interact>().TurnFalling(blankSpots, yGap);
                    fallingTileLock++;
                }
            }

            for (int j = 1; j <= blankSpots; j++)
            {
                Vector3 spawnPos = new Vector3(startingPos.x + (xGap * i), startingPos.y + (yGap * j), startingPos.z);

                GameObject tempFallingTile = Instantiate(fallingTile, spawnPos, Quaternion.identity);
                tempFallingTile.transform.parent = tileDad.transform;
                Falling_Tile fallingScript = tempFallingTile.GetComponent<Falling_Tile>();

                int randomPiece = Random.Range(0, 7);

                fallingScript.Generate(randomPiece, i, blankSpots - j, startingPos.y - (yGap * (blankSpots - j)), startingPos.y + 0.5f);

                fallingTileLock++;
            }
        }
    }

    public void FallenTileCheck(int posX, int posY, int colorPos)
    {
        // Creates a new instance of the chosen piece
        GameObject newPiece = Instantiate(pieces[colorPos], new Vector3(startingPos.x + (xGap * posX), startingPos.y - (yGap * posY), startingPos.z), Quaternion.identity);
        newPiece.transform.parent = tileDad.transform;

        // Assigns that piece it's position on the board
        Tile_Interact tileScript = newPiece.GetComponent(typeof(Tile_Interact)) as Tile_Interact;
        tileScript.SetPlacement(posX, posY);

        // Adds piece to board array
        board[posX, posY] = newPiece;

        fallingTileLock--;

        if (fallingTileLock == 0)
        {
            ResolveChains();
        }
    }

    // Goes to each tile on the board and undoes the effects of CheckChain
    private void ChainReset()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] != null)
                {
                    board[i, j].GetComponent<Tile_Interact>().SetChain(1);
                    board[i, j].GetComponent<Tile_Interact>().SetVisited(false);
                }
            }
        }
    }

    // Checks if a specific tile on the board exists and matches the passed in color
    private bool CheckMatch(int x, int y, TColor color)
    {
        if (x >= 0 && x <= 7 && y >= 0 && y <= 7 && board[x, y] != null &&
            board[x, y].GetComponent<Tile_Interact>().GetColor() == color)
        {
            return true;
        }
        return false;
    }

    // Assigns points based on both the effects of the passed in color, and the amount of that color matched
    private void GainPoints(TColor _color, float chain)
    {
        switch (_color)
        {
            case TColor.BLUE:
                GameManager.instance.combat.energy.GainEnergy(chain, _color);
                break;
            case TColor.ORANGE:
                GameManager.instance.combat.energy.GainEnergy(chain, _color);
                break;
            case TColor.PINK:
                GameManager.instance.combat.energy.GainEnergy(chain, _color);
                break;
            case TColor.PURPLE:
                GameManager.instance.combat.energy.GainEnergy(chain, _color);
                break;
            case TColor.GREEN:
                int healAmount = Mathf.FloorToInt(chain / 4);
                GameManager.instance.party.PartyHeal(healAmount);
                break;
            case TColor.GREY:
                GameManager.instance.combat.energy.ExpoPowerUp((chain / 100) + 1, TColor.BLUE);
                GameManager.instance.combat.energy.ExpoPowerUp((chain / 100) + 1, TColor.ORANGE);
                GameManager.instance.combat.energy.ExpoPowerUp((chain / 100) + 1, TColor.PINK);
                GameManager.instance.combat.energy.ExpoPowerUp((chain / 100) + 1, TColor.PURPLE);
                break;
            case TColor.BLACK:
                GameManager.instance.combat.energy.GainEnergy(chain / 5, TColor.BLUE);
                GameManager.instance.combat.energy.GainEnergy(chain / 5, TColor.ORANGE);
                GameManager.instance.combat.energy.GainEnergy(chain / 5, TColor.PINK);
                GameManager.instance.combat.energy.GainEnergy(chain / 5, TColor.PURPLE);
                break;
            default:
                break;
        }
    }

    // Getters for the on screen position of the edges of the board
    public float GetLeftBound()
    {
        return centerPos.position.x - (((boardWidth / 2) - 0.5f) * xGap);
    }

    public float GetRightBound()
    {
        return centerPos.position.x + (((boardWidth / 2) - 0.5f) * xGap);
    }

    public float GetTopBound()
    {
        return centerPos.position.y + (((boardHeight / 2) - 0.5f) * yGap);
    }

    public float GetBottomBound()
    {
        return centerPos.position.y - (((boardHeight / 2) - 0.5f) * yGap);
    }

    // Getters for the amount of space between each tile
    public float GetXGap()
    {
        return xGap;
    }

    public float GetYGap()
    {
        return yGap;
    }

    public float GetRowSpeedBonus(int _rowNum)
    {
        return rowSpeedBonus[_rowNum];
    }

    public int MatchCombo { get => matchCombo; set => matchCombo = value; }
}
