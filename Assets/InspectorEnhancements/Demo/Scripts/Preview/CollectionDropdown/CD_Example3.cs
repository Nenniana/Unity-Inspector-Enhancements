
using UnityEngine;
using System.Linq;

namespace InspectorEnhancements.Preview {
    public class CollectionDropdownExample3 : MonoBehaviour {
        [CollectionDropdown("GetLevelRange", 1, 10)]
        [SerializeField]
        private int selectedLevel;

        private int[] GetLevelRange(int start, int end) {
            return Enumerable.Range(start, end - start + 1).ToArray();
        }
    }
}
