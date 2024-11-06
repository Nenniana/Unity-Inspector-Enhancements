namespace InspectorEnhancements
{
    public abstract class ConditionalAttribute : CustomPropertyAttribute, IMethodOwner
    {
        public string MethodName { get; protected set; }
        public object[] Parameters { get; protected set; }

        public ConditionalAttribute() {}
    
        public ConditionalAttribute(string condition)
        {
            MethodName = condition;
            Parameters = new object[0];
        }
    
        public ConditionalAttribute(string condition, params object[] parameters)
        {
            MethodName = condition;
            Parameters = parameters;
        }
    }
}