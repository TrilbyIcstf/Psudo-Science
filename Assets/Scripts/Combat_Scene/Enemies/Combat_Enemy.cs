using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Enemy : MonoBehaviour
{
    // The template for the enemy
    public Enemy_Information enemyBase;

    // The enemy's current stats
    private int currentHealth;

    private Behavior_Dad behavior;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = enemyBase.MaxHealth;
        behavior = GetComponent<Behavior_Dad>();
    }
}
