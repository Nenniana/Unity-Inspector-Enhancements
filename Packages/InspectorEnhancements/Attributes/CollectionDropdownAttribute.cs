namespace InspectorEnhancements
{
    public class CollectionDropdownAttribute : CustomPropertyAttribute, IMethodOwner
    {
        public string Condition { get; private set; }

        public object[] Parameters { get; private set; }

        public CollectionDropdownAttribute (string _condition) {
            Condition = _condition;
        }

        public CollectionDropdownAttribute (string _condition, params object[] _parameters) {
            Condition = _condition;
            Parameters = _parameters;
        }
    }
}