using UnityEngine;

namespace InspectorEnhancements.Testing
{
    public class ShowIfTest : MonoBehaviour
    {
        // Exposed properties to control ShowIf and HideIf behaviors in the inspector
        public bool shouldShow;
        public bool shouldHide;

        // 1. Test [ShowIf] on its own
        [ShowIf("shouldShow")]
        public int visibleField = 20;

        // 2. Test [ShowIf] on custom struct (Not currently supported - shows error message)
        [ShowIf("showIfStructNotNull")]
        public TestStruct showIfStructNotNull;

        // 3. Test [ShowIf] on custom class (Not currently supported - shows error message)
        [ShowIf("showIfClassNotNull")]
        public TestObject showIfClassNotNull;

        // 4. Test [ShowIf] on other custom class (Not currently supported - shows error message)
        [ShowIf("showIfClassNotNull")]
        public TestObject showIfOtherClassNotNull;

        // 5. Test [ShowIf] on MonoBehaviour
        [ShowIf("showMonoIfNotNull")]
        public MonoBehaviour showMonoIfNotNull;

        // 6. Test [ShowIf] with method
        [ShowIf("ReturnShouldShow")]
        public int visibleFieldMethod = 20;

        // 7. Test [ShowIf] with parameter method
        [ShowIf("ReturnBool", "shouldShow")]
        public int visibleFieldParameterMethod = 20;

        // 8. Combine [ShowIf] with [HideIf]
        [ShowIf("shouldShow"), HideIf("shouldHide")]
        public string showIfWithHideIf = "Conditionally Visible";

        // 9. Combine [ShowIf] with [HideLabel]
        [ShowIf("shouldShow"), HideLabel]
        public string showIfWithHideLabel = "Hidden Field Label";

        // 10. Combine [ShowIf] with [Required]
        [ShowIf("shouldShow"), Required]
        public GameObject requiredField;

        // 11. Combine [ShowIf] with [Range], [SerializeField], and [Tooltip]
        [ShowIf("shouldShow"), Range(0, 100), SerializeField, Tooltip("This field is shown if 'shouldShow' is true.")]
        public int rangedVisibleField = 50;

        // 12. [ShowIf] with default parameters
        [ShowIf("TestMethod", 5)]
        public string methodDefaultParametersField;

        // 13. [ShowIf] with full parameters
        [ShowIf("TestMethod", 5, "methodFilledDefaultParametersField", false)]
        public string methodFilledDefaultParametersField = "Lengthy sentence to overwrite default value.";

        private bool ReturnShouldShow()
        {
            return shouldShow;
        }

        private bool ReturnBool(bool parameterBool)
        {
            return parameterBool;
        }

        public bool TestMethod(int a, string b = "default", bool c = false)
        {
            if (a >= b.Length && !c)
                return false;
            
            return true;
        }
    }
}
