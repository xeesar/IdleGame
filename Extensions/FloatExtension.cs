using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extensions
{
    public static class FloatExtension 
    {
        #region Fields

        private static readonly SortedDictionary<float, string> m_abbrevations = new SortedDictionary<float, string>
        {
            {0.001f,"K"},
            {1, "M" },
            {1000, "B" },
            {1000000,"T"},
            {1000000000,"P"},
            {1000000000000,"E"},
            {1000000000000000,"Z"},
            {1000000000000000000,"Y"}
        };


        private static readonly int m_abbrevationMultiplier = 1000000;

        #endregion



        #region Public Methods

        public static string AbbreviateNumber(this float number)
        {
            for (int i = m_abbrevations.Count - 1; i >= 0; i--)
            {
                KeyValuePair<float, string> abbervation = m_abbrevations.ElementAt(i);

                float abbervationNumber = abbervation.Key * m_abbrevationMultiplier;
                
                if (number >= abbervationNumber)
                {
                    float floatNumber = number / abbervationNumber;
                    return $"{Round(floatNumber, 2)}{abbervation.Value}";
                }
            }

            return Round(number, 2).ToString();
        }

        #endregion



        #region Private Methods

        private static float Round(float value, int digits)
        {
            float mult = Mathf.Pow(10.0f, (float)digits);
            return Mathf.Round(value * mult) / mult;
        }

        #endregion
    }
}
