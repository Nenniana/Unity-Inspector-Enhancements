using Nenn.InspectorEnhancements.Runtime.Attributes;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Demo.Scripts.Preview.HideLabel {
    public class HideLabelExample2 : MonoBehaviour {
        [HideLabel]
        [SerializeField]
        private Color mainColor;
    }
}
