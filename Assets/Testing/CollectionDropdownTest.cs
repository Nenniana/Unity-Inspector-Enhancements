using System.Collections.Generic;
using System.Linq;
using InspectorEnhancements.Attributes;
using UnityEngine;

namespace Nenn.InspectorEnhancements
{
    public class ArrayDropdownTest : MonoBehaviour 
    {
        // Test case 1: Using a string array directly
        [CollectionDropdown("colors")]
        public string selectedColor;

        // Test case 2: Using an integer array directly
        [CollectionDropdown("numbers")]
        public int selectedNumber;

        // Test case 3: Using a List<int> field
        [CollectionDropdown("numberList")]
        public int selectedFromList;

        // Test case 4: Using a method that returns a string array
        [CollectionDropdown("GetColorOptions")]
        public string selectedColorFromMethod;

        // Test case 5: Using a method that returns an integer array
        [CollectionDropdown("GetNumberOptions")]
        public int selectedNumberFromMethod;

        // Test case 6: Using a method with parameters to generate a List<int>
        [CollectionDropdown("GenerateNumberRange", 1, 10)]
        public int selectedNumberInRange;

        // Test case 7: Using an enum type
        [CollectionDropdown("customEnums")]
        public SampleEnum selectedEnumValue;

        // Test case 8: Using an enum type by method
        [CollectionDropdown("GetEnumValues")]
        public SampleEnum selectedEnumValueMethod;

        // Test case 9: Using a List of a custom class
        [CollectionDropdown("customObjects")]
        public CustomClass selectedCustomObject;

        // Test case 10: Using a method to retrieve a List of custom classes
        [CollectionDropdown("GetCustomObjectOptions")]
        public CustomClass selectedCustomObjectFromMethod;

        // Additional fields to test the drawer with diverse types
        [CollectionDropdown("stringList")]
        public string selectedStringFromList;

        [CollectionDropdown("GetStringList")]
        public string selectedStringFromListMethod;

        // Example data fields
        private string[] colors = { "Red", "Blue", "Green", "Yellow", "Purple", "Orange" };
        private int[] numbers = { 1, 2, 3, 4, 5, 6 };
        private List<int> numberList = new List<int> { 1, 2, 3, 4, 5, 6 };
        private List<string> stringList = new List<string> { "Alpha", "Beta", "Gamma", "Delta" };
        private List<CustomClass> customObjects = new List<CustomClass>
        {
            new CustomClass("Object 1"),
            new CustomClass("Object 2"),
            new CustomClass("Object 3")
        };
        private List<SampleEnum> customEnums = new List<SampleEnum>
        {
            SampleEnum.OptionB,
            SampleEnum.OptionC,
            SampleEnum.OptionA
        };

        // Methods used by the dropdown attributes
        private string[] GetColorOptions() 
        {
            return colors;
        }

        private int[] GetNumberOptions() 
        {
            return numbers;
        }

        private List<int> GenerateNumberRange(int fromValue, int toValue)
        {
            return Enumerable.Range(fromValue, toValue).ToList();
        }

        private List<string> GetStringList()
        {
            return stringList;
        }

        private SampleEnum[] GetEnumValues()
        {
            return (SampleEnum[])System.Enum.GetValues(typeof(SampleEnum));
        }

        private List<CustomClass> GetCustomObjectOptions()
        {
            return customObjects;
        }
    }

    // Example enum for testing dropdown with enums
    public enum SampleEnum
    {
        OptionA,
        OptionB,
        OptionC,
        OptionD
    }

    // Example custom class for testing dropdown with custom class instances
    [System.Serializable]
    public class CustomClass
    {
        public string Name;

        public CustomClass(string name)
        {
            Name = name;
        }

        // Override ToString to display the name in the dropdown
        public override string ToString()
        {
            return Name;
        }
    }
}
