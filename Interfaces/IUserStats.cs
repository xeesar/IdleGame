namespace Interfaces
{
    public interface IUserStats
    {
        bool IsFirstSession { get; set; }
        
        int TutorialStage { get; set; }
    }
}


