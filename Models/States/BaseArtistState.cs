using Interfaces;
using UnityEngine;

namespace Models.States
{
    public abstract class BaseArtistState : IArtistState
    {
        #region Public Methods

        public IArtist Artist { get; private set; }

        public abstract IArtistState HandleState();

        public virtual void OnStateEnter(IArtist artist)
        {
            Artist = artist;
        }

        public virtual void OnStateExit()
        {
        }

        #endregion
    }
}
