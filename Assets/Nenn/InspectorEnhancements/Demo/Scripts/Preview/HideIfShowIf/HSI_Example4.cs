using Nenn.InspectorEnhancements.Runtime.Attributes.Conditional;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Demo.Scripts.Preview.HideIfShowIf {
    public class HideIfShowIfExample4 : MonoBehaviour {
        [SerializeField]
        private bool shouldHide;
        [SerializeField]
        private bool shouldShow;

        #pragma warning disable CS0414 // Disable unused value warnings
        [HideIf("shouldHide"), ShowIf("shouldShow")]
        [SerializeField]
        private string combinedField = "Conditionally Visible";
        #pragma warning restore CS0414 // Reenable unused value warnings
    }
}
