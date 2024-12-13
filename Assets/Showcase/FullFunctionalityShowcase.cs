using Nenn.InspectorEnhancements.Runtime.Attributes;
using Nenn.InspectorEnhancements.Runtime.Attributes.Conditional;
using UnityEngine;

namespace Showcase
{
    public class FullFunctionalityShowcase : MonoBehaviour
    {
        [InlineProperty]
        public NestedData nestedExample;

        public bool showAdvancedOptions;

        [ShowIf("showAdvancedOptions")]
        public string advancedOption = "This is an advanced setting";

        [CollectionDropdown("_availableOptions")]
        public string selectedOption;

        [Required]
        public GameObject requiredObject;

        [HideLabel]
        public string labelHiddenExample = "No Label Here";

        [MethodButton]
        public void ToggleActiveState()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        private string[] _availableOptions = { "Option 1", "Option 2", "Option 3" };
    }

    [System.Serializable]
    public class NestedData
    {
#pragma warning disable CS0414 // Disable unused value warnings
        [SerializeField]
        private string fieldOne = "This is a nested field";

        [SerializeField]
        private int fieldTwo = 42;
#pragma warning restore CS0414 // Reenable unused value warnings
    }
}
