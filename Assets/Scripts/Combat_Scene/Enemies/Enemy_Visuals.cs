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
    [SerializeField]
    private Enemy_Health_UI healthBar;

    // Link to the enemy's turn number
    [SerializeField]
    private Enemy_Turn_UI turnNumber;

    // The position element of the enemy's body parts
    [SerializeField]
    private RectTransform center;
    [SerializeField]
    private RectTransform head;
    [SerializeField]
    private RectTransform body;
    [SerializeField]
    private RectTransform legs;
    [SerializeField]
    private RectTransform hands;

    // Visuals used for the death animation
    public GameObject deathMask;

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
        SetTurnNumberHeight(enemyBase.HealthBarHeight);
    }

    public void SetBehavior(Behavior_Dad behavior)
    {
        turnNumber.SetTurnNumber(behavior.BaseSpeed);
    }

    public void PlayAnimation(EnemyAnimation ea)
    {
        animController.PlayAnimation(ea);
    }

    public void PlayAnimationRotated(EnemyAnimation ea, float rotation)
    {
        animController.PlayAnimationRotated(ea, rotation);
    }

    public void PlayAnimationColor(EnemyAnimation ea, Color color)
    {
        animController.PlayAnimationColor(ea, color);
    }

    public IEnumerator PlayDeathAnimation(GameManager.CallbackFunction callback)
    {
        GameObject DeathOverlay = Instantiate(deathMask, spriteObject.transform);
        yield return new WaitForSeconds(0.0f);
        callback();
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

    public void SetTurnNumber(int val)
    {
        turnNumber.SetTurnNumber(val);
    }

    public void SetHealthBarHeight(float height)
    {
        healthBar.SetHeight(height);
    }

    public void SetTurnNumberHeight(float height)
    {
        turnNumber.SetHeight(height);
    }
}
