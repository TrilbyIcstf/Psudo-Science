using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health_UI : MonoBehaviour
{
    private const float baseHeight = 60;

    // The text displayed on the health bar
    public Text healthText;

    // A timer for having the health appear for a certain amount of time
    private float healthTimer = 0;

    // The slider UI being controlled
    private Slider _slider;

    // The images of the health bar
    public Image healthFront;
    public Image healthBack;

    private Slider slider
    {
        get {
            if (_slider == null)
            {
                _slider = GetComponent<Slider>();
            }
            return _slider;
        }
    }

    private void FixedUpdate()
    {
        if (healthTimer > 0) {
            healthTimer -= Time.deltaTime;
            if (healthTimer > 0)
            {
                float scaledAlpha = healthTimer / 0.5f;

                SetOpacity(scaledAlpha);
            } else
            {
                SetEnabled(false);
            }
        }
    }

    public void SetHealth(float current, float max)
    {
        slider.value = Mathf.Clamp(current / max, 0, 1);
        healthText.text = current + "/" + max;
    }

    public void SetEnabled(bool enabled)
    {
        if (enabled || healthTimer <= 0)
        {
            ForceEnabled(enabled);
        }
    }

    public void ForceEnabled(bool enabled)
    {
        SetOpacity(1);

        healthTimer = 0;

        healthText.enabled = enabled;
        healthFront.enabled = enabled;
        healthBack.enabled = enabled;
    }

    public void SetOpacity(float opacity)
    {
        Color frontColor = healthFront.color;
        Color backColor = healthBack.color;
        Color textColor = healthText.color;

        frontColor.a = opacity;
        backColor.a = opacity;
        textColor.a = opacity;

        healthFront.color = frontColor;
        healthBack.color = backColor;
        healthText.color = textColor;
    }

    public void SetTimer(float timer)
    {
        SetEnabled(true);
        healthTimer = timer;
    }

    public void SetHeight(float height)
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector3(0, baseHeight + height, 0);
    }
}
