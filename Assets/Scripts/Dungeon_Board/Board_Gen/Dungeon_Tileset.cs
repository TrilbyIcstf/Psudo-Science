using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon Tileset", menuName = "ScriptableObjects/New Dungeon Tileset", order = 4)]
[System.Serializable]
public class Dungeon_Tileset : ScriptableObject
{
    [Header("Floor Tiles")]
    [SerializeField] private List<GameObject> whiteFloorTiles;
    [SerializeField] private List<GameObject> blackFloorTiles;

    [Header("Wall Tiles")]
    [SerializeField] private List<GameObject> whiteWallTiles;
    [SerializeField] private List<GameObject> blackWallTiles;

    [Header("Door Tiles")]
    [SerializeField] private List<GameObject> whiteDoorTiles;
    [SerializeField] private List<GameObject> blackDoorTiles;

    [Header("Chess Pieces")]
    [SerializeField] private ChessPrefabDictionary chessPieces;

    public List<GameObject> WhiteFloorTiles { get => whiteFloorTiles; }
    public List<GameObject> BlackFloorTiles { get => blackFloorTiles; }
    public List<GameObject> WhiteWallTiles { get => whiteWallTiles; }
    public List<GameObject> BlackWallTiles { get => blackWallTiles; }
    public List<GameObject> WhiteDoorTiles { get => whiteDoorTiles; }
    public List<GameObject> BlackDoorTiles { get => blackDoorTiles; }
    public ChessPrefabDictionary ChessPieces { get => chessPieces; }
}
