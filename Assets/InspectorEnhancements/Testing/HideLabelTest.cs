using UnityEngine;

namespace InspectorEnhancements.Testing
{
    public class HideLabelTest : MonoBehaviour
    {
        // 1. Test [HideLabel] on its own
        [HideLabel]
        public string labelHiddenField = "No Label";

        // 2. Combine [HideLabel] with [Range], [SerializeField], and [Tooltip] for private field
        [HideLabel, Range(0, 100), SerializeField, Tooltip("This field has no label.")]
        private int rangedHiddenLabelField = 50;
    }
}