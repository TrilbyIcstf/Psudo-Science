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

    private void Start()
    {
        RefreshBarFromSource();
    }

    protected override void DisplayChangeNumber(BarChangeDetails details)
    {
        int amount = details.Amount;
        if (!details.IsIncrease)
        {
            float posOffset = Random.Range(-0.75f, 0.75f);

            GameObject damageNum = Instantiate(damageTextObject, transform.parent);
            damageNum.GetComponent<Floating_Number_Combat>().SetText(amount.ToString(), details.Effectiveness);
            Vector3 spawnPos = transform.position;
            spawnPos.x += posOffset;
            damageNum.transform.position = spawnPos;
        }
    }

    public override void RefreshBarFromSource()
    {
        max = GameManager.instance.party.GetPlayer(playerColor).MaxHealth;
        progress = GameManager.instance.party.GetPlayer(playerColor).CurrentHealth;

        UpdateBar();
    }
}
