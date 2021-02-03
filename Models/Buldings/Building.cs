using Data;
using Data.Configs;
using DG.Tweening;
using Interfaces;
using Managers;
using Models.Graffiti;
using Models.Ladder;
using Models.Tiles;
using UnityEngine;

namespace Models.Buildings
{
    public class Building : MonoBehaviour
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private GraffitiAreaTile m_graffitiAreaTile =  null;
        [SerializeField] private GraffitiHolst m_graffitiHolst = null;
        [SerializeField] private Scaffolding m_scaffolding = null;
        
        [SerializeField] private Sprite m_defaultTileSprite;
        [SerializeField] private Sprite m_completedTileSprite;
        
        #endregion



        #region Properties

        public IGraffitiHolst Holst => m_graffitiHolst;

        public GraffitiAreaTile GraffitiAreaTile => m_graffitiAreaTile;

        public Scaffolding Scaffolding => m_scaffolding;
        
        public Sprite DefaultTileSprite => m_defaultTileSprite;

        public Sprite CompletedTileSprite => m_completedTileSprite;

        #endregion



        #region Public Methods

        public void HideScaffolding(float time)
        {
            var gameConfig = ConfigManager.Instance.Get<GameConfig>();
            float endValue = m_scaffolding.transform.position.y - gameConfig.ScaffoldingYOffset;
            
            m_scaffolding.transform.DOMoveY(endValue, time).SetEase(Ease.InOutSine);
        }

        #endregion
    }
}