using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the UI slider for the player's energy bar
/// </summary>
public class Energy_UI : MonoBehaviour
{
    // The color of the player using the script
    public TColor playerColor;

    // A gradient to change the bar color as it fills
    public Gradient energyColor;

    // The bar of the slider
    public Image energyBar;

    // The slider UI being controlled
    private Slider slider;

    // Tracks the amount of energy being displayed. Tries to simulate the actual energy of that color, but may not be accurate.
    private float trackedEnergyCounter = 0;

    // Tracks the energy cap to display.
    private float trackedEnergyCap;

    // Timer used to set bar value to actual value once blips are finished
    private IEnumerator blipCounter;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
        energyBar.color = energyColor.Evaluate(0.0f);
        trackedEnergyCap = GameManager.instance.combat.energy.GetCap(playerColor);
    }

    public void RecieveEnergy(float amount)
    {
        trackedEnergyCounter += amount;
        while (trackedEnergyCounter >= trackedEnergyCap)
        {
            trackedEnergyCounter -= trackedEnergyCap;
            trackedEnergyCap = GameManager.instance.combat.energy.GetCap(playerColor);
        }

        trackedEnergyCounter = trackedEnergyCounter % trackedEnergyCap;

        slider.value = Mathf.Clamp(trackedEnergyCounter / trackedEnergyCap, 0, 0.98f);
        energyBar.color = energyColor.Evaluate(Mathf.Clamp(trackedEnergyCounter / trackedEnergyCap, 0, 1));

        // Refreshes a timer which will update the bar to the correct value once all the blips are gone
        if (blipCounter != null)
        {
            StopCoroutine(blipCounter);
        }
        blipCounter = CheckRemainingBlips();
        StartCoroutine(blipCounter);
    }

    public void SetEnergy(float amount)
    {
        trackedEnergyCounter = amount;
        trackedEnergyCap = GameManager.instance.combat.energy.GetCap(playerColor);

        slider.value = Mathf.Clamp((trackedEnergyCounter % trackedEnergyCap) / trackedEnergyCap, 0, 0.98f);
        energyBar.color = energyColor.Evaluate(Mathf.Clamp((trackedEnergyCounter % trackedEnergyCap) / trackedEnergyCap, 0, 1));
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
            SetEnergy(GameManager.instance.combat.energy.GetColor(playerColor));
        }
    }

    public float EnergyCounter { get => trackedEnergyCounter; set => trackedEnergyCounter = value; }
    public float EnergyCap { get => trackedEnergyCap; set => trackedEnergyCap = value; }
}
