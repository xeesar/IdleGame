using Data;
using UnityEngine;

namespace Models.Analytics
{
    [CreateAssetMenu(menuName = "Models/Analytics/AppMetrica Analytics", fileName = "AppMetricaAnalytics")]
    public class AppMetricaAnalytics : BaseAnalytics
    {
        public override void CustomEvent(AnalyticEvent analyticEvent)
        {
            AppMetrica.Instance.ReportEvent(analyticEvent.Name, analyticEvent.Parameters);
        }
    }
}