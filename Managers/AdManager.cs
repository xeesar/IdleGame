using System;
using Data;
using Data.Configs;
using Extensions;
using GoogleMobileAds.Api;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class AdManager : Singleton<AdManager>, IManager, IAds, IDisposable
    {
        [SerializeField] private AdsConfig m_adsConfig;

        private InterstitialAd m_interstitial = null;
        private RewardedAd m_rewardedAd = null;

        private Action m_onSuccessRewarded = null;
        private Action m_onFailRewarded = null;

        private string m_placement = "";

        
        public bool IsInitialized { get; private set; }

        public bool IsInterstitialLoaded => m_interstitial.IsLoaded();

        public bool IsRewardedLoaded => m_rewardedAd.IsLoaded();

        public void Initialize()
        {
            MobileAds.Initialize((status) => { IsInitialized = true; });
            
            LoadInterstitial();
            LoadRewarded();
        }

        public void Dispose()
        {
            DisposeInterstitial();
            DisposeRewarded();
        }


        public void LoadInterstitial()
        {
            m_interstitial = new InterstitialAd(m_adsConfig.InterstitialAdUnit);
            
            m_interstitial.OnAdLoaded += OnInterstitialAdLoaded;
            m_interstitial.OnAdFailedToLoad += OnInterstitialAdFailedToLoad;
            m_interstitial.OnAdOpening += OnInterstitialAdOpen;
            m_interstitial.OnAdClosed += OnInterstitialAdClosed;
            m_interstitial.OnAdLeavingApplication += OnInterstitialAdLeavingApplication;

            AdRequest adRequest = new AdRequest.Builder().Build();
            m_interstitial.LoadAd(adRequest);
        }

        public void LoadRewarded()
        {
            m_rewardedAd = new RewardedAd(m_adsConfig.RewardedAdUnit);

            m_rewardedAd.OnAdLoaded += OnRewardedAdLoaded;
            m_rewardedAd.OnAdFailedToLoad += OnRewardedAdFailedToLoad;
            m_rewardedAd.OnAdOpening += OnRewardedAdOpen;
            m_rewardedAd.OnAdFailedToShow += OnRewardedAdFailedToShow;
            m_rewardedAd.OnAdClosed += OnRewardedAdClosed;
            m_rewardedAd.OnUserEarnedReward += OnRewardedAdRewarded;
            
            AdRequest adRequest = new AdRequest.Builder().Build();
            m_rewardedAd.LoadAd(adRequest);
        }

        public bool ShowInterstitial(string placement)
        {
            if (IsInterstitialLoaded)
            {
                m_placement = placement;
                m_interstitial.Show();
                return true;
            }

            Debug.Log("ShowInterstitial: not ready");
            return false;
        }

        public bool ShowRewarded(string placement, Action onSuccess, Action onFail)
        {
            if (IsRewardedLoaded)
            {
                m_onSuccessRewarded = onSuccess;
                m_onFailRewarded = onFail;

                m_placement = placement;
                m_rewardedAd.Show();
                return true;
            }
            onFail?.Invoke();

            Debug.Log("ShowRewarded: not ready");
            return false;
        }

        private void DisposeInterstitial()
        {
            m_interstitial.OnAdLoaded -= OnInterstitialAdLoaded;
            m_interstitial.OnAdFailedToLoad -= OnInterstitialAdFailedToLoad;
            m_interstitial.OnAdOpening -= OnInterstitialAdOpen;
            m_interstitial.OnAdClosed -= OnInterstitialAdClosed;
            m_interstitial.OnAdLeavingApplication -= OnInterstitialAdLeavingApplication;
            
            m_interstitial.Destroy();
        }

        private void DisposeRewarded()
        {
            m_rewardedAd.OnAdLoaded -= OnRewardedAdLoaded;
            m_rewardedAd.OnAdFailedToLoad -= OnRewardedAdFailedToLoad;
            m_rewardedAd.OnAdOpening -= OnRewardedAdOpen;
            m_rewardedAd.OnAdFailedToShow -= OnRewardedAdFailedToShow;
            m_rewardedAd.OnAdClosed -= OnRewardedAdClosed;
            m_rewardedAd.OnUserEarnedReward -= OnRewardedAdRewarded;
        }


        private void OnInterstitialAdLoaded(object sender, EventArgs args)
        {
            AnalyticEvent analyticEvent = new AnalyticEvent("ad_interstitial_loaded");
            analyticEvent.AddParameter("placement", m_placement);
            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
        }
        
        private void OnInterstitialAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            AnalyticEvent analyticEvent = new AnalyticEvent("ad_interstitial_failed");
            analyticEvent.AddParameter("placement", m_placement);
            analyticEvent.AddParameter("reason", args.Message);
            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
        }
        
        private void OnInterstitialAdOpen(object sender, EventArgs args)
        {
            AnalyticEvent analyticEvent = new AnalyticEvent("ad_interstitial_opened");
            analyticEvent.AddParameter("placement", m_placement);
            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
        }
        
        private void OnInterstitialAdClosed(object sender, EventArgs args)
        {
            AnalyticEvent analyticEvent = new AnalyticEvent("ad_interstitial_closed");
            analyticEvent.AddParameter("placement", m_placement);
            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
            
            DisposeInterstitial();
            LoadInterstitial();
        }
        
        private void OnInterstitialAdLeavingApplication(object sender, EventArgs args)
        {
            AnalyticEvent analyticEvent = new AnalyticEvent("ad_interstitial_left_app");
            analyticEvent.AddParameter("placement", m_placement);
            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
        }


        private void OnRewardedAdLoaded(object sender, EventArgs args)
        {
            AnalyticEvent analyticEvent = new AnalyticEvent("ad_reward_loaded");
            analyticEvent.AddParameter("placement", m_placement);
            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);        
        }
        
        private void OnRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
        {
            AnalyticEvent analyticEvent = new AnalyticEvent("ad_reward_failed");
            analyticEvent.AddParameter("placement", m_placement);
            analyticEvent.AddParameter("reason", args.Message);
            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
        }
        
        private void OnRewardedAdOpen(object sender, EventArgs args)
        {
            AnalyticEvent analyticEvent = new AnalyticEvent("ad_reward_opened");
            analyticEvent.AddParameter("placement", m_placement);
            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
        }
        
        private void OnRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
        {
            AnalyticEvent analyticEvent = new AnalyticEvent("ad_reward_failed");
            analyticEvent.AddParameter("placement", m_placement);
            analyticEvent.AddParameter("reason", args.Message);
            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
            
            m_onFailRewarded?.Invoke();
            m_onSuccessRewarded = null;
            m_onFailRewarded = null;
        }
        
        private void OnRewardedAdClosed(object sender, EventArgs args)
        {
            AnalyticEvent analyticEvent = new AnalyticEvent("ad_reward_closed");
            analyticEvent.AddParameter("placement", m_placement);
            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
            
            DisposeRewarded();
            LoadRewarded();
        }
        
        private void OnRewardedAdRewarded(object sender, Reward reward)
        {
            AnalyticEvent analyticEvent = new AnalyticEvent("ad_reward_earned");
            analyticEvent.AddParameter("placement", m_placement);
            AnalyticsManager.Instance.SendCustomEvent(analyticEvent);
            
            m_onSuccessRewarded?.Invoke();
            m_onSuccessRewarded = null;
            m_onFailRewarded = null;
        }
    }
}
