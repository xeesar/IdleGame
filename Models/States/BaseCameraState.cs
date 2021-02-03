using Controllers;
using Data.Configs;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Models.States
{
    public abstract class BaseCameraState
    {
        #region Properties

        protected CameraController CameraController { get; private set; }

        public virtual float MinAngle
        {
            get
            {
                CameraConfig cameraConfig = ConfigManager.Instance.Get<CameraConfig>();

                return CameraController.MinAngle - cameraConfig.angleError;
            }
        }

        public virtual float MaxAngle
        {
            get
            {
                CameraConfig cameraConfig = ConfigManager.Instance.Get<CameraConfig>();

                return CameraController.MaxAngle + cameraConfig.angleError;
            }
        }

        #endregion
        
        
        
        #region Public Methods

        public virtual BaseCameraState HandleState()
        {
            RotateCamera();
            LookCameraAtTarget();

            return null;
        }


        public virtual void OnStateEnter(CameraController cameraController)
        {
            CameraController = cameraController;
        }


        public virtual void OnStateExit()
        {
            
        }
        
        
        #endregion



        #region Private Methods

        protected virtual void RotateCamera()
        {
            Transform transform = CameraController.transform;
            
            float yAngle = transform.eulerAngles.y;
            yAngle = yAngle > 180 ? yAngle - 360 : yAngle;

            float rotationSpeed = GetRotationSpeed(yAngle) * Time.deltaTime;
                
            float targetAngle = Mathf.Lerp(yAngle, CameraController.Angle, rotationSpeed);
            
            Vector3 newAngle = new Vector3(0, targetAngle, 0);

            transform.eulerAngles = newAngle;
            
        }

        
        private float GetRotationSpeed(float angle)
        {
            CameraConfig cameraConfig = ConfigManager.Instance.Get<CameraConfig>();
            
            if (angle < MinAngle || angle > MaxAngle)
            {
                return cameraConfig.returnAngleRotationSpeed;
            }

            return cameraConfig.rotationSpeed;
        }


        private void LookCameraAtTarget()
        {
            Transform cameraTransform = CameraController.Camera.transform;
            Transform target = CameraController.Target;
            
            cameraTransform.LookAt(target);
        }

        #endregion
    }
}
