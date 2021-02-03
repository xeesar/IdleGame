using Interfaces;

namespace Models.UserProfile
{
    public class UserProfile : IUserProfile
    {
        #region Properties

        public IUserInventory UserInventory { get; set; }

        public IUserStats UserStats { get; set; }
        
        public IGameOptions GameOptions { get; set; }

        #endregion



        #region Public Methods

        public UserProfile(IUserInventory inventory, IUserStats userStats, IGameOptions gameOptions)
        {
            UserInventory = inventory;
            UserStats = userStats;
            GameOptions = gameOptions;
        }

        #endregion
    }
}

