
using UnityEngine;

namespace InspectorEnhancements.Preview {
    public class HideLabelExample1 : MonoBehaviour {
        [HideLabel]
        [SerializeField]
        private float transparency;
    }
}
