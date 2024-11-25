using Nenn.InspectorEnhancements.Attributes;
using UnityEngine;

namespace InspectorEnhancements.Demo.Scripts.Preview.InlineProperty {
    public class InlinePropertyExample2 : MonoBehaviour {
        [InlineProperty]
        [SerializeField]
        private TestClass defaultClass;
    }
    
    [System.Serializable]
    public class TestClass {
        public bool boolField;
        public Quaternion quaternionField;
    }
}
