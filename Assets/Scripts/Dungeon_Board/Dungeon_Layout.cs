using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dungeon Layout", menuName = "ScriptableObjects/New Dungeon", order = 8)]
[System.Serializable]
public class Dungeon_Layout : ScriptableObject
{
    [SerializeField]
    private string dungeonName;
    public string DungeonName { get => dungeonName; }

    [SerializeField]
    private DungeonLayoutDictionary layout;
    public DungeonLayoutDictionary Layout { get => layout; }

    [SerializeField]
    private Vector2Int startingBoard;
    public Vector2Int StartingBoard { get => startingBoard; }

    [SerializeField]
    private Vector2Int startingPos;
    public Vector2Int StartingPos { get => startingPos; }
}
