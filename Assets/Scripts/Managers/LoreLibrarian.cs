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

#if UNITY_EDITOR
[CustomEditor(typeof(LoreLibrarian))]
public class LoreLibrarianEditor : Editor
{
    SerializedProperty keyValuePairs;

    void OnEnable()
    {
        keyValuePairs = serializedObject.FindProperty("moveRepository").FindPropertyRelative("keyValuePairs");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LoreLibrarian ll = (LoreLibrarian)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Dictionary Entries", EditorStyles.boldLabel);

        // Display each key-value pair field in the editor
        for (int i = 0; i < keyValuePairs.arraySize; i++)
        {
            SerializedProperty pair = keyValuePairs.GetArrayElementAtIndex(i);
            SerializedProperty key = pair.FindPropertyRelative("key");
            SerializedProperty value = pair.FindPropertyRelative("value");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(key, GUIContent.none, GUILayout.MaxWidth(120));
            EditorGUILayout.ObjectField(value, GUIContent.none, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();
        }

        // Add a button to add a new entry
        if (GUILayout.Button("Add Entry"))
        {
            ll.moveRepository.keyValuePairs.Add(new MoveNameDictionary.KeyValuePair());
        }
    }
}
#endif