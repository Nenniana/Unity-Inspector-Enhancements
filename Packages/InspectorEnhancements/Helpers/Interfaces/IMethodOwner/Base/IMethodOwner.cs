namespace InspectorEnhancements
{
    public interface IMethodOwner
    {
        public string Condition { get; }
        public object[] Parameters { get; }
    }
}