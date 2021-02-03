using UnityEngine;

namespace Data
{
    [System.Serializable]
    public struct CrossPlatformKey
    {
        [SerializeField] private string m_androidKey;
        [SerializeField] private string m_iosKey;


        public string GetKey()
        {
            if (Application.isEditor)
            {
                return "unexpected_platform";
            }
            
            if (Application.platform == RuntimePlatform.Android)
            {
                return m_androidKey;
            }

            return m_iosKey;
        }
    }
}
