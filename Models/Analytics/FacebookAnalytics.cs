using Data;
using Facebook.Unity;
using UnityEngine;

namespace Models.Analytics
{
    [CreateAssetMenu(menuName = "Models/Analytics/Facebook Analytics", fileName = "FacebookAnalytics")]
    public class FacebookAnalytics : BaseAnalytics
    {
        public override void CustomEvent(AnalyticEvent analyticEvent)
        {
            if (FB.IsInitialized)
            {
                FB.LogAppEvent(analyticEvent.Name, 0,  analyticEvent.Parameters);
            }
        }
    }
}

