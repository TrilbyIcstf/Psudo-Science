using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Tile : MonoBehaviour
{
    private SpriteRenderer sr;

    public Vector3 posit;

    public Sprite[] sprites = new Sprite[7];
    private int colorPos;
    
    private int endX;
    private int endY;

    private float goalPos; // The position the falling tile must reach
    private float visablePos = 0; // The position at which the falling tile becomes visable

    private float fallSpeed = 0;
    private float fauxTimer = 1;
    private float speedMultiplier = 1;

    private float reboundSpeed = 0.005f;

    private bool generated = false;

    private bool startRebound = false; // Tile will slightly rebound upon hitting it intended position

    public void Generate(int spriteNum, int x, int y, float goal, float _visable)
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[spriteNum];
        colorPos = spriteNum;

        endX = x;
        endY = y;
        goalPos = goal;

        speedMultiplier = GameManager.instance.combat.board.GetRowSpeedBonus(x);
        speedMultiplier += 0.5f * (GameManager.instance.combat.board.MatchCombo - 1);

        fallSpeed = -0.0275f * Mathf.Pow(endY, 1/2) * (speedMultiplier) - (0.025f * (GameManager.instance.combat.board.MatchCombo - 1));

        posit = transform.position;
        generated = true;

        visablePos = _visable;

        if (posit.y > visablePos)
        {
            sr.enabled = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (generated)
        {
            float accel = -0.003f;

            if (posit.y > goalPos)
            {
                accel = (0.0017f);
                accel += 0.0003f * endY * speedMultiplier;
                accel += 0.0009f * (GameManager.instance.combat.board.MatchCombo - 1);
                accel = Mathf.Max(accel, 0);
            }

            fallSpeed += accel;

            if (posit.y - fallSpeed > goalPos - (0.07f) && !startRebound)
            {
                transform.position = new Vector3(posit.x, posit.y - fallSpeed, posit.z);
                posit = transform.position;
                if (posit.y <= visablePos)
                {
                    if (sr.enabled == false)
                    {
                        sr.enabled = true;
                    }
                } else
                {
                    sr.enabled = false;
                }
            }
            else
            {
                if (posit.y + reboundSpeed > goalPos)
                {
                    Board_Controller bc = GameManager.instance.combat.board;
                    bc.FallenTileCheck(endX, endY, colorPos);
                    Destroy(gameObject);
                }
                else if (!startRebound)
                {
                    startRebound = true;
                    transform.position = new Vector3(posit.x, goalPos - (0.07f), posit.z);
                    posit = transform.position;
                }
                else
                {
                    transform.position = new Vector3(posit.x, posit.y + reboundSpeed, posit.z);
                    posit = transform.position;
                    reboundSpeed += 0.005f;
                }
            }
        }
    }
}
