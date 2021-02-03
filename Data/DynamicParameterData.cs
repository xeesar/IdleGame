using Enums;

namespace Data
{
    [System.Serializable]
    public class DynamicParameterData
    {
        #region Fields

        public DynamicParameterType type;

        public DefaultValuesData Parameter;
        public DefaultValuesData Price;

        public int maxLevel;

        public string valueFormat;

        #endregion
    }
}

