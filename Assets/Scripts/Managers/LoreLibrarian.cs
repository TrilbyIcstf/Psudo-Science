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