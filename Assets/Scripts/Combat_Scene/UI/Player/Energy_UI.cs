using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the UI slider for the player's energy bar
/// </summary>
public class Energy_UI : Fill_Bar
{
    // The color of the player using the script
    public TColor playerColor;

    public override void AddToBar(int amount)
    {
        progress += amount;
        while (progress >= max)
        {
            progress -= max;
            max = GameManager.instance.combat.energy.GetCap(playerColor);
        }

        UpdateBar();
    }

    public override void RefreshBarFromSource()
    {
        progress = GameManager.instance.combat.energy.GetColor(playerColor);
        max = (int)GameManager.instance.combat.energy.GetCap(playerColor);

        UpdateBar();
    }
}
