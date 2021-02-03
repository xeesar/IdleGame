namespace Models.ProgressionCalculators
{
    public abstract class ProgressionCalculator
    {
        #region Public Methods

        public abstract float GetValue(float firstValue, float prevValue, float multiplier, float addition);

        #endregion
    }
}
