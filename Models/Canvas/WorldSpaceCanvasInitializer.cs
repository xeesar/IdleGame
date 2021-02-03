using Controllers;
using UnityEngine;

namespace Models.Canvas
{
    public class WorldSpaceCanvasInitializer : MonoBehaviour
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private UnityEngine.Canvas m_canvas = null;

        private Camera m_camera;

        #endregion
        
        
        
        #region Unity Lifecycle

        private void Start()
        {
            m_camera = FindObjectOfType<CityCameraController>().Camera;
            m_canvas.worldCamera = m_camera;
        }

        #endregion
    }
}
