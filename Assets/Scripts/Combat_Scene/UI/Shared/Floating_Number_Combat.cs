using UnityEngine;
using TMPro;

public class Floating_Number_Combat : MonoBehaviour
{
    [SerializeField]
    private float lifespan = 0;
    private float lifetime = 0;

    [SerializeField]
    private float speed = 0;

    [SerializeField]
    private Gradient fadeColor;

    private TextMeshProUGUI text;
    private RectTransform rectTransform;

    private float Progress { get => lifetime / lifespan; }

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string val, Effectiveness effectiveness)
    {
        text.text = val;

        if (effectiveness == Effectiveness.WEAKNESS)
        {
            text.text += "!!";
            Vector3 scale = transform.localScale;
            scale.x *= 1.25f;
            scale.y *= 1.25f;
            transform.localScale = scale;
        } else if (effectiveness == Effectiveness.STRENGTH)
        {
            text.text += "...";
            Vector3 scale = transform.localScale;
            scale.x *= 0.75f;
            scale.y *= 0.75f;
            transform.localScale = scale;
        }
    }

    private void FixedUpdate()
    {
        lifetime += Time.deltaTime;

        Vector2 pos = rectTransform.anchoredPosition;
        pos.y += speed / Time.deltaTime;
        rectTransform.anchoredPosition = pos;

        Color tCol = fadeColor.Evaluate(Progress);
        tCol.a = 1.0f - (Progress / 1.25f);
        text.color = tCol;

        if (lifetime >= lifespan)
        {
            Destroy(gameObject);
        }
    }
}
