using UnityEngine;

namespace Data
{
    [System.Serializable]
    public struct AudioClipData
    {
        [SerializeField] private AudioClip m_clip;
        [SerializeField] [Range(0, 1)] private float m_volume;


        public AudioClip Clip => m_clip;

        public float Volume => m_volume;
    }
}
