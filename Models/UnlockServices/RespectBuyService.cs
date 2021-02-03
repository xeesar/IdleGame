using System;
using Enums;
using Interfaces;
using Services;

namespace Models.UnlockService
{
    public class RespectBuyService : BuyService
    {
        #region Public Methods

        public override void TryToBuy(float price, Action onComplete = null)
        {
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();

            if (userProfileModel.GetCurrency(CurrencyType.Respect).Value < price)
            {
                return;
            }
            
            onComplete?.Invoke();
            userProfileModel.UpdateCurrency(CurrencyType.Respect, -price);
        }

        #endregion
    }
}
