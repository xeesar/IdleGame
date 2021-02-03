using UnityEngine;

namespace Models.ProgressionCalculators
{
    public class GeometricCalculator : ProgressionCalculator
    {
        #region Public Methods

        public override float GetValue(float firstValue, float prevValue, float multiplier, float addition)
        {
            return firstValue * Mathf.Pow(multiplier, prevValue);
        }

        #endregion
    }
}

