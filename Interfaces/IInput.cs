using System;
using UnityEngine;

namespace Interfaces
{
    public interface IInput
    {
        event Action EventOnPointerDown;
        event Action EventOnPointerDrag;
        event Action EventOnPointerUp;

        
        Vector2 Direction { get; }
    }
}
