using System;
using Managers;

namespace Models.UnlockService
{
    public class AdBuyService : BuyService
    {
        private string m_placement = "";
        
        
        public AdBuyService(string placement)
        {
            m_placement = placement;
        }
        
        public override void TryToBuy(float price, Action onComplete = null)
        {
            AdManager.Instance.ShowRewarded(m_placement, onComplete, null);
        }
    }
}
