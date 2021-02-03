using Enums;
using Extensions;

namespace Models.Currencies
{
    public class Dollar : Currency
    {
        #region Properties

        public override CurrencyType CurrencyType => CurrencyType.Dollar;
        
        public override string Name => StringConstants.PlayerPrefsKeys.Dollar;

        #endregion
        
        
        
        #region Public Methods

        public Dollar(float amount) : base(amount)
        {
        }

        #endregion
    }
}

