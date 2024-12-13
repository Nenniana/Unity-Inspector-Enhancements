using Nenn.InspectorEnhancements.Runtime.Attributes;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Demo.Scripts.Preview.MethodButton {
    public class MethodButtonExample2 : MonoBehaviour {
        [MethodButton]
        public void SetPlayerStats(int level, string playerName) {
            Debug.Log($"Setting player stats: Level - {level}, Name - {playerName}");
        }
    }
}
