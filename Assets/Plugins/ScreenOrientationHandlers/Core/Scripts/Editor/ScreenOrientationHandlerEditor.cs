namespace Plugins.ScreenOrientationHandlers.Core.Scripts.Editor
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(ScreenOrientationHandler))]
    public class ScreenOrientationHandlerEditor : Editor
    {
        private ScreenOrientationHandler _handler;
        
        private void Awake()
        {
            _handler = target as ScreenOrientationHandler;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Update Orientation"))
            {
                _handler.UpdateOrientationInEditor();
            }
        }
        
        [MenuItem("Tools/Screen Orientation Handler/Update Orientation")]
        public static void SetScreenOrientation()
        {
            var handler = FindObjectOfType<ScreenOrientationHandler>();
            handler.UpdateOrientationInEditor();
        }
    }
}
