namespace InspectorEnhancements
{
    public interface IMethodOwner
    {
        public string MethodName { get; }
        public object[] Parameters { get; }
    }
}