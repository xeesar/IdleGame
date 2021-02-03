using System;
using System.Collections.Generic;
using Data;
using Data.Configs;
using DG.Tweening;
using Enums;
using Extensions;
using Interfaces;
using Models.GraffitiDrawer;
using Models.Screens;
using Models.Tiles;
using Services;
using UnityEngine;

namespace Managers
{
    public class GraffitiManager : Singleton<GraffitiManager>, IManager
    {
        #region Fields

        public event Action EventBlockCompleted;
        public event Action EventGraffitiCompleted;
        
        private BaseGraffitiDrawer m_graffitiDrawer;

        private ProductionBuildingData m_productionBuildingData;
        
        #endregion



        #region Properties

        public float Progress => m_graffitiDrawer.GraffitiData.Progress;

        public bool IsGraffitiCompleted => m_graffitiDrawer.GraffitiData.Progress >= m_graffitiDrawer.GraffitiData.blocksCount;
        
        #endregion
        
        
        
        #region Public Methods

        public void Initialize()
        {
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();

            m_productionBuildingData = userProfileModel.CurrentCityProgress.GetProductionBuildingData(userProfileModel.OpenedBuildingId);

        }
        

        public void SetGraffitiDrawer(BaseGraffitiDrawer graffitiDrawer)
        {
            m_graffitiDrawer = graffitiDrawer;
            
            m_graffitiDrawer.EventBlockCompleted += OnBlockCompleted;
            m_graffitiDrawer.EventGraffitiCompleted += OnGraffitiCompleted;
        }

        public void DrawBlock(GraffitiBlockData blockData)
        {
            m_graffitiDrawer.Draw(blockData);

            m_productionBuildingData.drawProgress = Progress;
        }


        public bool HasEmptyBlock()
        {
            List<GraffitiBlockData> blocks = m_graffitiDrawer.GraffitiData.BlocksData;

            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].IsComplete || blocks[i].HasArtist) continue;

                return true;
            }

            return false;
        }
        
        
        public GraffitiBlockData GetEmptyBlock()
        {
            List<GraffitiBlockData> blocks = m_graffitiDrawer.GraffitiData.BlocksData;

            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i].IsComplete || blocks[i].HasArtist) continue;

                return blocks[i];
            }

            return null;
        }


        public Vector3 GetBlockPosition(GraffitiBlockData blockData)
        {
            int blockIndex = blockData.blockID;
            int blocksX = m_graffitiDrawer.GraffitiData.BlocksX;
            int blocksY = m_graffitiDrawer.GraffitiData.BlocksY;
            int blockXIndex = (blockIndex % blocksX);
            int blockYIndex = ((blockIndex - 1 ) / blocksX);

            float m_halfOfBlockWidth = (1f / blocksX) / 2f;
            float xTime = Mathf.Clamp01((float) blockXIndex / blocksX + m_halfOfBlockWidth);
            float yTime = (float) blockYIndex / blocksY;
            
            return m_graffitiDrawer.Holst.GetBlockPosition(xTime, yTime);
        }

        #endregion



        #region Private Methods

        private void OnBlockCompleted()
        {
            EventBlockCompleted?.Invoke();
            ServiceLocator.Instance.Get<IncomeService>().GiveIncomeFor(IncomeType.PerPixel);
            ParticleManager.Instance.SpawnParticle(ParticleType.Income, m_graffitiDrawer.Holst.Transform);
        }


        private void OnGraffitiCompleted()
        {
            SendDrawnEvent();

            m_productionBuildingData.isDrawn = true;

            ParticleManager.Instance.SpawnParticle(ParticleType.Confetti, FindObjectOfType<GraffitiAreaTile>().ConfetiSpawnPos);

            DOTween.Sequence().SetDelay(ConfigManager.Instance.Get<GameConfig>().DelayBeforeEndGame)
            .OnComplete(() =>
            {
                LevelManager.Instance.Building.HideScaffolding(ConfigManager.Instance.Get<GameConfig>().ScaffoldingHideDuration);
                EventGraffitiCompleted?.Invoke();
            });
            
            ScreensManager.Instance.GetScreen<GameScreen>().TryToShowBackButtonTutorial();
        }


        private void SendDrawnEvent()
        {
            IUserProfileModel userProfileModel = ServiceLocator.Instance.Get<IUserProfileModel>();
            DynamicParametersManager dynamicParametersManager = DynamicParametersManager.Instance;
            
            AnalyticEvent analyticEvent = new AnalyticEvent(StringConstants.AnalyticsEvents.DrawnGraffiti);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.BuildingId, userProfileModel.OpenedBuildingId);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.CountOfArtists, dynamicParametersManager.Get(DynamicParameterType.ArtistsCount).Value);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.MorePerBlock, dynamicParametersManager.Get(DynamicParameterType.RespectIncomePerBlock).Level);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.RunningSpeed, dynamicParametersManager.Get(DynamicParameterType.RunningSpeed).Level);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.DrawingSpeed, dynamicParametersManager.Get(DynamicParameterType.DrawingSpeed).Level);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.CapacityOfCans, dynamicParametersManager.Get(DynamicParameterType.SprayBottleCapacity).Level);
            analyticEvent.AddParameter(StringConstants.AnalyticsEventsParameters.CurrencyType, userProfileModel.OpenedBuildingCurrencyType.ToString().ToLower());
        }

        #endregion
    }
}
