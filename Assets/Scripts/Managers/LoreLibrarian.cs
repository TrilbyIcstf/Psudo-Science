using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class LoreLibrarian : MonoBehaviour
{
    [Header("Prefabs")]
    public MoveNameDictionary moveRepository;
    public EnemyMoveNameDictionary enemyMoveRepository;
    public BestiaryDictionary enemyRepository;

    [Header("Sprites")]
    public TileSpriteDictionary tileSprites;
    public StatusIconDictionary statusIcons;
}

public class GenericDictionary<K, V>
{
    [Serializable]
    protected struct KeyValuePair
    {
        public K key;
        public V value;
    }

    [SerializeField]
    protected List<KeyValuePair> keyValuePairs = new List<KeyValuePair>();

    public V GetValue(K key)
    {
        return keyValuePairs.FirstOrDefault(kv => EqualityComparer<K>.Default.Equals(kv.key, key)).value;
    }

    public bool ContainsKey(K key)
    {
        return keyValuePairs.Any(kv => kv.key.Equals(key));
    }

    public List<K> Keys()
    {
        return keyValuePairs.Select(kv => kv.key).ToList();
    }
}

[Serializable]
public class MoveNameDictionary : GenericDictionary<MoveName, GameObject>
{
    public Move_Information GetInformation(MoveName key)
    {
        GameObject value = GetValue(key);
        if (value != null)
        {
            return value.GetComponent<Player_Move>().MoveInfo;
        }

        return null;
    }
}

[Serializable]
public class EnemyMoveNameDictionary : GenericDictionary<EnemyMoveName, GameObject> { }

[Serializable]
public class BestiaryDictionary : GenericDictionary<Bestiary, GameObject> { }

[Serializable]
public class TileSpriteDictionary : GenericDictionary<TColor, Sprite> { }

[Serializable]
public class StatusIconDictionary : GenericDictionary<StatusEffect, Sprite> { }

[Serializable]
public class ChessPrefabDictionary : GenericDictionary<ChessPiece, GameObject> { }

[Serializable]
public class DoorConnectionDictionary : GenericDictionary<Vector2Int, DoorConnection> { }

[Serializable]
public class DungeonLayoutDictionary : GenericDictionary<Vector2Int, Dungeon_Board_Layout> { }

[Serializable]
public class MoveTypeIconDictionary : GenericDictionary<MoveType, Sprite> { }