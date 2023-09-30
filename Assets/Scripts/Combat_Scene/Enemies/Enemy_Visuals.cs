using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Visuals : MonoBehaviour
{
    // The in game image element of the enemy
    public GameObject spriteObject;
    private Image enemyImage;
    private RectTransform imageTransform;

    // Link to the script that controls animations
    public Enemy_Animations animController;

    // Link to the enemy's health bar
    public Enemy_Health_UI healthBar;

    // The position element of the enemy's body parts
    public RectTransform center;
    public RectTransform head;
    public RectTransform body;
    public RectTransform legs;
    public RectTransform hands;

    public void Startup(Enemy_Information enemyBase)
    {
        enemyImage = spriteObject.GetComponent<Image>();
        imageTransform = spriteObject.GetComponent<RectTransform>();

        if (enemyBase.EnemySprite != null)
        {
            enemyImage.sprite = enemyBase.EnemySprite;

            imageTransform.sizeDelta = enemyBase.SpriteSize;
        }

        gameObject.GetComponent<BoxCollider2D>().size = imageTransform.sizeDelta;

        UpdateHealthBar(enemyBase.MaxHealth, enemyBase.MaxHealth);
        SetHealthBarHeight(enemyBase.HealthBarHeight);
    }

    public void PlayAnimation(EnemyAnimation ea)
    {
        animController.PlayAnimation(ea);
    }

    public void PlayAnimationRotated(EnemyAnimation ea, float rotation)
    {
        animController.PlayAnimationRotated(ea, rotation);
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

    public void SetHealthBarEnabled(bool enabled)
    {
        healthBar.SetEnabled(enabled);
    }

    public void SetHealthBarTimer(float time)
    {
        healthBar.SetTimer(time);
    }

    public void UpdateHealthBar(float current, float max)
    {
        healthBar.SetHealth(current, max);
    }

    public void SetHealthBarHeight(float height)
    {
        healthBar.SetHeight(height);
    }
}
