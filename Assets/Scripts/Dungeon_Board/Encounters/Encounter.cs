using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object to hold the contents of combat encounters
/// </summary>
[CreateAssetMenu(fileName = "Encounter", menuName = "ScriptableObjects/New Encounter", order = 4)]
[System.Serializable]
public class Encounter : ScriptableObject
{
    // A list of enemies in the combat
    [SerializeField] private List<GameObject> encounterContents;

    // A list of where each enemy's portrait will be displayed
    [SerializeField] private List<Vector2> enemyPositions;

    public List<GameObject> EncounterContents { get => encounterContents; set => encounterContents = value; }
    public List<Vector2> EnemyPositions { get => enemyPositions; set => enemyPositions = value; }
}
