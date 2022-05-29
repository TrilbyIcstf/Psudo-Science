using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Energy_Hover : MonoBehaviour
{
    public Text energyText;
    public TColor playerColor;

    /// <summary>
    /// Updates the energy total if bar is hovered over.
    /// </summary>
    private void Update()
    {
        if (energyText.enabled)
        {
            //string tempText = GameManager.instance.combat.energy.GetColor(playerColor).ToString("F0") + "/" + GameManager.instance.combat.energy.GetCap(playerColor);
            string tempText = GetComponent<Energy_UI>().EnergyCounter.ToString("F0") + "/" + GameManager.instance.combat.energy.GetCap(playerColor);
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
