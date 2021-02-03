using System.Collections.Generic;
using UnityEngine;

namespace Models.Particles
{
    public class UnityParticle : BaseParticle
    {
        [Header("Components")] 
        [SerializeField] private List<ParticleSystem> m_particles;


        public override void Activate()
        {
            base.Activate();

            for (int i = 0; i < m_particles.Count; i++)
            {
                m_particles[i].Play();
            }
        }
    }
}

