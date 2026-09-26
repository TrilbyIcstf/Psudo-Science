using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Enemy_Turn_UI : MonoBehaviour
{
    [SerializeField]
    private Image intentImage;

    private const float baseHeight = 110;

    private int turnNumber = 0;

    [SerializeField]
    private TextMeshProUGUI turnText;

    public void SetTurnNumber(int val)
    {
        turnNumber = val;
        turnText.text = turnNumber.ToString();
    }

    public void SetIntent(MoveType type)
    {
        intentImage.sprite = GameManager.instance.ll.intentIcons.GetValue(type);
    }

    public void SetHeight(float height)
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector3(0, baseHeight + height, 0);
    }
}
