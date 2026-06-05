using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoreLibrarian : MonoBehaviour
{
    public MoveNameDictionary moveRepository;
}

[System.Serializable]
public class MoveNameDictionary
{
    [System.Serializable]
    public struct KeyValuePair
    {
        public MoveName key;
        public GameObject value;
    }

    public List<KeyValuePair> keyValuePairs = new List<KeyValuePair>();

    public GameObject GetValue(MoveName key)
    {
        foreach (KeyValuePair kv in this.keyValuePairs)
        {
            if (kv.key == key) { return kv.value; }
        }

        return null;
    }

    public Move_Information GetInformation(MoveName key)
    {
        GameObject value = GetValue(key);
        if (value != null)
        {
            return value.GetComponent<Move_Dad>().moveInfo;
        }

        return null;
    }
}