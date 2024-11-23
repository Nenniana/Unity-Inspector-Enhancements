
using UnityEngine;

namespace InspectorEnhancements.Preview {
    public class MethodButtonExample3 : MonoBehaviour {
        [MethodButton("Hello World", 3)]
        public void PrintMessageMultipleTimes(string message, int repeatCount) {
            for (int i = 0; i < repeatCount; i++) {
                Debug.Log(message);
            }
        }
    }
}
