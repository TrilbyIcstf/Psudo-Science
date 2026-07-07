using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Crosshair : MonoBehaviour
{
    private const float MAXALPHA = 2.0f;
    private const float MINALPHA = 0.6f;
    private const float ALPHASPEED = 0.03f;

    private float crosshairAlpha = 1.0f;
    private Image crosshairImage;

    public void Start()
    {
        crosshairImage = gameObject.GetComponent<Image>();
    }

    public void FixedUpdate()
    {
        if (crosshairAlpha > MINALPHA)
        {
            crosshairAlpha -= ALPHASPEED;
        }
    }

    public void Update()
    {
        UpdateScale();
    }

    private void UpdateScale()
    {
        if (crosshairAlpha > MINALPHA)
        {
            float scaledAlpha = Mathf.Min(1.0f, crosshairAlpha);
            float aLerp = Mathf.Lerp(0.9f, 1.15f, LerpCalc(scaledAlpha));

            crosshairImage.transform.localScale = new Vector3(aLerp, aLerp, 1);

            Color tempColor = crosshairImage.color;
            tempColor.a = scaledAlpha;
            crosshairImage.color = tempColor;
        }
    }

    private float LerpCalc(float scaledAlpha)
    {
        float rise = 1;
        float run = 1 - MINALPHA;
        float a = rise / run;
        float b = -(a * MINALPHA);
        float result = (a * scaledAlpha) + b;
        return result;
    }

    /// <summary>
    /// Moves the position of the crosshair, along with UI effects.
    /// </summary>
    /// <param name="target">
    /// The position to move the crosshair to.
    /// </param>
    public void TargetCrosshair(Vector2 target)
    {
        crosshairAlpha = MAXALPHA;
        UpdateScale();

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
