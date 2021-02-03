namespace Interfaces
{
    public interface IStorage
    {
        #region Properties

        IConverter Converter { get; set; }

        #endregion


        #region Methods

        void Save(IUserProfileModel userProfileModel);
        void Load(IUserProfileModel userProfileModel);
        void Reset();

        #endregion
    }
}


