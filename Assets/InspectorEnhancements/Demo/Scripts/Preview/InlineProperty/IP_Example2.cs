
using UnityEngine;

namespace InspectorEnhancements.Preview {
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
