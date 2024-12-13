using Nenn.InspectorEnhancements.Runtime.Attributes.Conditional;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Demo.Scripts.Preview.HideIfShowIf {
    public class HideIfShowIfExample2 : MonoBehaviour {
        [SerializeField]
        private int number = 10;

        #pragma warning disable CS0414 // Disable unused value warnings
        [ShowIf("ReturnShouldShow")]
        [SerializeField]
        private string showFieldMethod = "Visible when condition is met";
        #pragma warning restore CS0414 // Reenable unused value warnings

        private bool ReturnShouldShow() {
            return number >= 5;
        }
    }
}
