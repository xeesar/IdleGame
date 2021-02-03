namespace Interfaces
{
    public interface IArtistState
    {
        IArtist Artist { get; }
        
        IArtistState HandleState();
        
        void OnStateEnter(IArtist artist);
        void OnStateExit();
    }
}


