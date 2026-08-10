using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Health_Hover : MonoBehaviour
{
    public Text healthText;
    public TColor playerColor;

    private Health_UI healthScript;

    private void Awake()
    {
        healthScript = GetComponent<Health_UI>();
    }

    /// <summary>
    /// Updates the health total if bar is hovered over.
    /// </summary>
    private void Update()
    {
        if (healthText.enabled)
        {
            string tempText = healthScript.Progress.ToString("F0") + "/" + healthScript.Max.ToString("F0");
            healthText.text = tempText;
        }
    }

    private void OnMouseEnter()
    {
        healthText.enabled = true;
    }

    private void OnMouseExit()
    {
        healthText.enabled = false;
    }
}
