using Nenn.InspectorEnhancements.Runtime.Attributes;
using Nenn.InspectorEnhancements.Runtime.Helpers.Enums;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Demo.Scripts.Preview.InlineProperty {
    public class InlinePropertyExample1 : MonoBehaviour {
        [InlineProperty(InlinePropertyNameMode.PrependName)]
        [SerializeField]
        private TestStructExample1 structWithPrepend;
    }
    
    [System.Serializable]
    [InlineProperty(InlinePropertyNameMode.HeaderName, "Custom Struct Header")]
    public struct TestStructExample1 {
        public int intField;
        public float floatField;
        public Vector3 vectorField;
    }
}
