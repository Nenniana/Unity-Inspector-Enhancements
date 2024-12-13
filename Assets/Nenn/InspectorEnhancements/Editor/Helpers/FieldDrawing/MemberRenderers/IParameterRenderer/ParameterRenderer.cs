using System.Reflection;

namespace Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.MemberRenderers.IParameterRenderer
{
    public class ParameterRenderer : Base.IParameterRenderer
    {
        private readonly IFieldDrawer.Base.IFieldDrawer _fieldDrawer;

        public ParameterRenderer(IFieldDrawer.Base.IFieldDrawer fieldDrawer) 
        {
            this._fieldDrawer = fieldDrawer;
        }

        public void DrawParameterFields(ParameterInfo[] parameters, ref object[] parameterValues)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                _fieldDrawer.DrawField(parameter.Name, ref parameterValues[i], parameter.ParameterType, true);
            }
        }
    }
}