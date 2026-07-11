using UnityEngine;
using TMPro;

public class Enemy_Turn_UI : MonoBehaviour
{
    private const float baseHeight = 90;

    private int turnNumber = 0;

    private TextMeshProUGUI turnText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        turnText = GetComponent<TextMeshProUGUI>();
    }

    public void SetTurnNumber(int val)
    {
        turnNumber = val;
        turnText.text = turnNumber.ToString();
    }

    public void SetHeight(float height)
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector3(0, baseHeight + height, 0);
    }
}
