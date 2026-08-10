using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Energy_Hover : MonoBehaviour
{
    public Text energyText;
    public TColor playerColor;

    private Energy_UI energyScript;

    private void Awake()
    {
        energyScript = GetComponent<Energy_UI>();
    }

    /// <summary>
    /// Updates the energy total if bar is hovered over.
    /// </summary>
    private void Update()
    {
        if (energyText.enabled)
        {
            string tempText = energyScript.Progress.ToString("F0") + "/" + energyScript.Max.ToString("F0");
            energyText.text = tempText;
        }
    }

    private void OnMouseEnter()
    {
        energyText.enabled = true;
    }

    private void OnMouseExit()
    {
        energyText.enabled = false;
    }
}
