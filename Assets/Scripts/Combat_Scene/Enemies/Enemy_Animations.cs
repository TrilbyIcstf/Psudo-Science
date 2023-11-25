using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Enemy_Animations : MonoBehaviour
{
    // Animation controller for the enemy
    private Animator animController;

    // The sprite object for the enemy
    public GameObject sprite;

    // The position to apply to the sprite
    public Vector2 spriteVector = new Vector2();

    private bool isMoving = false;

    // Information on the sprite object's color
    private Image spriteImage;
    private Color spriteColor;

    private Vector3 overlayColor = Vector3.zero;
    public float overlayMultiplier = 0;

    // Transforms for self and sprite
    private RectTransform holderPos;
    private RectTransform spritePos;

    // Start is called before the first frame update
    void Start()
    {
        animController = GetComponent<Animator>();

        holderPos = GetComponent<RectTransform>();
        spritePos = sprite.GetComponent<RectTransform>();

        spriteImage = sprite.GetComponent<Image>();
        spriteColor = spriteImage.color;
    }

    private void Update()
    {
        if (isMoving)
        {
            if (holderPos.rotation.z != 0)
            {
                float rotRad = holderPos.rotation.eulerAngles.z * Mathf.Deg2Rad;
                Debug.Log(rotRad);

                float xPos = (spriteVector.x * Mathf.Cos(rotRad)) - (spriteVector.y * Mathf.Sin(rotRad));
                float yPos = (spriteVector.x * Mathf.Sin(rotRad)) + (spriteVector.y * Mathf.Cos(rotRad));

                holderPos.anchoredPosition = new Vector2(xPos, yPos);
            }
            else
            {
                holderPos.anchoredPosition = spriteVector;
            }
        }

        if (overlayMultiplier > 0)
        {
            Color tempColor = spriteColor;
            tempColor.r = spriteColor.r + (overlayColor.x - spriteColor.r) * overlayMultiplier;
            tempColor.g = spriteColor.g + (overlayColor.y - spriteColor.g) * overlayMultiplier;
            tempColor.b = spriteColor.b + (overlayColor.z - spriteColor.b) * overlayMultiplier;
            spriteImage.color = tempColor;
            Debug.Log("COLOR");
        }
    }

    public void PlayAnimation(EnemyAnimation ea)
    {
        animController.SetTrigger(EnumMapping.EnemyAnimationMap(ea));
    }

    public void PlayAnimationRotated(EnemyAnimation ea, float rotation)
    {
        holderPos.rotation = Quaternion.Euler(0, 0, rotation);
        spritePos.rotation = Quaternion.Euler(0, 0, 0);
        animController.SetTrigger(EnumMapping.EnemyAnimationMap(ea));
    }

    public void PlayAnimationColor(EnemyAnimation ea, Vector3 color)
    {
        overlayColor = color;
        animController.SetTrigger(EnumMapping.EnemyAnimationMap(ea));
    }

    public void ResetRotation()
    {
        holderPos.rotation = Quaternion.Euler(0, 0, 0);
        spritePos.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
        spriteVector = new Vector2();
    }

    public void ResetColor()
    {
        overlayMultiplier = 0;
        spriteImage.color = spriteColor;
    }
}
