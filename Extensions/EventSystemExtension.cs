using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Extensions
{
    public static class EventSystemExtension
    {
        #region Field

        private static List<RaycastResult> m_raycastResult = new List<RaycastResult>();

        #endregion



        #region Public Methods

        public static bool IsPointerUnderUI(this EventSystem eventSystem, Vector2 mousePosition, LayerMask excludeLayer)
        {
            int layer = Mathf.RoundToInt(Mathf.Log(excludeLayer.value, 2));

            if (!HandleRaycast(mousePosition)) return false;

            for (int i = m_raycastResult.Count - 1; i >= 0; i--)
            {
                if(m_raycastResult[i].gameObject.layer != layer) continue;
                
                m_raycastResult.RemoveAt(i);    
            }

            return m_raycastResult.Count > 0;
        }
        
        #endregion



        #region Private Methods

        private static bool HandleRaycast(Vector2 mousePosition)
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = mousePosition;

            m_raycastResult.Clear();
            EventSystem.current.RaycastAll(pointerEventData, m_raycastResult);

            return m_raycastResult.Count > 0;
        }

        #endregion
    }
}