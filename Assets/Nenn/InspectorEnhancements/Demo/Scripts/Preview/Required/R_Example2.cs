using Nenn.InspectorEnhancements.Runtime.Attributes;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Demo.Scripts.Preview.Required {
    public class RequiredExample2 : MonoBehaviour {
        [Required("Custom Error Message")]
        [SerializeField]
        private Rigidbody playerRigidbody;
    }
}
