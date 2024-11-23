using UnityEngine;
using InspectorEnhancements; // Namespace for your attributes

public class ShowcaseExample : MonoBehaviour
{
    [InlineProperty]
    public NestedData nestedExample;

    public bool showAdvancedOptions;

    [ShowIf("showAdvancedOptions")]
    public string advancedOption = "This is an advanced setting";

    [CollectionDropdown("availableOptions")]
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

    private string[] availableOptions = { "Option 1", "Option 2", "Option 3" };
}

[System.Serializable]
public class NestedData
{
    [SerializeField]
    private string fieldOne = "This is a nested field";

    [SerializeField]
    private int fieldTwo = 42;
}
