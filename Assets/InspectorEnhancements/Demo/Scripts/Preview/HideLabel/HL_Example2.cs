using InspectorEnhancements.Attributes;
using UnityEngine;

namespace InspectorEnhancements.Demo.Scripts.Preview.HideLabel {
    public class HideLabelExample2 : MonoBehaviour {
        [HideLabel]
        [SerializeField]
        private Color mainColor;
    }
}
