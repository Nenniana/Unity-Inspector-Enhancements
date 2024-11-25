using System.Reflection;

namespace InspectorEnhancements.Helpers.MemberRenderers.IParameterRenderer.Base
{
    public interface IParameterRenderer
    {
        public void DrawParameterFields(ParameterInfo[] parameters, ref object[] parameterValues);
    }
}