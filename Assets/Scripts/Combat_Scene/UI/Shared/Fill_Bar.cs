using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Fill_Bar : MonoBehaviour
{
    // A gradient to change the bar color as it fills
    [SerializeField]
    protected Gradient barColor;

    // The bar of the slider
    [SerializeField]
    protected Image frontBar;

    // Damage Text
    [SerializeField]
    protected GameObject damageTextObject;

    // The slider UI being controlled
    protected Slider slider;

    // Tracks the displayed amount on the bar
    protected float progress = 0;
    public float Progress { get => progress; }

    protected int max = 0;
    public int Max { get => max; }

    Dictionary<GameObject, int> incomingChanges = new Dictionary<GameObject, int>();

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void ApplyChange(GameObject messenger)
    {
        if (incomingChanges.ContainsKey(messenger))
        {
            int amount = incomingChanges[messenger];
            incomingChanges.Remove(messenger);

            AddToBar(amount);

            DisplayChange(amount);

            if (incomingChanges.Count == 0)
            {
                RefreshBarFromSource();
            }
        }
        else
        {
            Debug.LogError("Unregistered messenger sent to bar!");
        }
    }

    public void RegisterChange(GameObject messenger, int amount)
    {
        incomingChanges.Add(messenger, amount);
    }

    public virtual void AddToBar(int amount)
    {
        progress = Mathf.Clamp(progress + amount, 0, max);
        UpdateBar();
    }

    public void RemoveFromBar(int amount)
    {
        AddToBar(-amount);
    }

    public void SetBar(float amount)
    {
        progress = Mathf.Clamp(amount, 0, max);
        UpdateBar();
    }

    protected virtual void UpdateBar()
    {
        float fillAmount = FillAmount();
        slider.value = fillAmount;
        frontBar.color = barColor.Evaluate(fillAmount);
    }

    protected float FillAmount()
    {
        return Mathf.Clamp(progress / max, 0, 1);
    }

    protected virtual void DisplayChange(int amount) { }
    public abstract void RefreshBarFromSource();
}
