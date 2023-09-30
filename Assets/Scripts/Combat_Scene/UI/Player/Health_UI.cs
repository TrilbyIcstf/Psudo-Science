using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the UI slider for the player's health bar
/// </summary>
public class Health_UI : MonoBehaviour
{
    // The color of the player using the script
    public TColor playerColor;

    // The bar of the slider
    public Image healthBar;

    // The slider UI being controlled
    private Slider slider;

    // Tracks the amount of energy being displayed. Tries to simulate the actual energy of that color, but may not be accurate.
    private float healthCounter = 0;

    // Timer used to set bar value to actual value once blips are finished
    private IEnumerator blipCounter;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        SetHealth(GameManager.instance.party.GetPlayer(playerColor).Status.CurrentHealth);
    }

    public void RecieveHealth(float amount)
    {
        healthCounter += amount;
        healthCounter = Mathf.Min(healthCounter, GameManager.instance.party.GetPlayer(playerColor).MaxHealth);

        slider.value = Mathf.Clamp(healthCounter / GameManager.instance.party.GetPlayer(playerColor).MaxHealth, 0, 1);

        // Refreshes a timer which will update the bar to the correct value once all the blips are gone
        if (blipCounter != null)
        {
            StopCoroutine(blipCounter);
        }
        blipCounter = CheckRemainingBlips();
        StartCoroutine(blipCounter);
    }

    public void SetHealth(float amount)
    {
        healthCounter = amount;
        healthCounter = Mathf.Min(healthCounter, GameManager.instance.party.GetPlayer(playerColor).MaxHealth);

        slider.value = Mathf.Clamp(healthCounter / GameManager.instance.party.GetPlayer(playerColor).MaxHealth, 0, 1);
    }

    private IEnumerator CheckRemainingBlips()
    {
        yield return new WaitForSeconds(0.15f);

        GameObject[] blips = GameObject.FindGameObjectsWithTag("Blip");
        bool blipsRemain = false;

        foreach (GameObject b in blips)
        {
            if (b.GetComponent<Energy_Blip>().PlayerNum == (int)playerColor + 1)
            {
                blipsRemain = true;
            }
        }

        if (!blipsRemain)
        {
            SetHealth(GameManager.instance.party.GetPlayer(playerColor).Status.CurrentHealth);
        }
    }
}
