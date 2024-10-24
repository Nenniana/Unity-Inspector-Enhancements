namespace InspectorEnhancements
{
    public class ShowIfAttribute : ConditionalAttribute
    {
        public ShowIfAttribute () {}
        public ShowIfAttribute(string condition) : base(condition)
        {
            Condition = condition;
            Parameters = new object[0];
        }
    
        public ShowIfAttribute(string condition, params object[] parameters)  : base(condition, parameters)
        {
            Condition = condition;
            Parameters = parameters;
        }
    }
}