using Controllers;
using Data.Configs;
using Managers;

namespace Models.States.CityCamera
{
    public abstract class CityCameraState
    {
        protected CityCameraController m_cameraController;
        
        protected CameraConfig m_cameraConfig = null;
        
        public virtual void OnStateEnter(CityCameraController cameraController)
        {
            m_cameraController = cameraController;
            m_cameraConfig = ConfigManager.Instance.Get<CameraConfig>();
        }
        

        public virtual void OnStateExit()
        {
            
        }
    }
}