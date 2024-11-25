namespace Nenn.InspectorEnhancements.Helpers.Interfaces.IMemberOwner
{
    public interface IMemberOwner : IParameterOwner.IParameterOwner
    {
        public string MemberName { get; }
    }
}