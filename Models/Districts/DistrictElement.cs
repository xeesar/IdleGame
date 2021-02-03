using UnityEngine;

namespace Models.Districts
{
    public abstract class DistrictElement : MonoBehaviour
    {
        #region Fields

        private const string GRAYSCALE_PARAMETER_NAME = "_GrayscaleAmount";
        
        #endregion
        
        
        
        #region Properties
        
        protected abstract SpriteRenderer SpriteRenderer { get; }

        #endregion
        
        
        
        #region Public Methods

        public virtual void SetElementActive(bool isActive)
        {
            SetGrayScale(isActive);
        }

        #endregion



        #region Private Methods
        
        private void SetGrayScale(bool isActive)
        {
            SpriteRenderer.material.SetFloat(GRAYSCALE_PARAMETER_NAME, isActive ? 0 : 1);
        }

        #endregion
    }
}

