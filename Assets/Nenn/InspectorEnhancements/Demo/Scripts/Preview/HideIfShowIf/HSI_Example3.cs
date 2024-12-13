using Nenn.InspectorEnhancements.Runtime.Attributes.Conditional;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Demo.Scripts.Preview.HideIfShowIf {
    public class HideIfShowIfExample3 : MonoBehaviour {
        [SerializeField]
        private bool shouldHide;

        #pragma warning disable CS0414 // Disable unused value warnings
        [HideIf("CheckCondition", "shouldHide")]
        [SerializeField]
        private int conditionalField = 30;
        #pragma warning restore CS0414 // Reenable unused value warnings

        private bool CheckCondition(bool parameter) {
            return parameter;
        }
    }
}
