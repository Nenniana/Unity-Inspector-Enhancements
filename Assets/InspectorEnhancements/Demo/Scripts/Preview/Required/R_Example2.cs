
using UnityEngine;

namespace InspectorEnhancements.Preview {
    public class RequiredExample2 : MonoBehaviour {
        [Required("Custom Error Message")]
        [SerializeField]
        private Rigidbody playerRigidbody;
    }
}
