using Data;
using Interfaces;
using UnityEngine;

namespace Models.GameFactory
{
    public abstract class BaseGameFactory
    {
        #region Fields

        protected readonly GraffitiData _graffitiData;

        protected readonly IGraffitiHolst _holst;
        
        #endregion
        
        
        
        #region Public Methods

        public void CreateGame()
        {
            Texture texture = _graffitiData.previewTexture;
            _holst.Initialize(texture.width, texture.height);

            _graffitiData.PrepareGraffitiBlocksData();
            
            InitializeGraffitiDrawer();
        }

        #endregion



        #region Private Methods

        protected BaseGameFactory(GraffitiData graffitiData, IGraffitiHolst holst)
        {
            _graffitiData = graffitiData;
            _holst = holst;
        }
        
        
        protected abstract void InitializeGraffitiDrawer();

        #endregion
    }
}

