using Data;
using UnityEngine;

namespace Models.Analytics
{
    public abstract class BaseAnalytics : ScriptableObject
    {
        public abstract void CustomEvent(AnalyticEvent analyticEvent);
    }
}
