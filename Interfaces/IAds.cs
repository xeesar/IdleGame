using System;

namespace Interfaces
{
    public interface IAds
    {
        bool IsInitialized { get; }
        bool IsInterstitialLoaded { get; }
        bool IsRewardedLoaded { get; }
        
        void LoadInterstitial();
        void LoadRewarded();

        bool ShowInterstitial(string placement);
        bool ShowRewarded(string placement, Action onSuccess, Action onFail);
    }
}

