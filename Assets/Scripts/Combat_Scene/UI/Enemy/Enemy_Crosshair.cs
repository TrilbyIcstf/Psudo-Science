using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Crosshair : MonoBehaviour
{
    private float crosshairAlpha = 1.0f;
    private Image crosshairImage;

    public void Start()
    {
        crosshairImage = gameObject.GetComponent<Image>();
    }

    public void FixedUpdate()
    {
        if (crosshairAlpha > 0.4f)
        {
            float scaledAlpha = Mathf.Min(1.0f, crosshairAlpha);

            Color tempColor = crosshairImage.color;
            tempColor.a = scaledAlpha;
            crosshairImage.color = tempColor;

            crosshairImage.transform.localScale = new Vector3(0.9f + (scaledAlpha / 4), 0.9f + (scaledAlpha / 4), 1);

            crosshairAlpha -= 0.04f;
        }
    }

    /// <summary>
    /// Moves the position of the crosshair, along with UI effects.
    /// </summary>
    /// <param name="target">
    /// The position to move the crosshair to.
    /// </param>
    public void TargetCrosshair(Vector3 target)
    {
        crosshairAlpha = 2.0f;

        gameObject.transform.position = target;
    }

    /// <summary>
    /// Moves the position of the crosshair, but doesn't trigger any UI effects.
    /// </summary>
    /// <param name="target">
    /// The position to move the crosshair to.
    /// </param>
    public void ForceMove(Vector3 target)
    {
        gameObject.transform.position = target;
    }

    public void SetCrosshairEnabled(bool val)
    {
        crosshairImage.enabled = val;
    }
}
