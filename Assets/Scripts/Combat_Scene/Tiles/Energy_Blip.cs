using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy_Blip : MonoBehaviour
{
    // Whether the blip has been initialized
    private bool activated = false;

    // Color of the tile which created the blip
    private TColor blipColor;

    // The player's number the blip is going to
    private int playerNum;

    // The location the blip will travel to
    private Transform destination;

    // The direction to move in
    private Vector2 direction;

    // A randomized multiplier for the blip's move speed
    private float speedMultiplier = 1.0f;

    // The blip's current velocity
    private float currentSpeed = 0.0f;

    // Decides when the tile should move towards its bar
    private bool startApproach = false;

    // The value in points the blip is carrying
    private float pointValue = 0.0f;

    public void Activate(TColor _tileColor, int _player, Transform _goal, float _points)
    {
        blipColor = _tileColor;
        GetComponent<SpriteRenderer>().color = ColorVals.GetColorVal(blipColor);

        playerNum = _player;

        destination = _goal;

        pointValue = _points;

        float randRad = Random.Range(0, 2 * Mathf.PI);

        direction = new Vector2(Mathf.Cos(randRad), Mathf.Sin(randRad));

        speedMultiplier = Random.Range(0.8f, 1.4f);

        currentSpeed = 0.04f + Random.Range(0, 0.005f);

        StartCoroutine(ApproachCountdown());

        activated = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activated)
        {
            if (!startApproach)
            {
                Vector3 tempPos = transform.position;

                currentSpeed = Mathf.Max(0, currentSpeed - 0.0018f);
                tempPos = new Vector3(tempPos.x + (direction.x * currentSpeed), tempPos.y + (direction.y * currentSpeed), tempPos.z);

                transform.position = tempPos;
            } 
            else
            {
                Vector3 tempPos = transform.position;

                currentSpeed = Mathf.Min(currentSpeed + (0.004f * speedMultiplier), 0.9f);
                tempPos = new Vector3(tempPos.x + (direction.x * currentSpeed), tempPos.y + (direction.y * currentSpeed), tempPos.z);

                if (Mathf.Sign(tempPos.x - destination.position.x) != Mathf.Sign(transform.position.x - destination.position.x))
                {
                    if (blipColor == TColor.BLUE || blipColor == TColor.ORANGE || blipColor == TColor.PINK || blipColor == TColor.PURPLE)
                    {
                        Combat_UI_Commands.SendEnergy(pointValue, (int)blipColor + 1);
                    }
                    else if (blipColor == TColor.GREEN)
                    {
                        Combat_UI_Commands.SendHealth(pointValue, playerNum);
                    }
                    else if (blipColor == TColor.BLACK)
                    {
                        Combat_UI_Commands.SendEnergy(pointValue, PlayerNum);
                    }

                    Destroy(gameObject);
                }

                transform.position = tempPos;
            }
        }
    }

    private IEnumerator ApproachCountdown()
    {
        yield return new WaitForSeconds(0.6f);

        startApproach = true;
        currentSpeed = 0;
        direction = new Vector2(destination.position.x - transform.position.x, destination.position.y - transform.position.y).normalized;
    }

    // Get/Set
    public TColor BlipColor { get => blipColor; }
    public int PlayerNum { get => playerNum; }
}
