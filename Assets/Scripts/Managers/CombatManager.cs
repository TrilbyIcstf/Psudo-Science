using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Board_Controller board;
    public Player_Energy energy;

    public void CombatSetup()
    {
        energy = new Player_Energy();
    }

    public void CombatSetup(Board_Controller _board)
    {
        board = _board;
        CombatSetup();
    }
}
