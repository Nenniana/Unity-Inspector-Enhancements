using Nenn.InspectorEnhancements.Runtime.Attributes;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Demo.Scripts.Preview.MethodButton {
    public class MethodButtonExample1 : MonoBehaviour {
        [MethodButton]
        public void LogMessage() {
            Debug.Log("Button clicked!");
        }
    }
}
