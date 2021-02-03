using System;

namespace Models.UnlockService
{
    public abstract class BuyService
    {
        #region Public Methods

        public abstract void TryToBuy(float price, Action onComplete = null);

        #endregion
    }
}
