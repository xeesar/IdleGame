using UnityEngine;

namespace Models.Screens
{
    public abstract class BaseScreen : MonoBehaviour
    {
        #region Public Methods

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }


        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        #endregion
    }
}

