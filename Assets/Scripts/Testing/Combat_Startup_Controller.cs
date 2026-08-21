using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Startup_Controller : MonoBehaviour
{
    public Encounter testEnemies;

    [SerializeField]
    Combat_UI combatUI;

    [SerializeField]
    Board_Controller boardController;

    private void Awake()
    {
        GameManager.instance.party.ResetStatus(); // FOR TESTING

        foreach (Player_UI_Controller playerUI in combatUI.PlayerUI)
        {
            playerUI.Setup();
        }
        combatUI.Setup();
        boardController.Setup();
        GameManager.instance.combat.CombatSetup(testEnemies);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(new Vector2(3, 3), 0.7f);
    }
}