using GameAnalyticsSDK;
using UnityEngine;

namespace Managers
{
    public class GameAnalyticsManager : MonoBehaviour
    {
        private void Start()
        {
            GameAnalytics.Initialize();
        }
    }
}
