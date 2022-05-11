using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Board_Controller board;
    public Combat_UI combatUI;
    public Player_Energy energy;

    public void CombatSetup()
    {
        energy = new Player_Energy();
    }

    public void CombatCleanup()
    {
        energy = null;
        combatUI = null;
        board = null;
    }

    public void SetBoard(Board_Controller _val)
    {
        board = _val;
    }

    public void SetCombatUI(Combat_UI _val)
    {
        combatUI = _val;
    }
}
