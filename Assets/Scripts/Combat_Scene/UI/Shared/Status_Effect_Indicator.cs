using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Status_Effect_Indicator : MonoBehaviour
{
    [SerializeField] private Image statusImage;
    [SerializeField] private TextMeshProUGUI durationText;

    private void Awake()
    {
        Setup(StatusEffect.MAJORPOWERUP, 2);
        UpdateDuration(3);
    }

    public void Setup(StatusEffect effect, int duration)
    {
        statusImage.sprite = GameManager.instance.ll.statusIcons.GetValue(effect);
        durationText.text = duration.ToString();
    }

    public void UpdateDuration(int duration)
    {
        durationText.text = duration.ToString();
    }
}
