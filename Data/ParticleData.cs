using Enums;
using Models.Particles;

namespace Data
{
    [System.Serializable]
    public struct ParticleData
    {
        public ParticleType type;
        public BaseParticle prefab;
    }
}
