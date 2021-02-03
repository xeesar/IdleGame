using Controllers;
using DG.Tweening;
using UnityEngine;

namespace Models.States.CityCamera
{
    public class TargetMoveCameraState : CityCameraState
    {
        #region Fields

        private Vector3 m_targetPos;

        #endregion
        
        
        
        #region Public Methods

        public TargetMoveCameraState(Vector3 targetPos)
        {
            m_targetPos = targetPos;
        }
        
        
        public override void OnStateEnter(CityCameraController cameraController)
        {
            base.OnStateEnter(cameraController);

            MoveToTarget();
        }

        #endregion



        #region Private Methods

        private void MoveToTarget()
        {
            DOTween.To(() => m_cameraController.CameraPosition,
                    x => m_cameraController.CameraPosition = x, m_targetPos, m_cameraConfig.moveToTargetTime)
                .SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    m_cameraController.ChangeCameraState(new FreeMoveCameraState());
                });
        }

        #endregion
    }
}

