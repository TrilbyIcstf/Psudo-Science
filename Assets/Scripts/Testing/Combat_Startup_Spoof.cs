using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Startup_Spoof : MonoBehaviour
{
    private void Start()
    {
        GameManager.instance.combat.CombatSetup();
    }
}
