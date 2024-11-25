using Nenn.InspectorEnhancements.Attributes;
using UnityEngine;

namespace InspectorEnhancements.Demo.Scripts.Preview.HideLabel {
    public class HideLabelExample1 : MonoBehaviour {
        [HideLabel]
        [SerializeField]
        private float transparency;
    }
}
