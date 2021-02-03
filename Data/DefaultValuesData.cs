using Enums;

namespace Data
{
    [System.Serializable]
    public struct DefaultValuesData
    {
        public BonusType bonusType;
        public ProgressionType progressionType;
        
        public float firstValue;
        public float multiplier;
        public float addition;
    }
}
