using UnityEngine;

namespace Data.Configs
{
    [CreateAssetMenu(menuName = "Configs/Camera Config", fileName = "CameraConfig")]
    public class CameraConfig : BaseConfig
    {
        #region Fields

       
        [Header("Global Config")] 

        public float angleError = 5f;
        public float rotationSpeed = 2f;
        public float returnAngleRotationSpeed = 1f;
        public float swipeForce = 2f;
        public float swipeMapForce = 2f;
        public float delayBeforeZoom = 0.5f;

        [Header("City Camera Config")]
        public float movingSpeed = 2f;
        public float movingFactor = -0.01f;
        public float moveToTargetTime = 1.5f;

        [Header("Zoom Parameters")] 
        public float zoomInSpeed = 1f;
        public float zoomOutSpeed = 0.5f;
        public AnimationCurve zoomCurve = null;
        

        #endregion
    }
}

