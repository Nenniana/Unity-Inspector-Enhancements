using Nenn.InspectorEnhancements.Runtime.Attributes;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Demo.Scripts.Preview.CollectionDropdown {
    public class CollectionDropdownExample1 : MonoBehaviour {
        [CollectionDropdown("colors")]
        [SerializeField]
        private string selectedColor;

        private string[] colors = { "Red", "Blue", "Green", "Yellow", "Purple", "Orange" };
    }
}
