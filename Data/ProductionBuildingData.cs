namespace Data
{
    [System.Serializable]
    public class ProductionBuildingData
    {
        #region Fields

        public bool isUnlocked;
        public bool isAutoCollectingEnabled;
        public bool isDrawn;
        
        public int incomeParameterLevel;
        public int productionSpeedParameterLevel;
        public int artistsCount;
        public int currency;
        
        public float incomeValue;
        public float productionSpeedValue;
        public float productionProgress;
        public float drawProgress;
        
        #endregion
    }
}
