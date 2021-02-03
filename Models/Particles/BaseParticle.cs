using UnityEngine;

namespace Models.Particles
{
    public abstract class BaseParticle : MonoBehaviour
    {
        #region Fields

        [Header("Options")] 
        [SerializeField] private float m_lifeTime = 2f;

        #endregion
        
        
        
        #region Public Methods

        public virtual void Activate()
        {
            Destroy(gameObject, m_lifeTime);
        }

        #endregion
    }
}
