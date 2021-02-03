using System;
using Managers;
using UnityEngine;

namespace App.Scripts.Extentions
{
    public class CanvasSafeArea : MonoBehaviour
    {
        private RectTransform _panel;
        private Rect _lastSafeArea = new Rect(0, 0, 0, 0);

        private void Awake()
        {
            _panel = GetComponent<RectTransform>();
            Refresh();
        }
        
        
        private Rect GetSafeArea()
        {
            return Screen.safeArea;
        }

        private void Refresh()
        {
            Rect safeArea = GetSafeArea();
            if (safeArea != _lastSafeArea)
            {
                ApplySafeArea(safeArea);
            }
        }

        private void ApplySafeArea(Rect r)
        {
            _lastSafeArea = r;
            Vector2 anchorMin = r.position;
            Vector2 anchorMax = r.position + r.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
        
            _panel.anchorMin = anchorMin;
            _panel.anchorMax = anchorMax;
        }
    }
}
