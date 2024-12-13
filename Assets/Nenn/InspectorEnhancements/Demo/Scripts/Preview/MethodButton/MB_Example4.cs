using Nenn.InspectorEnhancements.Runtime.Attributes;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Demo.Scripts.Preview.MethodButton {
    public class MethodButtonExample4 : MonoBehaviour {

        #pragma warning disable CS0414 // Disable unused value warnings
        [SerializeField]
        private int damageAmount = 10;
        #pragma warning restore CS0414 // Reenable unused value warnings

        [MethodButton("damageAmount")]
        public void ApplyDamage(int amount) {
            // Example health variable for demonstration; replace as needed
            int health = 100;
            health -= amount;
            Debug.Log($"Damage applied: {amount}, Health remaining: {health}");
        }
    }
}
