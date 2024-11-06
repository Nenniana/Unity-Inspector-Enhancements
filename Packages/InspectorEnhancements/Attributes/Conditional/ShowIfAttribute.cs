namespace InspectorEnhancements
{
    public class ShowIfAttribute : ConditionalAttribute
    {
        public ShowIfAttribute () {}
        public ShowIfAttribute(string condition) : base(condition)
        {
            MethodName = condition;
            Parameters = new object[0];
        }
    
        public ShowIfAttribute(string condition, params object[] parameters)  : base(condition, parameters)
        {
            MethodName = condition;
            Parameters = parameters;
        }
    }
}