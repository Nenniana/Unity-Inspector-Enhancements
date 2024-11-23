
using UnityEngine;

namespace InspectorEnhancements.Preview {
    public class HideLabelExample2 : MonoBehaviour {
        [HideLabel]
        [SerializeField]
        private Color mainColor;
    }
}
