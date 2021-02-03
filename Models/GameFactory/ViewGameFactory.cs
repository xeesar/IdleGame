using Data;
using Interfaces;
using Managers;
using Models.GraffitiDrawer;

namespace Models.GameFactory
{
    public class ViewGameFactory : BaseGameFactory
    {
        public ViewGameFactory(GraffitiData graffitiData, IGraffitiHolst holst) : base(graffitiData, holst)
        {
        }

        protected override void InitializeGraffitiDrawer()
        {
            BaseGraffitiDrawer graffitiDrawer = new OfflineGraffitiDrawer(_graffitiData, _holst);
            graffitiDrawer.Initialize();
            
            GraffitiManager.Instance.SetGraffitiDrawer(graffitiDrawer);

            LevelManager.Instance.Building.HideScaffolding(0);
        }
    }
}