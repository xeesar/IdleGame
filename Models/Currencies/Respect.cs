using Enums;
using Extensions;

namespace Models.Currencies
{
    public class Respect : Currency
    {
        #region Properties

        public override CurrencyType CurrencyType => CurrencyType.Respect;

        public override string Name => StringConstants.PlayerPrefsKeys.Respect;

        #endregion
        
        
        
        #region Public Methods

        public Respect(float amount) : base(amount)
        {
        }

        #endregion
    }
}

