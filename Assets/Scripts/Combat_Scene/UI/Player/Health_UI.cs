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

    protected override void DisplayChange(int amount)
    {
        if (amount < 0)
        {
            float posOffset = Random.Range(-0.75f, 0.75f);

            GameObject damageNum = Instantiate(damageTextObject, transform);
            damageNum.GetComponent<Floating_Number_Combat>().SetText(Mathf.Abs(amount).ToString());
            Vector3 spawnPos = damageNum.transform.position;
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
