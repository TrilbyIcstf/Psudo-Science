using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_UI : MonoBehaviour
{
    [Header("Player UI")]
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    [Header("")]
    public GameObject player1Health;
    public GameObject player2Health;
    public GameObject player3Health;
    public GameObject player4Health;
    [Header("")]
    public GameObject player1Energy;
    public GameObject player2Energy;
    public GameObject player3Energy;
    public GameObject player4Energy;
    [Header("Crosshair")]
    public Enemy_Crosshair crosshairScript;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.combat.SetCombatUI(this);
    }

    public void TargetCrosshair(Vector3 target)
    {
        crosshairScript.TargetCrosshair(target);
    }

    public void SetCrosshairEnabled(bool val)
    {
        crosshairScript.SetCrosshairEnabled(val);
    }
}
