namespace Interfaces
{
    public interface IUserProfile
    {
        IUserInventory UserInventory { get; set; }
        
        IUserStats UserStats { get; set; }
        
        IGameOptions GameOptions { get; set; }
    }
}

