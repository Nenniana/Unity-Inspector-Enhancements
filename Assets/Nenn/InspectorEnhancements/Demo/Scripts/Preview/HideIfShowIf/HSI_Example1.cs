using Nenn.InspectorEnhancements.Runtime.Attributes.Conditional;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Demo.Scripts.Preview.HideIfShowIf {
    public class HideIfShowIfExample1 : MonoBehaviour {
        [SerializeField]
        private bool shouldHide;

        #pragma warning disable CS0414 // Disable unused value warnings
        [HideIf("shouldHide")]
        [SerializeField]
        private int hiddenField = 20;
        #pragma warning restore CS0414 // Reenable unused value warnings
    }
}
