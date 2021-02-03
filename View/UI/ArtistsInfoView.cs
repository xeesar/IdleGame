using Enums;
using Extensions;
using Managers;
using TMPro;
using UnityEngine;

namespace View.UI
{
    public class ArtistsInfoView : MonoBehaviour
    {
        #region Fields

        [Header("Components")]
        [SerializeField] private TextMeshProUGUI m_text;
        
        #endregion



        #region Unity Lifecycle

        private void Start()
        {
            DynamicParametersManager.Instance.Get(DynamicParameterType.ArtistsCount).EventOnUpgraded += DisplayArtistsInfo;
            TeamManager.Instance.EventOnArtistsUpdated += DisplayArtistsInfo;

            DisplayArtistsInfo();
        }

        #endregion



        #region Private Methods

        private void DisplayArtistsInfo()
        {
            int artistsCount = TeamManager.Instance.ArtistsCount;
            int emptyArtistsCount = artistsCount - TeamManager.Instance.BusyArtistsCount;
            
            m_text.text = string.Format(StringConstants.Formats.ArtistsFormat, emptyArtistsCount, artistsCount);
        }

        #endregion
    }
}

