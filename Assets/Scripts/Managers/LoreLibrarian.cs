using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class LoreLibrarian : MonoBehaviour
{
    public MoveNameDictionary moveRepository;
    public BestiaryDictionary enemyRepository;
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
            return value.GetComponent<Move_Dad>().MoveInfo;
        }

        return null;
    }
}

[Serializable]
public class BestiaryDictionary : GenericDictionary<Bestiary, GameObject> { }