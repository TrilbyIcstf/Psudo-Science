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
    private float visablePos = 1.2f; // The position at which the falling tile becomes visable

    private float fallSpeed = 0;
    private float fauxTimer = 1;

    private bool generated = false;

    public void Generate(int spriteNum, int x, int y, float goal)
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[spriteNum];
        colorPos = spriteNum;

        endX = x;
        endY = y;
        goalPos = goal;

        fallSpeed = 0.0002f * endY;

        posit = transform.position;
        generated = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (generated)
        {
            float accel = (0.0015f - (0.000003f * fauxTimer));
            accel += 0.0003f * endY;
            accel = Mathf.Max(accel, 0);

            fallSpeed += accel;

            if (posit.y - fallSpeed > goalPos)
            {
                transform.position = new Vector3(posit.x, posit.y - fallSpeed, posit.z);
                posit = transform.position;
                if (sr.enabled == false && posit.y <= visablePos)
                {
                    sr.enabled = true;
                }
            }
            else
            {
                Board_Controller bc = GameObject.FindGameObjectWithTag("BoardController").GetComponent<Board_Controller>();
                bc.FallenTileCheck(endX, endY, colorPos);
                Destroy(gameObject);
            }
        }
    }
}
