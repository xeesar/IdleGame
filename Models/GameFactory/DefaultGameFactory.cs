using Data;
using Interfaces;
using Managers;
using Models.GraffitiDrawer;
using Models.Popups;

namespace Models.GameFactory
{
    public class DefaultGameFactory : BaseGameFactory
    {
        #region Private Methods

        public DefaultGameFactory(GraffitiData graffitiData, IGraffitiHolst holst) : base(graffitiData, holst)
        {
        }

        protected override void InitializeGraffitiDrawer()
        {
           HandleOfflineDrawer();
           CreateDefaultGraffitiDrawer();
        }


        private void HandleOfflineDrawer()
        {
            BaseGraffitiDrawer graffitiDrawer = new OfflineGraffitiDrawer(_graffitiData, _holst);
            graffitiDrawer.Initialize();
        }


        private void CreateDefaultGraffitiDrawer()
        {
            BaseGraffitiDrawer graffitiDrawer = new DefaultGraffitiDrawer(_graffitiData, _holst);
            graffitiDrawer.Initialize();
            
            GraffitiManager.Instance.SetGraffitiDrawer(graffitiDrawer);
            
            graffitiDrawer.CheckCompletionGraffiti();
        }
        
        #endregion
    }
}
