using UnityEngine;

namespace InspectorEnhancements
{
    public class MethodButtonTest : MonoBehaviour
    {
        // 1. Parameterless method test
        [MethodButton]
        public void PrintDebug()
        {
            Debug.Log("You pressed the button.");
        }

        // 2. Single parameter (string) without default value
        [MethodButton]
        public void PrintTextDebug(string text)
        {
            Debug.Log($"Text: {text}");
        }

        // 3. Single parameter (string) with default value
        [MethodButton(false)]
        public void PrintDefaultTextDebug(string text = "Default text right here.")
        {
            Debug.Log($"Text with default: {text}");
        }

        // 4. Single parameter (bool)
        [MethodButton]
        public void PrintBoolDebug(bool value)
        {
            Debug.Log($"Bool value: {value}");
        }

        // 5. Single parameter (bool) with default parameter supplied in attribute
        [MethodButton(true, true)]
        public void PrintBoolParameterDebug(bool boolValue)
        {
            Debug.Log($"Bool value with parameter in attribute: {boolValue}");
        }

        // 6. Integer parameter referencing a class field by name
        [MethodButton(true, "testInt")]
        public void PrintFieldReferenceParameterDebug(int intValue)
        {
            Debug.Log($"Int value from field reference: {intValue}");
        }

        // 7. Single parameter (custom class instance)
        [MethodButton]
        public void PrintCustomClassDebug(CustomClass customClass)
        {
            Debug.Log($"Custom Class - Int: {customClass.nestedInt}, String: {customClass.nestedString}");
        }

        // Additional Testing Scenarios:

        // 8. Multiple parameters of different types
        [MethodButton]
        public void PrintMultipleParamsDebug(string text, int number, bool flag)
        {
            Debug.Log($"Text: {text}, Number: {number}, Bool: {flag}");
        }

        // 9. Multiple parameters with default values
        [MethodButton]
        public void PrintMultipleDefaultParamsDebug(string text = "Default text", int number = 123, bool flag = true)
        {
            Debug.Log($"Text with defaults: {text}, Number with default: {number}, Bool with default: {flag}");
        }

        // 10. Testing method with float parameter
        [MethodButton]
        public void PrintFloatDebug(float value)
        {
            Debug.Log($"Float value: {value}");
        }

        // 11. Testing method with custom class field reference
        [MethodButton(true, "customClassInstance")]
        public void PrintCustomClassFieldReferenceDebug(CustomClass classInstance)
        {
            Debug.Log($"Referenced Custom Class - Int: {classInstance.nestedInt}, String: {classInstance.nestedString}");
        }

        // 12. Method with parameter of unsupported type (e.g., GameObject) to test handling
        [MethodButton]
        public void PrintGameObjectDebug(GameObject gameObject)
        {
            Debug.Log($"GameObject Name: {gameObject.name}");
        }

        #pragma warning disable CS0414
        private int testInt = 144;
        private float testFloat = 3.14f;
        #pragma warning restore CS0414

        [System.Serializable]
        public class CustomClass
        {
            public int nestedInt = 42;
            public string nestedString = "Nested Hello";

            [MethodButton]
            public void PrintDebug44()
            {
                Debug.Log("You pressed the button.");
            }
        }

        [SerializeField] private CustomClass customClassInstance = new CustomClass();
    }
}
