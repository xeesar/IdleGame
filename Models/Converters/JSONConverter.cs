using Interfaces;
using Newtonsoft.Json;
using UnityEngine;

namespace Models.Converters
{
    public class JSONConverter : IConverter
    {
        #region Public Methods

        public void Serialize(object objectToConvert, string key)
        {
            string json = JsonConvert.SerializeObject(objectToConvert);

            PlayerPrefs.SetString(key, json);
        }

        public T Deserialize<T>(string key)
        {
            string json = PlayerPrefs.GetString(key);
            object result = JsonConvert.DeserializeObject<T>(json);

            if (result == null)
            {
                return default;
            }
            
            return (T)result;
        }

        #endregion
    }
}
