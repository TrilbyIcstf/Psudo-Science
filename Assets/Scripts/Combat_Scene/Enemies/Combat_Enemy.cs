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
    private int enemyPosition;

    // The enemy's current stats
    private int displayHealth;
    private bool alive = true;

    // The behavior script which decides how the enemy attacks
    private Behavior_Dad behavior;

    // Script for controlling appearance of the enemy
    private Enemy_Visuals visuals;

    public void Setup(int position, int varient)
    {
        behavior = GetComponent<Behavior_Dad>();
        behavior.SetVarient(varient);
        visuals = GetComponent<Enemy_Visuals>();
        visuals.Startup(enemyBase);
        visuals.SetBehavior(behavior);
        stats = new Enemy_Stats(enemyBase);
        displayHealth = stats.MaxHealth;
        enemyPosition = position;
    }

    public void TakeDamage(int amount)
    {
        stats.CurrentHealth = Mathf.Max(0, stats.CurrentHealth - amount);
        displayHealth = stats.CurrentHealth;
        visuals.UpdateHealthBar(displayHealth, stats.MaxHealth);
    }

    // Visually displays damage without actually reducing the enemy's health
    public void TakeDisplayDamage(int amount)
    {
        displayHealth = Mathf.Max(0, displayHealth - amount);
        visuals.UpdateHealthBar(displayHealth, stats.MaxHealth);
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
            GameManager.instance.combat.TargetEnemy(enemyPosition);
        }
    }

    private void OnMouseEnter()
    {
        if (alive && !GameManager.instance.fx.CheckAllFXLock())
        {
            GameManager.instance.combat.HoverEnemy(enemyPosition);
        }
        visuals.SetHealthBarEnabled(true);
    }

    private void OnMouseExit()
    {
        GameManager.instance.combat.UnhoverEnemy(enemyPosition);

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
