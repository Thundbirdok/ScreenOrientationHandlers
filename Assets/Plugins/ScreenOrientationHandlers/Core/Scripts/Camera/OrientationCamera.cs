namespace Plugins.ScreenOrientationHandlers.Core.Scripts.Camera
{
    using Plugins.ScreenOrientationHandlers.Core.Scripts;
    using UnityEngine;
    
#if CINEMACHINE

    using Cinemachine;
    
#endif
    
    [ExecuteAlways]
    public class OrientationCamera : MonoBehaviour
    {
        [SerializeField]
        private ScreenOrientationHandler handler;

#if CINEMACHINE

        [SerializeField] 
        private CinemachineVirtualCamera targetCamera;

#else

        [SerializeField] 
        private Camera targetCamera;
        
#endif
        
        [SerializeField]
        private float verticalFovOnLandscapeOrientation = 70.49793f;
        
        private float _portraitFov;
        private float _landscapeFov;

        private bool _isInitialized;
        
        private void OnEnable()
        {
            Initialize();
            
            handler.OnDeviceOrientationChanged += SetOrientation;
        }

        private void OnDisable()
        {
            handler.OnDeviceOrientationChanged -= SetOrientation;
        }

        private void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }
            
            _isInitialized = true;
            
            _landscapeFov = verticalFovOnLandscapeOrientation;

            _portraitFov = Camera.HorizontalToVerticalFieldOfView
            (
                verticalFovOnLandscapeOrientation,
                handler.HorizontalToVerticalFovThreshold
            );
        }

        private void SetOrientation()
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

#if CINEMACHINE

        private void SetLandscape() => targetCamera.m_Lens.FieldOfView = _landscapeFov;

        private void SetPortrait() => targetCamera.m_Lens.FieldOfView = _portraitFov;

#else

        private void SetLandscape() => targetCamera.fieldOfView = _landscapeFov;

        private void SetPortrait() => targetCamera.fieldOfView = _portraitFov;
        
#endif
        
    }
}
