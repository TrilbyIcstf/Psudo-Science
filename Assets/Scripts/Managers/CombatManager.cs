using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Board_Controller board;
    public Combat_UI combatUI;
    public Player_Energy energy;

    [SerializeField] private List<GameObject> activeEnemies;

    public void CombatSetup(Encounter _enc)
    {
        energy = new Player_Energy();
        Transform enemyHolderPos = GameObject.FindGameObjectWithTag("EnemyHolder").transform;

        for (int i = 0; i < _enc.EncounterContents.Count; i++)
        {
            activeEnemies.Add(Instantiate(_enc.EncounterContents[i], enemyHolderPos));
            activeEnemies[i].transform.position += _enc.EnemyPositions[i];
        }
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
