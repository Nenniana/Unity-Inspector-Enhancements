namespace InspectorEnhancements
{
    public class ArrayDropdownAttribute : CustomPropertyAttribute, IMethodOwner
    {
        public string Condition { get; private set; }

        public object[] Parameters { get; private set; }

        public ArrayDropdownAttribute (string _condition) {
            Condition = _condition;
        }

        public ArrayDropdownAttribute (string _condition, object[] _parameters) {
            Condition = _condition;
            Parameters = _parameters;
        }
    }
}