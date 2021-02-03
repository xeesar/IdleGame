using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models.Ladder
{
    public class Scaffolding : MonoBehaviour
    {
        #region Fields

        [Header("Options")] 
        [SerializeField] private float m_floorHeight = 0.7f;
        
        [SerializeField] private List<Transform> m_enterPoses;
        [SerializeField] private List<Transform> m_exitPoses;

        #endregion



        #region Properties

        public float FloorHeight => m_floorHeight;

        #endregion



        #region Public Methods

        public Vector3 GetLadderStartPos(float artistYPos, float destinationYPos)
        {
            int destinationFloor = CalculateFloor(destinationYPos);
            int artistFloor = CalculateFloor(artistYPos);
            
            if (artistFloor < destinationFloor)
            {
                return m_enterPoses[artistFloor].position;
            }

            return m_exitPoses[artistFloor].position;
        }


        public Vector3 GetLadderDestinationPos(float artistYPos, float destinationYPos)
        {
            int destinationFloor = CalculateFloor(destinationYPos);
            int artistFloor = CalculateFloor(artistYPos);

            if (artistFloor < destinationFloor)
            {
                return m_enterPoses[destinationFloor].position;
            }
            
            return m_exitPoses[destinationFloor].position;
        }
        

        public int CalculateFloor(float targetYPos)
        {
            float targetY = Mathf.Clamp(targetYPos, 0, Int32.MaxValue);
            int floor = Mathf.RoundToInt(targetY / m_floorHeight);
            int clampedFloor = Mathf.Clamp(floor, 0, m_enterPoses.Count - 1);

            return clampedFloor;
        }

        #endregion
    }
}

