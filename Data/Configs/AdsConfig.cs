using UnityEngine;

namespace Data.Configs
{
    [CreateAssetMenu(menuName = "Ads/Config", fileName = "AdsConfig")]
    public class AdsConfig : ScriptableObject
    {
        [SerializeField] private CrossPlatformKey m_rewardedAdUnit;
        [SerializeField] private CrossPlatformKey m_interstitialAdUnit;

        public string RewardedAdUnit => m_rewardedAdUnit.GetKey();
        
        public string InterstitialAdUnit => m_interstitialAdUnit.GetKey();
    }
}
