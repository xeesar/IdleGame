using System;
using UnityEngine;

namespace Extensions
{
    public static class CustomPlayerPrefs
    {
        #region Public Methods

        public static bool GetBool(string key, bool defaultValue)
        {
            return GetInt(key, Convert.ToInt32(defaultValue)) == 1;
        }

        public static void SetBool(string key, bool value)
        {
            SetInt(key, Convert.ToInt32(value));
        }
        
        
        public static int GetInt(string key, int defaultValue)
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }


        public static void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }


        public static float GetFloat(string key, float defaultValue)
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }


        public static void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }
        
        
        public static DateTime GetDateTime(string key, DateTime defaultValue)
        {
            string defaultStringValue = defaultValue.Year + "." + defaultValue.Month +
                                        "." + defaultValue.Day + "." + defaultValue.Hour + "." + defaultValue.Minute + "." + defaultValue.Second;

            string dateTime = GetString(key, defaultStringValue);
            string[] splittedString = dateTime.Split('.');
                
            return new DateTime(Convert.ToInt32(splittedString[0]), Convert.ToInt32(splittedString[1]), Convert.ToInt32(splittedString[2]), 
                Convert.ToInt32(splittedString[3]), Convert.ToInt32(splittedString[4]), Convert.ToInt32(splittedString[5]));
        }

        
        public static void SetDateTime(string key, DateTime value)
        {
            string date = value.Year + "." + value.Month +
                          "." + value.Day + "." + value.Hour + "." + value.Minute + "." + value.Second;
            SetString(key, date);
        }


        public static string GetString(string key, string defaultValue)
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }


        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
        
        #endregion
    }
}

