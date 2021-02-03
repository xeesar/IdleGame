using Cinemachine;
using Interfaces;
using Models.States.CityCamera;
using Services;
using UnityEngine;

namespace Controllers
{
    public class CityCameraController : MonoBehaviour
    {
        #region Fields
        
        [SerializeField] Camera m_camera;
        [SerializeField] CinemachineVirtualCamera m_vCamera;
        [SerializeField] CinemachineConfiner m_confiner;
        
        private CityCameraState m_cameraState;

        private IUserProfileModel m_userProfileModel;
        
        #endregion

        

        #region Properties

        public Camera Camera => m_camera;
        
        public CinemachineVirtualCamera VirtualCamera => m_vCamera;

        public CinemachineConfiner Confiner => m_confiner;

        public Vector3 CameraPosition
        {
            get => m_vCamera.transform.position;
            set
            {
                m_vCamera.transform.position = value;
                m_userProfileModel.LastCameraPos = value;
            }
        }
        
        #endregion



        #region Unity Lifecycle

        private void Start()
        {
            m_userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();
            CameraPosition = m_userProfileModel.LastCameraPos;
            
            ChangeCameraState(new FreeMoveCameraState());
        }


        private void OnDisable()
        {
            m_cameraState?.OnStateExit();
        }

        #endregion



        #region Public Methods

        public void ChangeCameraState(CityCameraState cameraState)
        {
            m_cameraState?.OnStateExit();
            m_cameraState = cameraState;
            m_cameraState.OnStateEnter(this);
        }

        #endregion
    }
}
