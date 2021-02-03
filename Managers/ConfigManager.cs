using System;
using System.Collections.Generic;
using Data.Configs;
using Extensions;
using Interfaces;
using UnityEngine;

namespace Managers
{
    public class ConfigManager : Singleton<ConfigManager>, IManager
    {
        #region Fields

        [SerializeField] private List<BaseConfig> _configs = new List<BaseConfig>();

        private Dictionary<Type, object> _dependencies = new Dictionary<Type, object>();

        #endregion



        #region Public Methods

        public void Initialize()
        {
            for (int i = 0; i < _configs.Count; i++)
            {
                _dependencies.Add(_configs[i].GetType(), _configs[i]);
            }
        }
        

        public T Get<T>()
        {
            T config = default;

            if (_dependencies.ContainsKey(typeof(T)))
            {
                return (T)_dependencies[typeof(T)];
            }

            return config;
        }

        #endregion
    }
}

