using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class GraffitiBlockData
    {
        #region Fields

        public List<PixelData> pixels;
        public int blockID;
        
        private bool _hasArtist;
        
        private float _progress;
        private float _drawTime;

        #endregion



        #region Properties

        public bool IsComplete => _progress >= 1f;

        public bool HasArtist
        {
            get => _hasArtist;
            set => _hasArtist = value;
        }

        public float Progress
        {
            get => _progress;
            set =>_progress = Mathf.Clamp01(value);
        }

        public float DrawTime
        {
            get => _drawTime;
            set => _drawTime = value;
        }

        #endregion
    }
}

