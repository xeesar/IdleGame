using System.Collections.Generic;
using Data;
using Extensions;
using Interfaces;
using UnityEngine;
using View;

namespace Models.Graffiti
{
    public class GraffitiHolst : MonoBehaviour, IGraffitiHolst
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private MeshRenderer m_meshRenderer = null;
        [SerializeField] private GraffitiView m_graffitiView = null;

        private float m_width;
        private float m_height;
        
        #endregion



        #region Properties

        public Transform Transform => transform;

        public float Width => m_width;

        public float Height => m_height;

        #endregion
        
        
        
        #region Public Methods

        public void Initialize(int width, int height)
        {
            m_width = width;
            m_height = height;
            
            m_graffitiView.Setup(width, height);
        }
        

        public void Draw(GraffitiBlockData graffitiBlockData)
        {
            m_graffitiView.DisplayBlock(graffitiBlockData);
        }


        public Vector3 GetBlockPosition(float xTime, float yTime)
        {
            Bounds holstBounds = m_meshRenderer.bounds;
            float xPos = Mathf.Lerp(holstBounds.min.x, holstBounds.max.x, xTime);
            float yPos = Mathf.Lerp(holstBounds.max.y, holstBounds.min.y, yTime);
                
            return new Vector3(xPos, yPos, transform.position.z);
        }

        #endregion
    }
}

