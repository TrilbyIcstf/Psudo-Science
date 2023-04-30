using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Startup_Spoof : MonoBehaviour
{
    public Encounter testEnemies;
    public GameObject testAttack;

    private void Start()
    {
        GameManager.instance.combat.CombatSetup(testEnemies);
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            QueuedMove qm = new QueuedMove(testAttack, PC.VANESSA);
            GameManager.instance.combat.AddMoveToQueue(qm);
            qm = new QueuedMove(testAttack, PC.VANESSA);
            GameManager.instance.combat.AddMoveToQueue(qm);
            GameManager.instance.combat.StartQueue();
        }
        */
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector2(3, 3), 0.7f);
    }
}