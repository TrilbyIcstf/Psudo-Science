using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Dungeon Board", menuName = "ScriptableObjects/New Dungeon Board", order = 7)]
[System.Serializable]
public class Dungeon_Board_Layout : ScriptableObject
{
    /// <summary>
    /// Map will coralate each pixel with a tile on the board, using color value to determine tile data
    /// R: Tile type (0 = empty, 1 = floor, 2 = wall, 3 = door)
    /// G: Tile varient to use from the tileset's array
    /// </summary>
    [SerializeField]
    private Texture2D mapImage;

    [SerializeField]
    private List<DungeonEnemy> baseEnemies;

    [SerializeField]
    private DoorConnectionDictionary doorConnections;

    public Texture2D MapImage { get => mapImage; }
    public List<DungeonEnemy> BaseEnemies { get => baseEnemies; }
    public DoorConnectionDictionary DoorConnections { get => doorConnections; }
}
