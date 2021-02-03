using System;
using Data;
using Enums;
using Extensions;
using Interfaces;
using Models.DynamicParameters;
using Services;
using UnityEngine;

namespace Managers
{
    public class TimeManager : Singleton<TimeManager>, IManager
    {
        #region Properties

        public float SecondsOffline
        {
            get
            {
                CityProgressData cityProgressData = ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCityProgress;
                DateTime exitTime = cityProgressData.exitTime;
                return (float)cityProgressData.enterTime.Subtract(exitTime).TotalSeconds;
            }
        }
        
        public int MinutesOffline
        {
            get
            {
                CityProgressData cityProgressData = ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCityProgress;
                DateTime exitTime = cityProgressData.exitTime;
                return (int)cityProgressData.enterTime.Subtract(exitTime).TotalMinutes;
            }
        }


        public TimeSpan OfflineTime
        {
            get
            {
                CityProgressData cityProgressData = ServiceLocator.Instance.Get<IUserProfileModel>().CurrentCityProgress;
                DateTime exitTime = cityProgressData.exitTime;
                return cityProgressData.enterTime.Subtract(exitTime);
            }
        }


        public float TimeOfDrawingOnePixel => GetTimeOfDrawingOnePixel();

        #endregion



        #region Public Methods

        public void Initialize()
        {
            Application.targetFrameRate = 60;
        }

        #endregion
        


        #region Private Methods

        private float GetTimeOfDrawingOnePixel()
        {
            DynamicParametersManager dynamicParametersManager = DynamicParametersManager.Instance;
            DynamicParameter sprayCapacity = dynamicParametersManager.Get(DynamicParameterType.SprayBottleCapacity);
            
            //TODO вынести в константу
            float runningTime = 5 / dynamicParametersManager.Get(DynamicParameterType.RunningSpeed).Value;
            float drawingTime = (dynamicParametersManager.Get(DynamicParameterType.DrawingSpeed).Value * sprayCapacity.Value) / sprayCapacity.FirstValue;
            float sprayCoast = sprayCapacity.Value / sprayCapacity.FirstValue;
            
            return (runningTime + drawingTime) / sprayCoast;
        }

        #endregion
    }
}
