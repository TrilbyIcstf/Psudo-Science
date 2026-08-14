using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Enemy : MonoBehaviour
{
    // The template for the enemy
    public Enemy_Information enemyBase;

    private Enemy_Stats stats;

    // Position of this enemy in the enemy array
    private int enemyNum;

    // The enemy's current stats
    private bool alive = true;

    // The behavior script which decides how the enemy attacks
    private Behavior_Dad behavior;

    // Script for controlling appearance of the enemy
    private Enemy_Visuals visuals;

    public void Setup(int position, int varient)
    {
        stats = new Enemy_Stats(enemyBase);
        enemyNum = position;
        behavior = GetComponent<Behavior_Dad>();
        behavior.SetVarient(varient);
        visuals = GetComponent<Enemy_Visuals>();
        visuals.Startup(enemyBase, position);
        visuals.SetBehavior(behavior);
        visuals.StatusIcons.SetStatusList(stats.StatusEffects);
    }

    public void TakeDamage(int amount)
    {
        stats.CurrentHealth = Mathf.Max(0, stats.CurrentHealth - amount);
        visuals.HealthBar.SetBar(stats.CurrentHealth);
    }

    // Visually displays damage without actually reducing the enemy's health
    public void TakeDisplayDamage(int amount)
    {
        visuals.HealthBar.RemoveFromBar(amount);
    }

    public void RegisterDisplayDamage(GameObject messenger, int amount)
    {
        visuals.HealthBar.RegisterChange(messenger, -amount);
    }

    public void ApplyDisplayDamage(GameObject messenger)
    {
        visuals.HealthBar.ApplyChange(messenger);
    }

    public bool AddStatusEffect(StatusEffect se, int duration)
    {
        bool overriden = stats.AddStatusEffect(se, duration);
        visuals.StatusIcons.SetStatusList(stats.StatusEffects);

        return overriden;
    }

        public bool ShouldDie()
    {
        return stats.CurrentHealth <= 0;
    }

    public bool Die()
    {
        this.alive = false;
        return true;
    }

    private void OnMouseDown()
    {
        if (alive && !GameManager.instance.fx.CheckAllFXLock())
        {
            GameManager.instance.combat.TargetEnemy(enemyNum);
        }
    }

    private void OnMouseEnter()
    {
        if (alive && !GameManager.instance.fx.CheckAllFXLock())
        {
            GameManager.instance.combat.HoverEnemy(enemyNum);
        }
        visuals.SetHealthBarEnabled(true);
    }

    private void OnMouseExit()
    {
        GameManager.instance.combat.UnhoverEnemy(enemyNum);

        visuals.SetHealthBarEnabled(false);
    }

    public Enemy_Visuals GetSpriteInfo()
    {
        return visuals;
    }

    public Enemy_Stats GetStats()
    {
        return stats;
    }

    public bool IsAlive()
    {
        return alive;
    }
}
