namespace InspectorEnhancements
{
    public class ArrayDropdownAttribute : CustomPropertyAttribute
    {
        public string Condition { get; private set; }

        public ArrayDropdownAttribute (string _condition) {
            Condition = _condition;
        }
    }
}