using Data.Configs;
using DG.Tweening;
using Managers;
using Models.States;
using Services;
using UnityEngine;

namespace Controllers
{
    public class CameraController : MonoBehaviour
    {
        #region Fields
        
        [Header("Components")]
        [SerializeField] private Camera m_camera = null;
        
        [Header("Angle Options")] 
        [SerializeField] private float m_minAngle = 0f;
        [SerializeField] private float m_maxAngle = 0f;

        [Header("Zoom Options")]
        [SerializeField] private float m_zoomValue = 40;
        [SerializeField] private Vector3 m_zoomPositionOffset = Vector3.zero;

        private float m_baseZoom = 60;
        private Vector3 m_basePosition = Vector3.zero;

        private BaseCameraState m_cameraState = null;

        private float m_angle = 0;

        private CameraConfig m_cameraConfig = null;
        
        #endregion



        #region Properties
        public Camera Camera => m_camera;
        
        public float Angle
        {
            get => m_angle;
            private set => m_angle = Mathf.Clamp(value, m_cameraState.MinAngle, m_cameraState.MaxAngle);
        }


        public float MinAngle => m_minAngle;
        
        public float MaxAngle => m_maxAngle;

        public float Zoom => m_zoomValue;

        public float BaseZoom => m_baseZoom;

        public Vector3 ZoomOffset => m_zoomPositionOffset;
        
        public Vector3 BasePosition => m_basePosition;
        
        public Transform Target { get; set; }
        
        #endregion

        

        #region Unity Lifecycle

        private void Start()
        {
            m_baseZoom = Camera.fieldOfView;
            m_basePosition = Camera.transform.localPosition;
            
            m_cameraConfig = ConfigManager.Instance.Get<CameraConfig>();
            InputService inputService = ServiceLocator.Instance.Get<InputService>();

            inputService.eventOnPointerDown += ActivateCamera;
            inputService.eventOnPointerDrag += UpdateCameraAngle;
            inputService.eventOnPointerUp += ResetCamera;
            
            GraffitiManager.Instance.EventGraffitiCompleted += OnGraffitiCompleted;

            ChangeCameraState(new IdleCameraState());
        }


        private void OnDisable()
        {
            InputService inputService = ServiceLocator.Instance.Get<InputService>();

            if(inputService == null) return;
            
            inputService.eventOnPointerDown -= ActivateCamera;
            inputService.eventOnPointerDrag -= UpdateCameraAngle;
            inputService.eventOnPointerUp -= ResetCamera;
            
            GraffitiManager.Instance.EventGraffitiCompleted -= OnGraffitiCompleted;
        }
        

        private void LateUpdate()
        {
            HandleState();
        }

        #endregion



        #region Private Methods

        private void OnGraffitiCompleted()
        {
            ChangeCameraState(new ResultViewCameraState());
        }

        private void ActivateCamera()
        {
            if(m_cameraState is ResultViewCameraState) return;

            DOTween.Sequence().SetDelay(m_cameraConfig.delayBeforeZoom).OnComplete(() =>
            {
                ChangeCameraState(new ZoomViewCameraState());
            }).SetId(this);
            
            ChangeCameraState(new ViewCameraState());
        }


        private void UpdateCameraAngle()
        {
            DOTween.Kill(this);
            
            Vector2 inputDirection = ServiceLocator.Instance.Get<InputService>().Direction;
            
            float angle = m_cameraConfig.swipeForce * (inputDirection.x * inputDirection.magnitude);

            Angle += angle;
            
        }
        
        
        private void ResetCamera()
        {
            if(m_cameraState is ResultViewCameraState) return;

            DOTween.Kill(this);
            
            ChangeCameraState(new IdleCameraState());
            UpdateCameraAngle();
        }
        
        
        private void HandleState()
        {
            BaseCameraState cameraState = m_cameraState.HandleState();

            if (cameraState != null && Target != null)
            {
                ChangeCameraState(cameraState);
            }
        }
        
        
        private void ChangeCameraState(BaseCameraState newState)
        {
            m_cameraState?.OnStateExit();
            m_cameraState = newState;
            m_cameraState.OnStateEnter(this);
        }

        #endregion
    }
}

