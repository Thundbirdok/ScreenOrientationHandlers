namespace Plugins.ScreenOrientationHandlers.Core.Scripts.Canvas
{
    using Plugins.ScreenOrientationHandlers.Core.Scripts;
    using UnityEngine;
    using UnityEngine.UI;

    [ExecuteAlways]
    public class OrientationResolution : MonoBehaviour
    {
        [SerializeField]
        private ScreenOrientationHandler handler;
        
        [SerializeField]
        private CanvasScaler canvasScaler;

        [SerializeField]
        private Vector2 landscapeResolution;

        private Vector2 _portraitResolution;

        private bool _isInitialized;
        
        private void OnEnable()
        {
            Initialize();
            
            handler.OnDeviceOrientationChanged += UpdateResolution;
        }
        
        private void OnDisable()
        {
            handler.OnDeviceOrientationChanged -= UpdateResolution;
        }

        private void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;
            
            _portraitResolution = new Vector2(landscapeResolution.y, landscapeResolution.x);
        }
        
        private void UpdateResolution()
        {
            switch (handler.Orientation)
            {
                case DeviceOrientation.LandscapeLeft:
                    SetLandscape();
                    break;
                
                case DeviceOrientation.Portrait:
                    SetPortrait();
                    break;
            }
        }
        
        private void SetLandscape()
        {
            canvasScaler.referenceResolution = landscapeResolution;
            canvasScaler.matchWidthOrHeight = 1;
        }

        private void SetPortrait()
        {
            canvasScaler.referenceResolution = _portraitResolution;
            canvasScaler.matchWidthOrHeight = 0;
        }
    }
}
