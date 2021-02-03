using UnityEngine;

namespace Models.Districts
{
    public class EnvironmentElement : DistrictElement
    {
        #region Fields
        
        [Header("Components")]
        [SerializeField] private SpriteRenderer m_sprite;

        #endregion
        
        
        
        #region Properties

        protected override SpriteRenderer SpriteRenderer => m_sprite;

        #endregion
    }
}

