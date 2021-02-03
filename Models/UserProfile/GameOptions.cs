using Interfaces;

namespace Models.UserProfile
{
    public class GameOptions : IGameOptions
    {
        #region Properties

        public bool IsSoundOn { get; set; }
        
        public bool IsMusicOn { get; set; }

        #endregion
    }
}