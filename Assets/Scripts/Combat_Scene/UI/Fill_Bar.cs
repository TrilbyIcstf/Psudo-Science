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
    protected Image bar;

    // The slider UI being controlled
    protected Slider slider;

    // Tracks the displayed amount on the bar
    protected float progress = 0;
    public float Progress { get => progress; }

    protected int max = 0;
    public int Max { get => max; }

    Dictionary<GameObject, int> incomingChanges = new Dictionary<GameObject, int>();

    private void Start()
    {
        slider = GetComponent<Slider>();
        RefreshBarFromSource();
    }

    public void ApplyChange(GameObject messenger)
    {
        if (incomingChanges.ContainsKey(messenger))
        {
            int amount = incomingChanges[messenger];
            incomingChanges.Remove(messenger);

            if (amount >= 0)
            {
                AddToBar(amount);
            }
            else
            {
                RemoveFromBar(amount);
            }

            UpdateBar();

            if (incomingChanges.Count == 0)
            {
                RefreshBarFromSource();
            }
        }
        else
        {
            Debug.LogError("Unregistered messenger sent to health bar!");
        }
    }

    public void RegisterChange(GameObject messenger, int amount)
    {
        incomingChanges.Add(messenger, amount);
    }

    public virtual void AddToBar(int amount)
    {
        progress = Mathf.Min(progress + amount, max);
        UpdateBar();
    }

    public void RemoveFromBar(int amount)
    {
        progress = Mathf.Max(progress - amount, 0);
        UpdateBar();
    }

    public void SetBar(float amount)
    {
        progress = Mathf.Clamp(amount, 0, max);
        UpdateBar();
    }

    protected void UpdateBar()
    {
        float fillAmount = FillAmount();
        slider.value = fillAmount;
        bar.color = barColor.Evaluate(fillAmount);
    }

    protected float FillAmount()
    {
        return Mathf.Clamp(progress / max, 0, 1);
    }

    public abstract void RefreshBarFromSource();
}
