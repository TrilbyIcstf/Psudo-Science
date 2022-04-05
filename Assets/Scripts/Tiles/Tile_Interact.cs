using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class representing a singular tile within the game board. Holds basic information such as tile color and position
/// </summary>
public class Tile_Interact : MonoBehaviour
{
    // The tile's current position in the game world
    private Vector3 posit;

    // Refrence to the board controller
    private Board_Controller boardController;

    // Dummy object holders used to mirror the tile as it wraps around the game board
    public GameObject ghostTile;
    private GameObject ghostTileHolder;
    private Ghost_Tile ghostTileScript;

    public GameObject fallingTile;

    // The color of the tile, as well as that color's position in the sprite arrays of other objects
    public string color;
    public int colorPos;

    // The position of the tile on the 8x8 game board
    private int posX;
    private int posY;

    // Variables used to store how far the player has moved the tile while clicking on it
    private float xOffset = 0;
    private float yOffset = 0;

    // Variables used to jump the tile to the other side of the board if the player attempts to drag it out of bounds
    private float xWrap = 0;
    private float yWrap = 0;

    // Checks if the tile has been visited during a chain check
    private bool visited = false;

    // Holds the number of other tiles with the same color this tile is connected to
    public int chain = 1;

    // Start is called before the first frame update
    void Start()
    {
        posit = transform.position;
        boardController = GameObject.FindGameObjectWithTag("BoardController").GetComponent<Board_Controller>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        boardController.ClickTile(posX, posY);
    }

    public void MoveHorizontal(float dist)
    {
        if (posit.x + dist + xWrap > boardController.GetRightBound() && dist > xOffset)
        {
            if (ghostTileHolder != null)
            {
                Destroy(ghostTileHolder);
            }

            ghostTileHolder = Instantiate(ghostTile, new Vector3(posit.x + dist + xWrap, posit.y, posit.z), Quaternion.identity);
            ghostTileScript = ghostTileHolder.GetComponent<Ghost_Tile>();
            ghostTileScript.Generate(dist, colorPos);

            xWrap -= boardController.GetXGap() * 8;
        }
        else if (posit.x + dist + xWrap < boardController.GetLeftBound() && dist < xOffset)
        {
            if (ghostTileHolder != null)
            {
                Destroy(ghostTileHolder);
            }

            ghostTileHolder = Instantiate(ghostTile, new Vector3(posit.x + dist + xWrap, posit.y, posit.z), Quaternion.identity);
            ghostTileScript = ghostTileHolder.GetComponent<Ghost_Tile>();
            ghostTileScript.Generate(dist, colorPos);

            xWrap += boardController.GetXGap() * 8;
        }

        Vector3 newPos = new Vector3(posit.x + dist + xWrap, posit.y, posit.z);
        transform.position = newPos;

        if (ghostTileHolder != null && ghostTileScript!= null)
        {
            if (posit.x + dist + xWrap - boardController.GetLeftBound() > 0.5f && posit.x + dist + xWrap - boardController.GetRightBound() < -0.5f)
            {
                Destroy(ghostTileHolder);
            }
            else
            {
                ghostTileScript.MoveHorizontal(dist);
            }
        }

        xOffset = dist;
    }

    public void MoveVertical(float dist)
    {
        if (posit.y + dist + yWrap > boardController.GetTopBound() && dist > yOffset)
        {
            if (ghostTileHolder != null)
            {
                Destroy(ghostTileHolder);
            }

            ghostTileHolder = Instantiate(ghostTile, new Vector3(posit.x, posit.y + dist + yWrap, posit.z), Quaternion.identity);
            ghostTileScript = ghostTileHolder.GetComponent<Ghost_Tile>();
            ghostTileScript.Generate(dist, colorPos);

            yWrap -= boardController.GetYGap() * 8;
        }
        else if (posit.y + dist + yWrap < boardController.GetBottomBound() && dist < yOffset)
        {
            if (ghostTileHolder != null)
            {
                Destroy(ghostTileHolder);
            }

            ghostTileHolder = Instantiate(ghostTile, new Vector3(posit.x, posit.y + dist + yWrap, posit.z), Quaternion.identity);
            ghostTileScript = ghostTileHolder.GetComponent<Ghost_Tile>();
            ghostTileScript.Generate(dist, colorPos);

            yWrap += boardController.GetYGap() * 8;
        }

        Vector3 newPos = new Vector3(posit.x, posit.y + dist + yWrap, posit.z);
        transform.position = newPos;

        if (ghostTileHolder != null && ghostTileScript != null)
        {
            if (posit.y + dist + yWrap - boardController.GetBottomBound() > 0.35f && posit.y + dist + yWrap - boardController.GetTopBound() < -0.35f)
            {
                Destroy(ghostTileHolder);
            }
            else
            {
                ghostTileScript.MoveVertical(dist);
            }
        }

        yOffset = dist;
    }

    public void TurnFalling(int yMoved, float yGap)
    {
        float travelDist = yMoved * yGap;

        GameObject tempFallingTile = Instantiate(fallingTile, transform.position, Quaternion.identity);
        Falling_Tile fallingScript = tempFallingTile.GetComponent<Falling_Tile>();

        fallingScript.Generate(colorPos, posX, posY + yMoved, transform.position.y - travelDist);

        Destroy(gameObject);
    }

    public void MoveReset()
    {
        transform.position = posit;
    }

    public void SetPlacement(int x, int y)
    {
        posX = x;
        posY = y;
    }

    public void PositionUpdate()
    {
        posit = transform.position;
        
        if (ghostTileHolder != null)
        {
            Destroy(ghostTileHolder);
        }

        xWrap = 0;
        yWrap = 0;
    }

    public int GetPosX()
    {
        return posX;
    }
    public void SetPosX(int x)
    {
        posX = x;
    }

    public int GetPosY()
    {
        return posY;
    }
    public void SetPosY(int y)
    {
        posY = y;
    }

    public string GetColor()
    {
        return color;
    }
    public void SetColor(string inp)
    {
        color = inp;
    }

    public bool GetVisited()
    {
        return visited;
    }
    public void SetVisited(bool inp)
    {
        visited = inp;
    }

    public int GetChain()
    {
        return chain;
    }
    public void SetChain(int inp)
    {
        chain = inp;
    }
    public void AddChain(int inp)
    {
        chain += inp;
    }
}
