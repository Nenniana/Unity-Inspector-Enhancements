using UnityEngine;

namespace InspectorEnhancements.Testing
{
    public class HideIfTest : MonoBehaviour
    {
        // Exposed properties to control HideIf and ShowIf behaviors in the inspector
        public bool shouldHide;
        public bool shouldShow;
    
        // 1. Test [HideIf] on its own
        [HideIf("shouldHide")]
        public int hiddenField = 20;

        // 2. Test [HideIf] on struct (Not currently supported - shows error message)
        [HideIf("hideIfStructNotNull")]
        public TestStruct hideIfStructNotNull;

        // 3. Test [HideIf] on class (Not currently supported - shows error message)
        [HideIf("hideIfClassNotNull")]
        public TestObject hideIfClassNotNull;

        // 4. Test [HideIf] on MonoBehaviour
        [HideIf("hideMonoIfNotNull")]
        public MonoBehaviour hideMonoIfNotNull;

        // 5. Test [HideIf] with method
        [HideIf("ReturnShouldHide")]
        public int hiddenFieldMethod = 20;

        // 6. Test [HideIf] with parameter method 
        [HideIf("ReturnBool", "shouldHide")]
        public int hiddenFieldParameterMethod = 20;
    
        // 7. Combine [HideIf] with [ShowIf]
        [HideIf("shouldHide"), ShowIf("shouldShow")]
        public string hideIfWithShowIf = "Conditionally Visible";
    
        // 8. Combine [HideIf] with [HideLabel]
        [HideIf("shouldHide"), HideLabel]
        public string hideIfWithHideLabel = "Hidden Field Label";
    
        // 9. Combine [HideIf] with [Required]
        [HideIf("shouldHide"), Required]
        public GameObject requiredField;
    
        // 10. Combine [HideIf] with [Range], [SerializeField], and [Tooltip]
        [HideIf("shouldHide"), Range(0, 100), SerializeField, Tooltip("This field is hidden if 'shouldHide' is true.")]
        public int rangedHiddenField = 50;

        // 11. [HideIf] with default parameters
        [HideIf("TestMethod", 5)]
        public string methodDefaultParametersField;

        // 12. [HideIf] with full parameters
        [HideIf("TestMethod", 5, "methodFilledDefaultParametersField", false)]
        public string methodFilledDefaultParametersField = "Lengthy sentence to overwrite default value.";

        private bool ReturnShouldHide () {
            return shouldHide;
        }

        private bool ReturnBool (bool parameterBool) {
            return parameterBool;
        }

        public bool TestMethod(int a, string b = "default", bool c = false)
        {
            if (a >= b.Length && !c)
                return true;
            
            return false;
        }
    }

    [System.Serializable]
    public struct TestStruct {
        public int testInt;
    }

    [System.Serializable]
    public class TestObject {
        public int testInt;
    }
}