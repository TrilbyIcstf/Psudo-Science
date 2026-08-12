using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Revive_UI : Fill_Bar
{
    // The color of the player using the script
    [SerializeField]
    private TColor playerColor;

    // A gradient to change the bar color as it fills
    [SerializeField]
    private Gradient reviveColor;

    private void Start()
    {
        RefreshBarFromSource();
    }

    public override void RefreshBarFromSource()
    {
        progress = GameManager.instance.party.GetPlayer(playerColor).Status.ReviveProgress;
        max = Player_Status.REVIVECAP;
    }
}
