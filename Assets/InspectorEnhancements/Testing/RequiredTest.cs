using UnityEngine;

namespace InspectorEnhancements.Testing
{
    public class RequiredTest : MonoBehaviour
    {
        // 1. Test [Required] on its own
        [Required]
        public GameObject requiredObject;
    
        // 2. Combine [Required] with [HideLabel]
        [Required, HideLabel] 
        public GameObject requiredWithHideLabel;
    
        // 3. Combine [Required] with [SerializeField], and [Header] for private field
        [Required, SerializeField, Header("Required Serialized GameObject")]
        private GameObject requiredSerializeHeader;
    }
}