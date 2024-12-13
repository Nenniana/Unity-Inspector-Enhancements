namespace Nenn.InspectorEnhancements.Editor.Helpers.FieldDrawing.IFoldoutProvider.Base
{
    public interface IFoldoutProvider
    {
        public bool ToggleFoldout(string foldoutKey, bool defaultState, string foldoutText);
        public bool GetFoldoutState(string foldoutKey, bool defaultState);
    }
}