using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Tile : MonoBehaviour
{
    private Vector3 posit;
    private float offset = 0;

    public Sprite[] sprites = new Sprite[7];

    public void Generate(float off, int spriteNum)
    {
        posit = transform.position;
        offset = off;
        GetComponent<SpriteRenderer>().sprite = sprites[spriteNum];
    }

    public void MoveHorizontal(float dist)
    {
        Vector3 newPos = new Vector3(posit.x + (dist - offset), posit.y, posit.z);
        transform.position = newPos;
    }

    public void MoveVertical(float dist)
    {
        Vector3 newPos = new Vector3(posit.x, posit.y + (dist - offset), posit.z);
        transform.position = newPos;
    }
}
