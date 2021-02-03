using System.Collections.Generic;

namespace Data
{
    public class AnalyticEvent
    {
        private string m_name;

        private Dictionary<string, object> m_parameters = null;



        public string Name => m_name;

        public Dictionary<string, object> Parameters => m_parameters;


        public AnalyticEvent(string name)
        {
            m_name = name;
        }


        public void AddParameter(string parameterName, object data)
        {
            if (m_parameters == null)
            {
                m_parameters = new Dictionary<string, object>();
            }
            
            m_parameters.Add(parameterName, data);
        }
    }
}
