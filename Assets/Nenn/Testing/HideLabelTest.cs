using System;
using Nenn.InspectorEnhancements.Runtime.Attributes;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Testing
{
    public class HideLabelTest : MonoBehaviour
    {
        // 1. Test [HideLabel] on its own
        [HideLabel]
        public string labelHiddenField = "No Label";

        #pragma warning disable CS0414 // Disable unused value warnings
        // 2. Combine [HideLabel] with [Range], [SerializeField], and [Tooltip] for private field
        [HideLabel, Range(0, 100), SerializeField, Tooltip("This field has no label.")]
        private int rangedHiddenLabelField = 50;
        #pragma warning restore CS0414 // Reenable unused value warnings

        [HideLabel]
        public MyCustomClass customSettings;
    }

    [Serializable]
    public class MyCustomClass
    {
        public int number;
        public string text;
    } 
}