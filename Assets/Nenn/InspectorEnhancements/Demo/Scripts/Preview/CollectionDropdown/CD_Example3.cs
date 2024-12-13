using System.Linq;
using Nenn.InspectorEnhancements.Runtime.Attributes;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Demo.Scripts.Preview.CollectionDropdown {
    public class CollectionDropdownExample3 : MonoBehaviour {
        [CollectionDropdown("GetLevelRange", 1, 10)]
        [SerializeField]
        private int selectedLevel;

        private int[] GetLevelRange(int start, int end) {
            return Enumerable.Range(start, end - start + 1).ToArray();
        }
    }
}
