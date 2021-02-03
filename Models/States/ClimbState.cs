using DG.Tweening;
using Enums;
using Interfaces;
using Managers;
using Models.Ladder;
using UnityEngine;

namespace Models.States
{
    public class ClimbState : BaseArtistState
    {
        #region Fields

        private Vector3 m_destination = Vector3.zero;

        private IArtistState m_artistState = null;
        
        #endregion
        
        
        
        #region Public Methods

        public ClimbState(Vector3 destination)
        {
            m_destination = destination;
        }

        
        public override IArtistState HandleState()
        {
            return m_artistState;
        }


        public override void OnStateEnter(IArtist artist)
        {
            base.OnStateEnter(artist);
            
            m_artistState = null;

            RotateToOffMeshLink().OnComplete(() =>
            {
                Artist.ArtistView.PlayAnimation(AnimationType.Swarm);
                Climb();
            });
        }

        
        public override void OnStateExit()
        {
            base.OnStateExit();
            
            Artist.ArtistView.StopAnimation(AnimationType.Swarm);
        }

        #endregion



        #region Private Methods

        private Tween RotateToOffMeshLink()
        {          
            Vector3 characterPosition = Artist.Transform.position;
            
            Vector3 destination = characterPosition;
            destination.x += 5f;

            Vector3 lookDirection = destination - characterPosition;
            Quaternion rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            Tween tween = Artist.Transform.DORotate(rotation.eulerAngles, 0.2f);

            return tween;
        }

        private void Climb()
        {
            Scaffolding scaffolding = LevelManager.Instance.Building.Scaffolding;
            
            Vector3 ladderEndPos = scaffolding.GetLadderDestinationPos(Artist.Transform.position.y, m_destination.y - Artist.NavMeshAgent.height);
            Vector3 destination = ladderEndPos + Vector3.up * Artist.NavMeshAgent.baseOffset;
            
            Artist.Transform.DOMove(destination, 1f / Artist.NavMeshAgent.speed).SetEase(Ease.Linear).OnComplete(() =>
            {
                m_artistState = new MovingState(m_destination, false);
            });
            
        }
        #endregion
    }
}
