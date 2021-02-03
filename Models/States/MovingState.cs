using DG.Tweening;
using Enums;
using Interfaces;
using Managers;
using Models.DynamicParameters;
using Models.Ladder;
using UnityEngine;
using UnityEngine.AI;

namespace Models.States
{
    public class MovingState : BaseArtistState
    {
        #region Fields

        private Vector3 m_destination = Vector3.zero;

        private Scaffolding m_scaffolding;
        
        private bool m_isMoveToLadder = false;
        private bool m_canMoveToLadder;
        
        #endregion
        
        
        
        #region Public Methods

        public MovingState(Vector3 destination, bool canMoveToLadder = true)
        {
            m_destination = destination;
            m_canMoveToLadder = canMoveToLadder;
            
            m_scaffolding = LevelManager.Instance.Building.Scaffolding;
        }
        

        public override IArtistState HandleState()
        {
            NavMeshAgent agent = Artist.NavMeshAgent;
            
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                if (agent.hasPath)
                {
                    Vector3 artistForward = Artist.Transform.forward;
                    
                    Vector3 toTarget = agent.steeringTarget - Artist.Transform.position;
                    float turnAngle = Vector3.Angle(artistForward, toTarget);
                    agent.acceleration = turnAngle * agent.speed;
                    Artist.Transform.forward = Vector3.Lerp(artistForward, toTarget, Time.deltaTime * agent.angularSpeed);
                }
                
                return null;
            }
            
            if (m_isMoveToLadder)
            {
                Artist.NavMeshAgent.isStopped = true;
                Artist.NavMeshAgent.ResetPath();
                return new ClimbState(m_destination);
            }
            
            if (Artist.PaintSpray == null || Artist.PaintSpray.Capacity <= 0)
            {
                return new GetSprayState();
            }
            
            RotateToGraffiti();
            return new DrawState();
        }
        

        public override void OnStateEnter(IArtist artist)
        {
            base.OnStateEnter(artist);

            Artist.NavMeshAgent.enabled = true;
            
            if (IsNeedToMoveOnLadder())
            {
                Vector3 ladderPos = m_scaffolding.GetLadderStartPos(Artist.Transform.position.y, m_destination.y - Artist.NavMeshAgent.height);
                Artist.NavMeshAgent.SetDestination(ladderPos);
                m_isMoveToLadder = true;
            }
            else
            {
                Artist.NavMeshAgent.SetDestination(m_destination);
            }

            Artist.ArtistView.PlayAnimation(AnimationType.Run);
        }


        public override void OnStateExit()
        {
            Artist.ArtistView.StopAnimation(AnimationType.Run);
            Artist.NavMeshAgent.enabled = false;
        }

        #endregion



        #region Private Methods

        private void RotateToGraffiti()
        {
            Transform characterTransform = Artist.Transform;
            Vector3 lookDirection = m_destination - characterTransform.position;
            lookDirection.y = characterTransform.position.y;

            characterTransform.DORotate(lookDirection, 1f);
        }


        private bool IsNeedToMoveOnLadder()
        {
            if (!m_canMoveToLadder) return false;

            int targetFloor = m_scaffolding.CalculateFloor(m_destination.y - Artist.NavMeshAgent.height);
            int playerFloor = m_scaffolding.CalculateFloor(Artist.Transform.position.y);

            return targetFloor != playerFloor;
        }

        #endregion
    }
}

