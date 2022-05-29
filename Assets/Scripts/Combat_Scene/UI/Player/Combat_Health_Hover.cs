using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Health_Hover : MonoBehaviour
{
    public Text healthText;
    public TColor playerColor;

    /// <summary>
    /// Updates the health total if bar is hovered over.
    /// </summary>
    private void Update()
    {
        if (healthText.enabled)
        {
            string tempText = GameManager.instance.party.GetPlayer(playerColor).CurrentHealth + "/" + GameManager.instance.party.GetPlayerInfo(playerColor).MaxHealth;
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
