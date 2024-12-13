using System.Collections.Generic;
using UnityEngine;

namespace Showcase
{
    public class CharacterSetupBefore : MonoBehaviour
    {
    #pragma warning disable CS0414 // Disable unused value warnings
        // General Character Information
        [SerializeField]
        private string characterName;
    
        [SerializeField]
        private HealthSettings healthSettings;
    
        [SerializeField]
        private float speed = 5f;
    
        // Flying Abilities (always visible)
        [SerializeField]
        private bool canFly;
        [SerializeField]
        private float flyingSpeed = 10f;
        [SerializeField]
        private float flyingDuration = 60f;
    
        // Special Attack (always visible)
        [SerializeField]
        private bool hasSpecialAttack;
        [SerializeField]
        private int attackPower = 50;
    
        // Weapons (unfiltered list)
        [SerializeField]
        private List<Weapon> weapons = new List<Weapon>
        {
            new Weapon { weaponName = "Sword", shouldUse = false },
            new Weapon { weaponName = "Bow", shouldUse = false },
            new Weapon { weaponName = "Staff", shouldUse = false },
            new Weapon { weaponName = "Dagger", shouldUse = false }
        };
    #pragma warning restore CS0414 // Reenable unused value warnings
    }
    
    // Weapon struct for the weapon list
    [System.Serializable]
    public struct Weapon
    {
        [SerializeField]
        internal string weaponName;
        [SerializeField]
        internal bool shouldUse;
    }
    
    [System.Serializable]
    public class HealthSettings
    {
    #pragma warning disable CS0414 // Disable unused value warnings
        [SerializeField]
        private float maxHealth = 200;
        [SerializeField]
        private float currentHealth = 150;
    #pragma warning restore CS0414 // Reenable unused value warnings
    }
}