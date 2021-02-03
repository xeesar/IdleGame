using Data;
using Data.Configs;
using Enums;
using Interfaces;
using Managers;
using Models.Currencies;
using Services;

namespace Models.Income
{
    public class PerPixelIncome : Income
    {
        #region Public Methods

        public override void Give(float multiplier)
        {
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();

            float value = DynamicParametersManager.Instance.Get(DynamicParameterType.RespectIncomePerBlock).Value * multiplier;
            userProfileModel.UpdateCurrency(CurrencyType.Respect, value);
        }

        #endregion
    }
}

