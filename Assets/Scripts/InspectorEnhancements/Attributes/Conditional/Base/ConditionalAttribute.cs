namespace InspectorEnhancements
{
    public abstract class ConditionalAttribute : CustomPropertyAttribute
    {
        public string Condition { get; protected set; }
        public object[] Parameters { get; protected set; }
    
        public ConditionalAttribute(string condition)
        {
            Condition = condition;
            Parameters = new object[0];
        }
    
        public ConditionalAttribute(string condition, params object[] parameters)
        {
            Condition = condition;
            Parameters = parameters;
        }
    }
}