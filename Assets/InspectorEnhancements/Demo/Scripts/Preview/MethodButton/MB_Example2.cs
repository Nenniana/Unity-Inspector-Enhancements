
using UnityEngine;

namespace InspectorEnhancements.Preview {
    public class MethodButtonExample2 : MonoBehaviour {
        [MethodButton]
        public void SetPlayerStats(int level, string playerName) {
            Debug.Log($"Setting player stats: Level - {level}, Name - {playerName}");
        }
    }
}
