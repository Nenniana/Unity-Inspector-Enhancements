
using UnityEngine;
using System.Collections.Generic;

namespace InspectorEnhancements.Preview {
    public class CollectionDropdownExample1 : MonoBehaviour {
        [CollectionDropdown("colors")]
        [SerializeField]
        private string selectedColor;

        private string[] colors = { "Red", "Blue", "Green", "Yellow", "Purple", "Orange" };
    }
}
