using System.Reflection;

namespace Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.MemberRenderers.IParameterRenderer.Base
{
    public interface IParameterRenderer
    {
        public void DrawParameterFields(ParameterInfo[] parameters, ref object[] parameterValues);
    }
}