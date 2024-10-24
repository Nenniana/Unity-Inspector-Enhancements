namespace InspectorEnhancements
{
    public class HideIfAttribute : ConditionalAttribute
    {
        public HideIfAttribute() {}
        public HideIfAttribute(string condition) : base(condition)
        {
            Condition = condition;
            Parameters = new object[0];
        }
    
        public HideIfAttribute(string condition, params object[] parameters)  : base(condition, parameters)
        {
            Condition = condition;
            Parameters = parameters;
        }
    }
}