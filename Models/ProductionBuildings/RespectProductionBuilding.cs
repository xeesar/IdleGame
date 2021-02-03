using Enums;
using Interfaces;
using Models.UnlockService;
using Services;

namespace Models.ProductionBuildings
{
    public class RespectProductionBuilding : ProductionBuilding
    {
        #region Properites

        public override CurrencyType CurrencyType => CurrencyType.Respect;

        #endregion
        
        
        
        #region Public Methods

        public override void CollectIncome()
        {
            ServiceLocator.Instance.Get<IUserProfileModel>().UpdateCurrency(CurrencyType, m_income.Value);
            
            base.CollectIncome();
        }

        #endregion



        #region Private Methods

        protected override void UnlockBuilding()
        {
            RespectBuyService respectBuyService = new RespectBuyService();
            
            respectBuyService.TryToBuy(m_buildingConfig.UnlockPrice, () => base.UnlockBuilding());
        }


        protected override void UnlockAutoCollecting()
        {
            BuyService buyService = new RespectBuyService();
            buyService.TryToBuy(m_buildingConfig.AutoCollectingPrice, base.UnlockAutoCollecting);
        }
//
//        protected override void UpgradeIncome()
//        {
//            BuyService buyService = new RespectBuyService();
//            buyService.TryToBuy((int)m_income.Price, base.UpgradeIncome);
//        }
//
//        protected override void UpgradeProductionSpeed()
//        {
//            BuyService buyService = new RespectBuyService();
//            buyService.TryToBuy((int)m_productionSpeed.Price, base.UpgradeProductionSpeed);
//        }

        #endregion
    }
}