using UnityEngine;
using UnityEngine.UI;

public class Combat_Revive_Hover : MonoBehaviour
{
    [SerializeField]
    private Text reviveText;

    /// <summary>
    /// Updates the revive total if bar is hovered over.
    /// </summary>
    private void Update()
    {
        if (reviveText.enabled)
        {
            string tempText = GetComponent<Revive_UI>().TrackedProgressCounter.ToString("F0") + "/" + Player_Status.REVIVECAP.ToString("F0");
            reviveText.text = tempText;
        }
    }

    private void OnMouseEnter()
    {
        reviveText.enabled = true;
    }

    private void OnMouseExit()
    {
        reviveText.enabled = false;
    }
}
