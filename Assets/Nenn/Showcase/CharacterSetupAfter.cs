using UnityEngine;
using System.Collections.Generic;
using Nenn.InspectorEnhancements.Runtime.Attributes;
using Nenn.InspectorEnhancements.Runtime.Attributes.Conditional;

// Assuming this is your namespace
namespace Nenn.Showcase
{
    public class CharacterSetupAfter : MonoBehaviour
    {
#pragma warning disable CS0414 // Disable unused value warnings
        // General Character Information
        [HideLabel]
        [SerializeField]
        private string characterName;

        [InlineProperty]
        [SerializeField]
        private HealthSettings healthSettings;

        [SerializeField]
        private float speed = 5f;

        [SerializeField]
        private bool canFly;

        
        [ShowIf("canFly")]
        [SerializeField]
        private float flyingSpeed;
        
        [ShowIf("canFly")]
        [SerializeField]
        private float flyingDuration;

        // Special Attack (only visible when HasSpecialAttack is true)
        [SerializeField]
        private bool hasSpecialAttack;
        
        [ShowIf("hasSpecialAttack")]
        [SerializeField]
        private int attackPower = 50;

        // Weapons (displayed with a dropdown for selection)
        [CollectionDropdown("_availableWeapons")]
        [SerializeField]
        private string selectedWeapon;

        // Available weapons list for dropdown
        private List<string> _availableWeapons = new List<string>
        {
            "Sword",
            "Bow",
            "Staff",
            "Dagger"
        };
#pragma warning restore CS0414 // Reenable unused value warnings
    }
}
