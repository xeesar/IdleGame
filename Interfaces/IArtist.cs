using Data;
using UnityEngine;
using UnityEngine.AI;
using View;

namespace Interfaces
{
    public interface IArtist
    {        
        IArtistState State { get; set; }
        IPaintSpray PaintSpray { get; set; }
        GraffitiBlockData GraffitiBlockData { get; set; }
        ArtistView ArtistView { get; }
        NavMeshAgent NavMeshAgent { get; }
        Transform Transform { get; }
        bool IsFinished { get; set; }
    }
}
