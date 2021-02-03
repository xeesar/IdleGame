using Data.Configs;
using Managers;

namespace Models.States
{
    public class IdleCameraState : BaseCameraState
    {
        #region Properties

        public override float MinAngle => CameraController.MinAngle;

        public override float MaxAngle => CameraController.MaxAngle;

        #endregion
    }
}

