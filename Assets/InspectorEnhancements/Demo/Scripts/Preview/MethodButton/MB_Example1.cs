
using InspectorEnhancements.Attributes;
using UnityEngine;

namespace InspectorEnhancements.Demo.Scripts.Preview.MethodButton {
    public class MethodButtonExample1 : MonoBehaviour {
        [MethodButton]
        public void LogMessage() {
            Debug.Log("Button clicked!");
        }
    }
}
