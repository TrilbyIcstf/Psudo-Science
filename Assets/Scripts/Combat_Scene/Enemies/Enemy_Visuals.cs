using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Visuals : MonoBehaviour
{
    // The in game image element of the enemy
    private Image enemyImage;
    private RectTransform imageTransform;

    // The position element of the enemy's body parts
    public RectTransform center;
    public RectTransform head;
    public RectTransform body;
    public RectTransform legs;
    public RectTransform hands;

    public void Startup(Enemy_Information enemyBase)
    {
        enemyImage = GetComponent<Image>();

        enemyImage.sprite = enemyBase.EnemySprite;

        imageTransform = GetComponent<RectTransform>();
        imageTransform.sizeDelta = enemyBase.SpriteSize;
        gameObject.GetComponent<BoxCollider2D>().size = imageTransform.sizeDelta;
    }

    public Vector2 GetCenter()
    {
        return center.position;
    }

    public Vector2 GetBodyPosition(BodyPart target)
    {
        switch(target)
        {
            case BodyPart.HEAD:
                if (head != null)
                {
                    return head.position;
                }
                break;
            case BodyPart.LEGS:
                if (legs != null)
                {
                    return legs.position;
                }
                break;
            case BodyPart.HANDS:
                if (hands != null)
                {
                    return hands.position;
                }
                break;
            default:
                break;
        }
        return body.position;
    }
}
