using UnityEngine;

namespace InspectorEnhancements
{
    public class InlinePropertyTest : MonoBehaviour
    {
        public bool shouldHide;
        public bool shouldShow;

        // Struct to test inline property with and without custom names
        [System.Serializable]
        [InlineProperty(InlinePropertyNameMode.HeaderName, "Custom Struct Name")]
        public struct TestStruct
        {
            [Required][HideLabel]
            public GameObject gameObject;
            public int intField;
            public float floatField;
            public Vector3 vectorField;
        }

        // Another struct without inline property attribute on struct itself
        [System.Serializable]
        public struct AnotherStruct
        {
            public string stringField;
            public Color colorField;
        }

        // Class to test inline property with custom names and different modes
        [System.Serializable]
        [InlineProperty(InlinePropertyNameMode.PrependName)]
        public class TestClass
        {
            public bool boolField;
            public Quaternion quaternionField;
        }

        // Test cases

        // Inline property at the field level (with custom name)
        [InlineProperty("Inline Struct With Header")]
        public TestStruct structWithHeader;

        // Inline property with the default behavior (no custom name)
        [InlineProperty]
        public TestStruct defaultStruct;

        // Inline property with prepended field names
        [InlineProperty(InlinePropertyNameMode.PrependName)]
        public AnotherStruct structWithPrepend;

        // Inline property at the class level with field level override (no custom name)
        [InlineProperty]
        public TestClass defaultClass;

        // Inline property at the class level with field-level custom name
        [InlineProperty("Inline Class With Header")]
        public TestClass classWithHeader;

        // Inline property with the default behavior (no custom name)
        [InlineProperty][HideIf("shouldHide")]
        public AnotherStruct hideIfStruct;

        // Inline property at the class level with field level override (no custom name)
        [InlineProperty][ShowIf("shouldShow")]
        public TestClass showIfClass;

        // Fields with no inline property (should use default Unity rendering)
        public AnotherStruct structNoInline;
        public TestClass classNoInline;
    }
}