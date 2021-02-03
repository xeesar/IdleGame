using System.Collections.Generic;
using Data;
using Enums;
using Extensions;
using Interfaces;
using Models.Particles;
using UnityEngine;

namespace Managers
{
    public class ParticleManager : Singleton<ParticleManager>, IManager
    {
        #region Fields

        [Header("Options")] 
        [SerializeField] private List<ParticleData> m_particlesData = null;

        private Dictionary<ParticleType, BaseParticle> m_particles;

        #endregion



        #region Public Methods

        public void Initialize()
        {
            InitializeParticles();
        }
        

        public void SpawnParticle(ParticleType type, Transform parent)
        {
            if(!m_particles.ContainsKey(type)) return;

            BaseParticle baseParticles = Instantiate(m_particles[type], parent);
            baseParticles.transform.localPosition = Vector3.zero;
            
            baseParticles.Activate();
        }

        #endregion



        #region Private Methods

        private void InitializeParticles()
        {
            m_particles = new Dictionary<ParticleType, BaseParticle>();
            
            for (int i = 0; i < m_particlesData.Count; i++)
            {
                m_particles.Add(m_particlesData[i].type, m_particlesData[i].prefab);
            }
        }

        #endregion
    }
}
