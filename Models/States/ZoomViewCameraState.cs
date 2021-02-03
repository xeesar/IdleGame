using Controllers;
using Data.Configs;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Models.States
{
    public class ZoomViewCameraState : BaseCameraState
    {
        #region Properties

        public override float MinAngle => CameraController.MinAngle;

        public override float MaxAngle => CameraController.MaxAngle;

        #endregion
        
        
        
        #region Public Methods
        
        public override void OnStateEnter(CameraController cameraController)
        {
            base.OnStateEnter(cameraController);

            DOTween.Kill(this);
            
            CameraConfig cameraConfig = ConfigManager.Instance.Get<CameraConfig>(); 
            StartZoomTween(CameraController.Zoom, cameraConfig.zoomInSpeed, cameraConfig.zoomCurve);

            Vector3 position = CameraController.Camera.transform.localPosition - CameraController.ZoomOffset;
            
            StartMovingTween(position, cameraConfig.zoomInSpeed, cameraConfig.zoomCurve);
        }


        public override void OnStateExit()
        {
            base.OnStateExit();
            
            DOTween.Kill(this);
            
            CameraConfig cameraConfig = ConfigManager.Instance.Get<CameraConfig>();

            StartZoomTween(CameraController.BaseZoom, cameraConfig.zoomOutSpeed, cameraConfig.zoomCurve);
            StartMovingTween(CameraController.BasePosition, cameraConfig.zoomOutSpeed, cameraConfig.zoomCurve);
        }

        #endregion



        #region Private Methods

        private void StartZoomTween(float zoomValue, float speed, AnimationCurve curve)
        {
            CameraController.Camera.DOFieldOfView(zoomValue, speed).SetEase(curve).SetId(this);
        }


        private void StartMovingTween(Vector3 position, float speed, AnimationCurve curve)
        {
            CameraController.Camera.transform.DOLocalMove(position, speed).SetEase(curve).SetId(this);
        }

        #endregion
    }
}

