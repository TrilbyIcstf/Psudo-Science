using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Startup_Spoof : MonoBehaviour
{
    public Encounter testEnemies;
    public GameObject testParticleController;
    private GameObject tempController;

    private void Start()
    {
        GameManager.instance.combat.CombatSetup(testEnemies);
        tempController = Instantiate(testParticleController);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A down");
            StartCoroutine(tempController.GetComponent<Lesser_Fire_Controller>().Activate(new Vector2(-3, -3), new Vector2(3, 3)));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector2(3, 3), 0.7f);
    }
}