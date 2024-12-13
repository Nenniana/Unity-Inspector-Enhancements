using Nenn.InspectorEnhancements.Runtime.Attributes;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Demo.Scripts.Preview.Required {
    public class RequiredExample1 : MonoBehaviour {
        [Required]
        [SerializeField]
        private GameObject importantObject;
    }
}
