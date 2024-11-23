
using UnityEngine;

namespace InspectorEnhancements.Preview {
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
