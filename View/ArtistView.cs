using System;
using Enums;
using Extensions;
using Managers;
using UnityEngine;

namespace View
{
    public class ArtistView : MonoBehaviour
    {
        #region Fields

        [Header("Components")]
        [SerializeField] private Animator m_animator = null;
        [SerializeField] private GameObject m_trailObject;

        private readonly int m_runningHash = Animator.StringToHash("IsRunning");
        private readonly int m_swarmingHash = Animator.StringToHash("IsSwarming");
        private readonly int m_paintingHash = Animator.StringToHash("IsPainting");
        private readonly int m_getSprayCanHash = Animator.StringToHash("GetSprayCan");

        #endregion



        #region Public Methods

        public void SetTrailActive(bool isActive)
        {
            m_trailObject.SetActive(isActive);
        }

        
        public float GetClipDuration(AnimationType animationType)
        {
            AnimationClip clip = GetClip(animationType);

            if (clip == null)
            {
                return 0;
            }
                
            return clip.length;
        }


        public bool IsOnState(AnimationType animationType)
        {
            return m_animator.GetCurrentAnimatorStateInfo(0).IsName(animationType.ToString());
        }
        
        
        public void PlayAnimation(AnimationType animationType)
        {
            switch (animationType)
            {
                case AnimationType.Run:
                    SetBool(m_runningHash, true);
                    break;
                case AnimationType.Paint:
                    SetBool(m_paintingHash, true);
                    break;
                case AnimationType.Swarm:
                    SetBool(m_swarmingHash, true);
                    break;
                case AnimationType.TakeSprayCan:
                    SetTrigger(m_getSprayCanHash);
                    break;
            }
        }


        public void StopAnimation(AnimationType animationType)
        {
            switch (animationType)
            {
                case AnimationType.Run:
                    SetBool(m_runningHash, false);
                    break;
                case AnimationType.Paint:
                    SetBool(m_paintingHash, false);
                    break;
                case AnimationType.Swarm:
                    SetBool(m_swarmingHash, false);
                    break;
            }
        }
        
        #endregion



        #region Private Methods

        private void SetTrigger(int animationHash)
        {
            m_animator.SetTrigger(animationHash);
        }


        private void SetBool(int animationHash, bool isActive)
        {
            m_animator.SetBool(animationHash, isActive);
        }


        private AnimationClip GetClip(AnimationType clipType)
        {
            AnimationClip[] clips = m_animator.runtimeAnimatorController.animationClips;
            string clipName = GetClipName(clipType);
            
            for (int i = 0; i < clips.Length; i++)
            {
                if (clips[i].name == clipName)
                {
                    return clips[i];
                }
            }

            return null;
        }


        private string GetClipName(AnimationType animationType)
        {
            switch (animationType)
            {
                case AnimationType.Run:
                    return "";
                case AnimationType.Paint:
                    return "";
                case AnimationType.Swarm:
                    return "";
                case AnimationType.TakeSprayCan:
                    return StringConstants.AnimationClipNames.TakeFromTable;
            }
            
            return "";
        }

        #endregion
    }
}
