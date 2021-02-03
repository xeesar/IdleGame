using Data;
using UnityEngine;

namespace Interfaces
{
    public interface IGraffitiHolst
    {
        Transform Transform { get; }
        
        float Width { get; }
        
        float Height { get; }
        
        void Initialize(int width, int height);
        void Draw(GraffitiBlockData graffitiBlockData);
        Vector3 GetBlockPosition(float xTime, float yTime);
    }
}
