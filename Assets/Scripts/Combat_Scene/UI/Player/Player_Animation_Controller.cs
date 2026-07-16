using UnityEngine;
using UnityEngine.UI;

public class Player_Animation_Controller : MonoBehaviour
{
    // Animation controller attached to the player image
    private Animator animController;

    // Transform of the player image
    private RectTransform spritePos;

    // The image for the player
    private Image spriteImage;

    // Color overlay
    private Color spriteColor;
    private Color overlayColor = Color.black;
    [SerializeField]
    private float overlayMultiplier = 0;

    private void Awake()
    {
        animController = GetComponent<Animator>();
        spritePos = GetComponent<RectTransform>();
        spriteImage = GetComponent<Image>();

        spriteColor = spriteImage.color;
    }

    private void Update()
    {
        if (overlayMultiplier > 0)
        {
            Color tempColor = spriteColor;
            tempColor.r = spriteColor.r + (overlayColor.r - spriteColor.r) * overlayMultiplier;
            tempColor.g = spriteColor.g + (overlayColor.g - spriteColor.g) * overlayMultiplier;
            tempColor.b = spriteColor.b + (overlayColor.b - spriteColor.b) * overlayMultiplier;
            spriteImage.color = tempColor;
        }
    }

    public void PlayAnimation(PlayerAnimation ea)
    {
        animController.SetTrigger(ea.ToAnimString());
    }

    public void PlayAnimationRotated(PlayerAnimation ea, float rotation)
    {
        spritePos.rotation = Quaternion.Euler(0, 0, rotation);
        animController.SetTrigger(ea.ToAnimString());
    }

    public void PlayAnimationColor(PlayerAnimation ea, Color color)
    {
        overlayColor = color;
        animController.SetTrigger(ea.ToAnimString());
    }

    public void ResetRotation()
    {
        spritePos.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void ResetColor()
    {
        overlayMultiplier = 0;
        spriteImage.color = spriteColor;
    }
}
