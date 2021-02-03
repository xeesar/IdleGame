namespace Interfaces
{
    public interface IConverter
    {
        void Serialize(object objectToConvert, string key);
        T Deserialize<T>(string key);
    }
}

