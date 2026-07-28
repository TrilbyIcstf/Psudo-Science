using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Revive_UI : MonoBehaviour
{
    // The color of the player using the script
    [SerializeField]
    private TColor playerColor;

    // A gradient to change the bar color as it fills
    [SerializeField]
    private Gradient reviveColor;

    // The bar of the slider
    [SerializeField]
    private Image reviveBar;

    // The slider UI being controlled
    private Slider slider;

    // Tracks the progress of the revive. Tries to simulate the actual progress, but may not be accurate.
    private float trackedProgressCounter = 0;
    public float TrackedProgressCounter { get => trackedProgressCounter; }

    // Timer used to set bar value to actual value once blips are finished
    private IEnumerator blipCounter;

    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
        reviveBar.color = reviveColor.Evaluate(0.0f);
    }

    public void RecieveRevive(float amount)
    {
        trackedProgressCounter = Mathf.Min(trackedProgressCounter + amount, Player_Status.REVIVECAP);

        UpdateBar();

        // Refreshes a timer which will update the bar to the correct value once all the blips are gone
        if (blipCounter != null)
        {
            StopCoroutine(blipCounter);
        }
        blipCounter = CheckRemainingBlips();
        StartCoroutine(blipCounter);
    }

    public void SetBar(float amount)
    {
        Debug.Log(amount);
        trackedProgressCounter = amount;
        UpdateBar();
    }

    private void UpdateBar()
    {
        slider.value = Mathf.Clamp(trackedProgressCounter / Player_Status.REVIVECAP, 0, 0.98f);
        reviveBar.color = reviveColor.Evaluate(Mathf.Clamp(trackedProgressCounter / Player_Status.REVIVECAP, 0, 1));
    }

    private IEnumerator CheckRemainingBlips()
    {
        yield return new WaitForSeconds(0.15f);

        GameObject[] blips = GameObject.FindGameObjectsWithTag("Blip");
        bool blipsRemain = false;

        foreach (GameObject b in blips)
        {
            if (b.GetComponent<Energy_Blip>().PlayerNum == (int)playerColor)
            {
                blipsRemain = true;
            }
        }

        if (!blipsRemain)
        {
            SetBar(GameManager.instance.party.GetPlayer(playerColor).Status.ReviveProgress);
        }
    }

    [ContextMenu("Test Fill")]
    public void TestFill()
    {
        float amount = 10.0f;
        trackedProgressCounter += amount;

        slider.value = Mathf.Clamp(trackedProgressCounter / Player_Status.REVIVECAP, 0, 0.98f);
        reviveBar.color = reviveColor.Evaluate(Mathf.Clamp(trackedProgressCounter / Player_Status.REVIVECAP, 0, 1));
    }
}
