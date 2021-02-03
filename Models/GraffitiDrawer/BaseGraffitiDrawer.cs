using System;
using Data;
using Enums;
using Interfaces;
using Managers;
using Models.DynamicParameters;
using UnityEngine;

namespace Models.GraffitiDrawer
{
    public abstract class BaseGraffitiDrawer
    {
        #region Properties
        
        public event Action EventBlockCompleted = delegate { };
        
        public event Action EventGraffitiCompleted = delegate { };

        public GraffitiData GraffitiData { get; private set; }
        
        public IGraffitiHolst Holst { get; private set; }

        protected float DrawSpeed => DynamicParametersManager.Instance.Get(DynamicParameterType.DrawingSpeed).Value;

        #endregion



        #region Public Methods

        protected BaseGraffitiDrawer(GraffitiData graffitiData, IGraffitiHolst holst)
        {
            GraffitiData = graffitiData;
            Holst = holst;
        }
        
        
        public abstract void Initialize();


        public void Draw(GraffitiBlockData graffitiBlockData)
        {
            IncreaseProgressFor(graffitiBlockData);
            Holst.Draw(graffitiBlockData);
            CheckCompletion(graffitiBlockData);
        }
        

        public void CheckCompletionGraffiti()
        {
            if (GraffitiData.Progress >= GraffitiData.blocksCount)
            {
                EventGraffitiCompleted?.Invoke();
            }
        }
        
        #endregion



        #region Private Method

        private void IncreaseProgressFor(GraffitiBlockData blockData)
        {
            blockData.DrawTime = GetDrawTime(blockData.DrawTime);
            blockData.Progress = Mathf.Lerp(0, 1, blockData.DrawTime / DrawSpeed);
        }
        
        
        protected abstract float GetDrawTime(float currentDrawTime);


        private void CheckCompletion(GraffitiBlockData blockData)
        {
            if(blockData.Progress < 1) return;
            
            EventBlockCompleted?.Invoke();
            
            CheckCompletionGraffiti();
        }

        #endregion
    }
}

