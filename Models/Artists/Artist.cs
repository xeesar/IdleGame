using System;
using Data;
using Enums;
using Interfaces;
using Managers;
using Models.States;
using UnityEngine;
using UnityEngine.AI;
using View;

namespace Models.Artists
{
    public class Artist : MonoBehaviour, IArtist
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private ArtistView m_artistView = null;
        [SerializeField] private NavMeshAgent m_navMeshAgent = null;

        private GraffitiBlockData m_graffitiBlockData = null;
        
        #endregion
        
        
        
        #region Properties

        public IPaintSpray PaintSpray { get; set; }
        
        public IArtistState State { get; set; }

        public GraffitiBlockData GraffitiBlockData
        {
            get => m_graffitiBlockData;
            set
            {
                m_graffitiBlockData = value;
                m_graffitiBlockData.HasArtist = true;
            }
        }

        public ArtistView ArtistView => m_artistView;

        public NavMeshAgent NavMeshAgent => m_navMeshAgent;

        public Transform Transform => transform;

        public bool IsFinished { get; set; } = false;

        #endregion



        #region Unity Lifecycle

        private void OnEnable()
        {
            UpdateManager.EventOnUpdate += OnUpdate;
        }


        private void OnDisable()
        {
            UpdateManager.EventOnUpdate -= OnUpdate;
        }
        

        private void Start()
        {
            m_navMeshAgent.autoTraverseOffMeshLink = false;
            m_navMeshAgent.updateRotation = false;
            ChangeState(new IdleState());
        }

        #endregion
        
        

        #region Private Methods

        private void OnUpdate(float deltaTime)
        {
            HandleState();
            HandleSpeedBonus();
        }
        
        
        private void HandleState()
        {
            UpdateSpeed();
            
            IArtistState state = State?.HandleState();
            
            ChangeState(state);
        }


        private void HandleSpeedBonus()
        {
            bool isBonusActive = BonusManager.Instance.IsBonusActive(BonusType.MovingSpeed);
            
            m_artistView.SetTrailActive(isBonusActive);
        }


        private void ChangeState(IArtistState state)
        {
            if (state == null) return;
           
            State?.OnStateExit();
            State = state;
            State.OnStateEnter(this);
        }


        private void UpdateSpeed()
        {
            m_navMeshAgent.speed = DynamicParametersManager.Instance.Get(DynamicParameterType.RunningSpeed).Value;
        }

        #endregion
    }
}

