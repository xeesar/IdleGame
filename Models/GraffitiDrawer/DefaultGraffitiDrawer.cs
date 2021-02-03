using Data;
using Interfaces;
using UnityEngine;

namespace Models.GraffitiDrawer
{
    public class DefaultGraffitiDrawer : BaseGraffitiDrawer
    {
        #region Public Methods

        public DefaultGraffitiDrawer(GraffitiData graffitiData, IGraffitiHolst holst) : base(graffitiData, holst)
        {
        }


        public override void Initialize()
        {
            
        }

        #endregion



        #region Private Methods

        protected override float GetDrawTime(float currentDrawTime)
        {
            return currentDrawTime + Time.deltaTime;
        }

        #endregion
    }
}

