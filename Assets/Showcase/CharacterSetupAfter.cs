using UnityEngine;
using System.Collections.Generic;
using Nenn.InspectorEnhancements;
using System;
using InspectorEnhancements.Attributes;
using InspectorEnhancements.Attributes.Conditional;

// Assuming this is your namespace

public class CharacterSetupAfter : MonoBehaviour
{
    // General Character Information
    [HideLabel]
    [SerializeField]
    private string CharacterName;

    [InlineProperty]
    [SerializeField]
    private HealthSettings healthSettings;

    [SerializeField]
    private float Speed = 5f;

    [SerializeField]
    private bool CanFly;

    [ShowIf("CanFly")]
    [SerializeField]
    private float FlyingSpeed;

    [ShowIf("CanFly")]
    [SerializeField]
    private float FlyingDuration;

    // Special Attack (only visible when HasSpecialAttack is true)
    [SerializeField]
    private bool HasSpecialAttack;

    [ShowIf("HasSpecialAttack")]
    [SerializeField]
    private int AttackPower = 50;

    // Weapons (displayed with a dropdown for selection)
    [CollectionDropdown("AvailableWeapons")]
    [SerializeField]
    private string SelectedWeapon;

    // Available weapons list for dropdown
    private List<string> AvailableWeapons = new List<string>
    {
        "Sword",
        "Bow",
        "Staff",
        "Dagger"
    };
}