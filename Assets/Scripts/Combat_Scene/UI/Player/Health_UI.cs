using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the UI slider for the player's health bar
/// </summary>
public class Health_UI : Fill_Bar
{
    // The color of the player using the script
    [SerializeField]
    private TColor playerColor;

    public override void RefreshBarFromSource()
    {
        max = GameManager.instance.party.GetPlayer(playerColor).MaxHealth;
        progress = GameManager.instance.party.GetPlayer(playerColor).CurrentHealth;

        UpdateBar();
    }
}
