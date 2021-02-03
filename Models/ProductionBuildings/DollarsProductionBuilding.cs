using Enums;
using Interfaces;
using Models.UnlockService;
using Services;

namespace Models.ProductionBuildings
{
    public class DollarsProductionBuilding : ProductionBuilding
    {
        #region Properites

        public override CurrencyType CurrencyType => CurrencyType.Dollar;

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
            DollarsBuyService dollarsBuyService = new DollarsBuyService();
            
            dollarsBuyService.TryToBuy(m_buildingConfig.UnlockPrice, base.UnlockBuilding);
        }
        
        
        protected override void UnlockAutoCollecting()
        {
            BuyService buyService = new DollarsBuyService();
            buyService.TryToBuy(m_buildingConfig.AutoCollectingPrice, base.UnlockAutoCollecting);
        }

//        protected override void UpgradeIncome()
//        {
//            BuyService buyService = new DollarsBuyService();
//            buyService.TryToBuy((int)m_income.Price, base.UpgradeIncome);
//        }
//
//        protected override void UpgradeProductionSpeed()
//        {
//            BuyService buyService = new DollarsBuyService();
//            buyService.TryToBuy((int)m_productionSpeed.Price, base.UpgradeProductionSpeed);
//        }

        #endregion
    }
}
