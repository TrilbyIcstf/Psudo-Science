using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scriptable object to hold information on pieces of equipment
/// </summary>
[CreateAssetMenu(fileName = "Equip Information", menuName = "ScriptableObjects/New Equipment", order = 2)]
[System.Serializable]
public class Equip_Information : ScriptableObject
{
    // The type of equipment
    [Header("Type")]
    [SerializeField] private EquipType eqType = EquipType.WEAPON;

    // Information about the equip
    [Header("Info")]
    [SerializeField] private string equipName = "Broken Straight Sword";
    [SerializeField] private string equipDiscription = "Half of the blade of this straight sword is broken off.";
    [SerializeField] private Sprite equipSprite;

    // Holds unique abilities of each equipment piece
    [Header("Ability")]
    [SerializeField] private Equip_Ability ability;

    // The equipments stats
    [Header("Stats")]
    [SerializeField] private int eqHealth = 0;
    [SerializeField] private int eqAttack = 0;
    [SerializeField] private int eqDefense = 0;
    [SerializeField] private int eqMagic = 0;
    [SerializeField] private int eqMagDefense = 0;



    /// <summary>
    /// Calls the equipment's unique ability, if it has one.
    /// </summary>
    /// <param name="CT">
    /// The timing within combat this is called at.
    /// </param>
    public bool EquipActivate(CombatTiming CT)
    {
        if (ability != null)
        {
            return ability.Activate(CT);
        }
        return false;
    }

    // Public get/set
    public int Health { get => eqHealth; set => eqHealth = value; }
    public int Attack { get => eqAttack; set => eqAttack = value; }
    public int Defense { get => eqDefense; set => eqDefense = value; }
    public int Magic { get => eqMagic; set => eqMagic = value; }
    public int MagDefense { get => eqMagDefense; set => eqMagDefense = value; }
    public EquipType Type { get => eqType; }
    public string EquipName { get => equipName; }
    public string Discription { get => equipDiscription; }
    public Sprite Sprite { get => equipSprite; set => equipSprite = value; }
    public Equip_Ability EquipAbility { set => ability = value; }
}
