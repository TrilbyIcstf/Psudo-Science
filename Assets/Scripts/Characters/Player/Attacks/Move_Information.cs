using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stats for a player move
/// </summary>
[CreateAssetMenu(fileName = "Move Information", menuName = "ScriptableObjects/New Move", order = 5)]
[System.Serializable]
public class Move_Information : ScriptableObject
{
    [Header("Identifiers")]
    [SerializeField] private string moveName;

    [Header("Classification")]
    [SerializeField] private MoveType type;
    [SerializeField] private Element element;

    [Header("Stats")]
    [SerializeField] private float potency;
    [SerializeField] private float manaCost;

    public string MoveName { get => moveName; set => moveName = value; }
    public MoveType Type { get => type; set => type = value; }
    public Element Element { get => element; set => element = value; }
    public float Potency { get => potency; set => potency = value; }
    public float ManaCost { get => manaCost; set => manaCost = value; }
}
