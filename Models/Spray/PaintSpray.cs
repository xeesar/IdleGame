using Enums;
using Interfaces;
using Managers;
using Models.DynamicParameters;
using UnityEngine;

namespace Models.Spray
{
    public class PaintSpray : IPaintSpray
    {
        #region Properties

        public float Capacity { get; set; }

        #endregion



        #region Public Methods

        public void Sprinkle()
        {
            DynamicParametersManager dynamicParametersManager = DynamicParametersManager.Instance;

            float sprayRate = dynamicParametersManager.Get(DynamicParameterType.SprayBottleCapacity).FirstValue /
                              dynamicParametersManager.Get(DynamicParameterType.DrawingSpeed).Value;
            Capacity -= sprayRate * Time.deltaTime;
        }

        #endregion
    }
}

