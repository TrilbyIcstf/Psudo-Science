using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Health_UI : Fill_Bar
{
    private const float baseHeight = 60;

    // The text displayed on the health bar
    public Text healthText;

    // A timer for having the health appear for a certain amount of time
    private float healthTimer = 0;

    // The images of the health bar
    [SerializeField]
    private Image backBar;

    // Enemy number in combat
    private int enemyNum;

    private void FixedUpdate()
    {
        if (healthTimer > 0) {
            healthTimer -= Time.deltaTime;
            if (healthTimer > 0)
            {
                float scaledAlpha = healthTimer / 0.5f;

                SetOpacity(scaledAlpha);
            } else
            {
                SetEnabled(false);
            }
        }
    }

    public void Setup(int enemyNum)
    {
        this.enemyNum = enemyNum;
        RefreshBarFromSource();
    }

    public void SetHealth(float val)
    {
        progress = val;
        UpdateBar();
    }

    public void SetEnabled(bool enabled)
    {
        if (enabled || healthTimer <= 0)
        {
            ForceEnabled(enabled);
        }
    }

    public void ForceEnabled(bool enabled)
    {
        SetOpacity(1);

        healthTimer = 0;

        healthText.enabled = enabled;
        frontBar.enabled = enabled;
        backBar.enabled = enabled;
    }

    public void SetOpacity(float opacity)
    {
        Color frontColor = frontBar.color;
        Color backColor = backBar.color;
        Color textColor = healthText.color;

        frontColor.a = opacity;
        backColor.a = opacity;
        textColor.a = opacity;

        frontBar.color = frontColor;
        backBar.color = backColor;
        healthText.color = textColor;
    }

    public void SetTimer(float timer)
    {
        SetEnabled(true);
        healthTimer = timer;
    }

    public void SetHeight(float height)
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector3(0, baseHeight + height, 0);
    }

    protected override void DisplayChange(int amount)
    {
        if (amount < 0)
        {
            float posOffset = Random.Range(-0.75f, 0.75f);

            GameObject damageNum = Instantiate(damageTextObject, transform);
            damageNum.GetComponent<Floating_Number_Combat>().SetText(Mathf.Abs(amount).ToString());
            Vector3 spawnPos = damageNum.transform.position;
            spawnPos.x += posOffset;
            damageNum.transform.position = spawnPos;
        }
    }

    public override void RefreshBarFromSource()
    {
        max = GameManager.instance.combat.GetEnemy(enemyNum).GetStats().MaxHealth;
        progress = GameManager.instance.combat.GetEnemy(enemyNum).GetStats().CurrentHealth;

        UpdateBar();
    }

    protected override void UpdateBar()
    {
        base.UpdateBar();
        healthText.text = progress + "/" + max;
    }
}
