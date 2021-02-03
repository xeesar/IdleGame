using Extensions;
using TMPro;
using UnityEngine;

namespace View.UI
{
    public class DistrictView : MonoBehaviour
    {
        #region Fields

        [Header("Components")]
        [SerializeField] private GameObject m_panel;
        [SerializeField] private TextMeshProUGUI m_text;

        #endregion



        #region Public Methods

        public void SetDistrictStatus(bool isUnlocked, int unlockCost)
        {
            m_panel.SetActive(!isUnlocked);
            m_text.text = string.Format(StringConstants.Formats.UnlockDistrictFormat, unlockCost);
        }

        #endregion
    }
}
