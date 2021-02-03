using UnityEngine;

namespace Extensions
{
    public abstract class Singleton<T> : MonoBehaviour where T : Component
    {

        #region Fields

        [SerializeField] private bool m_isDontDestroy = false;
        /// <summary>
        /// The instance.
        /// </summary>
        private static T instance;

        #endregion

        
        
        #region Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Use this for initialization.
        /// </summary>
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                if (m_isDontDestroy)
                {
                    transform.parent = null;
                    DontDestroyOnLoad(this);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion

    }
}