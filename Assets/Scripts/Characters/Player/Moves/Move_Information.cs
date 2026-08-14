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
    [SerializeField] private MoveName moveEnum;

    [Header("Classification")]
    [SerializeField] private MoveType type;
    [SerializeField] private Element element;

    [Header("Stats")]
    [SerializeField] private float potency;
    [SerializeField] private int manaCost;

    [Header("Description")]
    [SerializeField, TextArea(3, 10)] private string description;

    public string MoveName { get => moveName; }
    public MoveName MoveEnum { get => moveEnum; }
    public MoveType Type { get => type; }
    public Element Element { get => element; }
    public float Potency { get => potency; }
    public float AdjustedPotency { get => potency / 100; }
    public int ManaCost { get => manaCost; }
    public string Description { get => description; }
}
