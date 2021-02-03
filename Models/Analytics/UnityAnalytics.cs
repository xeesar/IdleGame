using Data;
using UnityEngine;

namespace Models.Analytics
{
    [CreateAssetMenu(menuName = "Models/Analytics/Unity Analytics", fileName = "UnityAnalytics")]
    public class UnityAnalytics : BaseAnalytics
    {
        public override void CustomEvent(AnalyticEvent analyticEvent)
        {
            string log = $"[{analyticEvent.Name}] - ";

            foreach (var parameter in analyticEvent.Parameters)
            {
                log += $"{parameter.Key} : {parameter.Value},";
            }
            
            Debug.Log(log);
        }
    }
}