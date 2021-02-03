using System;
using Interfaces;
using UnityEngine;

namespace Services
{
    public class InputService
    {
        #region Events

        public event Action eventOnPointerDown = delegate {  };
        public event Action eventOnPointerDrag = delegate {  };
        public event Action eventOnPointerUp = delegate {  };

        #endregion
        
        
        
        #region Fields

        private IInput m_input = null;

        #endregion



        #region Properties

        public bool IsActive { get; private set; } = true;
        
        public bool IsDragged { get; private set; }

        public Vector2 Direction => IsActive ? m_input.Direction : Vector2.zero;
        
        #endregion
        


        #region Public Methods

        public void AddInput(IInput input)
        {
            IsActive = true;
            
            m_input = input;
            m_input.EventOnPointerDown += OnPointerDown;
            m_input.EventOnPointerDrag += OnPointerDrag;
            m_input.EventOnPointerUp += OnPointerUp;
        }


        public void RemoveInput(IInput input)
        {
            IsActive = false;

            m_input = null;
            input.EventOnPointerDown -= OnPointerDown;
            input.EventOnPointerDrag -= OnPointerDrag;
            input.EventOnPointerUp -= OnPointerUp;
        }

        #endregion



        #region Private Methods

        private void OnPointerDown()
        {
            if(!IsActive) return;

            IsDragged = false;
            
            eventOnPointerDown?.Invoke();
        }


        private void OnPointerDrag()
        {
            if(!IsActive) return;

            IsDragged = true;
            eventOnPointerDrag?.Invoke();
        }


        private void OnPointerUp()
        {
            if(!IsActive) return;

            eventOnPointerUp?.Invoke();
        }

        #endregion
    }
}

