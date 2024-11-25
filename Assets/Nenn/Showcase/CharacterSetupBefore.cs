using UnityEngine;
using System.Collections.Generic;

public class CharacterSetupBefore : MonoBehaviour
{
    // General Character Information
    [SerializeField]
    private string CharacterName;

    [SerializeField]
    private HealthSettings healthSettings;

    [SerializeField]
    private float Speed = 5f;

    // Flying Abilities (always visible)
    [SerializeField]
    private bool CanFly;
    [SerializeField]
    private float FlyingSpeed = 10f;
    [SerializeField]
    private float FlyingDuration = 60f;

    // Special Attack (always visible)
    [SerializeField]
    private bool HasSpecialAttack;
    [SerializeField]
    private int AttackPower = 50;

    // Weapons (unfiltered list)
    [SerializeField]
    private List<Weapon> Weapons = new List<Weapon>
    {
        new Weapon { WeaponName = "Sword", ShouldUse = false },
        new Weapon { WeaponName = "Bow", ShouldUse = false },
        new Weapon { WeaponName = "Staff", ShouldUse = false },
        new Weapon { WeaponName = "Dagger", ShouldUse = false }
    };
}

// Weapon struct for the weapon list
[System.Serializable]
public struct Weapon
{
    [SerializeField]
    internal string WeaponName;
    [SerializeField]
    internal bool ShouldUse;
}

[System.Serializable]
public class HealthSettings
{
    [SerializeField]
    private float maxHealth = 200;
    [SerializeField]
    private float currentHealth = 150;
}