namespace InspectorEnhancements
{
    public interface IMemberOwner : IParameterOwner
    {
        public string MemberName { get; }
    }
}