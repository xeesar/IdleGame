using System;
using Extensions;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class UpdateManager : Singleton<UpdateManager>, IManager
    {
        #region Fields

        public static event Action<float> EventOnUpdate;
        public static event Action<float> EventOnFixedUpdate;
        public static event Action<float> EventOnLateUpdate;

        #endregion



        #region Unity Lifecycle

        private void Update()
        {
            EventOnUpdate?.Invoke(Time.deltaTime);
        }


        private void FixedUpdate()
        {
            EventOnFixedUpdate?.Invoke(Time.deltaTime);
        }


        private void LateUpdate()
        {
            EventOnLateUpdate?.Invoke(Time.deltaTime);
        }

        #endregion



        #region Public Methods

        public void Initialize()
        {
            
        }

        #endregion
    }
}