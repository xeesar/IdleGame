namespace Models.ProgressionCalculators
{
    public class ArithmeticCalculator : ProgressionCalculator
    {
        #region Public Methods

        public override float GetValue(float firstValue, float prevValue, float multiplier, float addition)
        {
            return firstValue + (prevValue * multiplier) + addition;
        }

        #endregion
    }
}
