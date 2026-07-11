using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Scriptable object to hold the contents of combat encounters
/// </summary>
[CreateAssetMenu(fileName = "Encounter", menuName = "ScriptableObjects/New Encounter", order = 4)]
[System.Serializable]
public class Encounter : ScriptableObject
{
    // A list of enemies in the combat
    [SerializeField]
    private List<EnemyTuple> encounterContents;

    // A list of where each enemy's portrait will be displayed
    [SerializeField] 
    private List<Vector3> enemyPositions;

    public List<Bestiary> EncounterEnemies { get => encounterContents.Select(e => e.enemy).ToList(); }
    public List<int> EnemyVarients { get => encounterContents.Select(e => e.varient).ToList(); }
    public List<Vector3> EnemyPositions { get => enemyPositions; }

    [Serializable]
    private struct EnemyTuple
    {
        [SerializeField]
        internal Bestiary enemy;

        [SerializeField]
        internal int varient;
    }
}