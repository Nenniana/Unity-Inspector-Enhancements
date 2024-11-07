using System.Reflection;

namespace InspectorEnhancements
{
    public interface IParameterRenderer
    {
        public void DrawParameterFields(ParameterInfo[] parameters, ref object[] parameterValues);
    }
}