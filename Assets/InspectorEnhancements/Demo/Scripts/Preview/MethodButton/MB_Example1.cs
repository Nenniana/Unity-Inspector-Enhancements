
using UnityEngine;

namespace InspectorEnhancements.Preview {
    public class MethodButtonExample1 : MonoBehaviour {
        [MethodButton]
        public void LogMessage() {
            Debug.Log("Button clicked!");
        }
    }
}
