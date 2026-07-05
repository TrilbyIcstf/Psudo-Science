using UnityEngine;

public class Combat_Move_Button : MonoBehaviour
{
    [SerializeField]
    private GameObject highlight;

    public void OnClick()
    {
        GameManager.instance.combat.SelectMove(PC.VANESSA, 1);
        Debug.Log("HI");
    }

    public void SetHighlight(bool val)
    {
        highlight.SetActive(val);
    }
}
