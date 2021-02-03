using UnityEngine;
using UnityEngine.UI;

namespace View.UI.UIInteractable
{
    public class ButtonInteractable : UIInteractableView
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private Button m_button = null;

        #endregion



        #region Public Methods

        public override void SetInteractable(bool isInteractable)
        {
            m_button.interactable = isInteractable;
        }

        #endregion
    }
}
