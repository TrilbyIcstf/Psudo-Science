using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hover_Crosshair : MonoBehaviour
{
    private float baseAlpha = 0.95f;
    private float crosshairAlpha;
    private float animTimer = 0;
    private Image crosshairImage;
    private static float loopTime = 1.5f;

    public void Start()
    {
        crosshairImage = gameObject.GetComponent<Image>();
    }

    public void FixedUpdate()
    {
        animTimer += Time.deltaTime * 0.5f;

        crosshairAlpha = baseAlpha - (Mathf.Sin((animTimer / loopTime) * Mathf.PI) * 0.6f);

        if (animTimer > loopTime)
        {
            animTimer -= loopTime;
        }
    }

    public void Update()
    {
        float scaledAlpha = Mathf.Max(Mathf.Min(1.0f, crosshairAlpha), 0.5f);

        Color tempColor = crosshairImage.color;
        tempColor.a = scaledAlpha;
        crosshairImage.color = tempColor;
    }

    /// <summary>
    /// Moves the position of the crosshair, along with UI effects.
    /// </summary>
    /// <param name="target">
    /// The position to move the crosshair to.
    /// </param>
    public void TargetCrosshair(Vector2 target)
    {
        crosshairAlpha = baseAlpha;
        animTimer = 0;

        gameObject.transform.position = target;
    }

    /// <summary>
    /// Moves the position of the crosshair, but doesn't trigger any UI effects.
    /// </summary>
    /// <param name="target">
    /// The position to move the crosshair to.
    /// </param>
    public void ForceMove(Vector2 target)
    {
        gameObject.transform.position = target;
    }

    public void SetCrosshairEnabled(bool val)
    {
        crosshairImage.enabled = val;
    }
}
