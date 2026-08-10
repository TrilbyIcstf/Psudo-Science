using UnityEngine;
using UnityEngine.UI;

public class Combat_Revive_Hover : MonoBehaviour
{
    [SerializeField]
    private Text reviveText;

    private Revive_UI reviveScript;

    private void Awake()
    {
        reviveScript = GetComponent<Revive_UI>();
    }

    /// <summary>
    /// Updates the revive total if bar is hovered over.
    /// </summary>
    private void Update()
    {
        if (reviveText.enabled)
        {
            string tempText = reviveScript.Progress.ToString("F0") + "/" + reviveScript.Max.ToString("F0");
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
