using Nenn.InspectorEnhancements.Attributes;
using Nenn.InspectorEnhancements.Helpers.Enums;
using UnityEngine;

namespace InspectorEnhancements.Demo.Scripts.Preview.InlineProperty {
    public class InlinePropertyExample3 : MonoBehaviour {
        [InlineProperty]
        [SerializeField]
        private TestStruct structWithHeader;
    }
    
    [System.Serializable]
    [InlineProperty(InlinePropertyNameMode.HeaderName, "Custom Struct Header")]
    public struct TestStruct {
        public int intField;
        public float floatField;
        public Vector3 vectorField;
    }
}
