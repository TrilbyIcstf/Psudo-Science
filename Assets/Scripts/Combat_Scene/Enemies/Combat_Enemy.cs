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
    private int currentHealth;
    private bool alive = true;

    // The behavior script which decides how the enemy attacks
    private Behavior_Dad behavior;

    // Script for controlling appearance of the enemy
    private Enemy_Visuals visuals;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyBase.MaxHealth;
        behavior = GetComponent<Behavior_Dad>();
        visuals = GetComponent<Enemy_Visuals>();
        visuals.Startup(enemyBase);
        stats = new Enemy_Stats(enemyBase);
    }

    public void Setup(int position)
    {
        enemyPosition = position;
    }

    public void TakeDamage(int amount)
    {
        stats.CurrentHealth = Mathf.Max(0, stats.CurrentHealth - amount);
        visuals.UpdateHealthBar(stats.CurrentHealth, stats.MaxHealth);
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
            visuals.SetHealthBarEnabled(true);
        }
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

    public bool isAlive()
    {
        return alive;
    }
}
