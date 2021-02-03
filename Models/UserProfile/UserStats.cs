using Interfaces;

namespace Models.UserProfile
{
    public class UserStats : IUserStats
    {
        #region Properties

        public bool IsFirstSession { get; set; }
        
        public int TutorialStage { get; set; }

        #endregion
    }
}
