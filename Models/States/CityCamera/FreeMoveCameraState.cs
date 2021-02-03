using Controllers;
using Enums;
using Interfaces;
using Managers;
using Services;
using UnityEngine;

namespace Models.States.CityCamera
{
    public class FreeMoveCameraState : CityCameraState
    {
        #region Fields

        private InputService m_inputService;

        private Vector3 m_cameraTargetPos;
        
        #endregion
        
        
        
        #region Public Methods

        public override void OnStateEnter(CityCameraController cameraController)
        {
            base.OnStateEnter(cameraController);
            
            m_inputService = ServiceLocator.Instance.Get<InputService>();
            m_inputService.eventOnPointerDrag += UpdateCameraPos;

            UpdateManager.EventOnLateUpdate += MoveCamera;
            
            m_cameraTargetPos = m_cameraController.CameraPosition;

        }

        public override void OnStateExit()
        {
            m_inputService.eventOnPointerDrag -= UpdateCameraPos;
            UpdateManager.EventOnLateUpdate -= MoveCamera;
        }
        
        #endregion



        #region Private Methods

        private void UpdateCameraPos()
        {
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();
            
            if(userProfileModel.TutorialStage <= (int)TutorialType.WatchGraffiti && userProfileModel.CurrentCityProgress.DrawnBuildings == 0) return;
            
            Vector3 direction = m_inputService.Direction;
            Vector3 movement = m_cameraConfig.swipeMapForce * direction.magnitude * m_cameraConfig.movingFactor * direction;
            Vector2 newPos = m_cameraTargetPos + movement;

            m_cameraTargetPos = newPos;
        }


        private void MoveCamera(float deltaTime)
        {
            float movingSpeed = m_cameraConfig.movingSpeed * deltaTime;
            
            if (m_cameraController.Confiner.CameraWasDisplaced(m_cameraController.VirtualCamera))
            {
                m_cameraController.CameraPosition = m_cameraTargetPos = m_cameraController.Camera.transform.position;
            }

            m_cameraController.CameraPosition = Vector3.Lerp(m_cameraController.CameraPosition, m_cameraTargetPos, movingSpeed);
        }

        #endregion
    }
}

