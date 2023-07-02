using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Enemy : MonoBehaviour
{
    // The template for the enemy
    public Enemy_Information enemyBase;

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
    }

    public void Setup(int position)
    {
        enemyPosition = position;
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
    }

    private void OnMouseExit()
    {
        GameManager.instance.combat.UnhoverEnemy(enemyPosition);
    }

    public Enemy_Visuals getSpriteInfo()
    {
        return visuals;
    }
}
