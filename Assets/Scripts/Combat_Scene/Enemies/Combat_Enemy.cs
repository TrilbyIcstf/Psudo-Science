using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Enemy : MonoBehaviour
{
    // The template for the enemy
    public Enemy_Information enemyBase;

    // The enemy's current stats
    private int currentHealth;
    private bool alive = true;

    // The behavior script which decides how the enemy attacks
    private Behavior_Dad behavior;

    // The in game image element of the enemy
    private Image enemyImage;
    private RectTransform imageTransform;

    // Start is called before the first frame update
    void Start()
    {
        enemyImage = GetComponent<Image>();

        enemyImage.sprite = enemyBase.EnemySprite;

        imageTransform = GetComponent<RectTransform>();
        imageTransform.sizeDelta = enemyBase.SpriteSize;

        currentHealth = enemyBase.MaxHealth;
        behavior = GetComponent<Behavior_Dad>();
    }
}
