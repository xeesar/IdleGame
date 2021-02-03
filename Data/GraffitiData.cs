using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Data/Graffiti Data", fileName = "GraffitiData")]
    public class GraffitiData : ScriptableObject
    {
        #region Fields

        public Texture2D previewTexture;

        public List<Texture2D> layers;
        
        public int polygonWidth;
        public int polygonHeight;

        public int blocksCount;

        private List<GraffitiBlockData> _graffitiBlocksData;
        
        #endregion



        #region Properties

        public List<GraffitiBlockData> BlocksData => _graffitiBlocksData;
        
        public float Progress => _graffitiBlocksData.Sum(data => data.Progress);
        
        public int BlocksX => previewTexture.width / polygonWidth;
        
        public int BlocksY => previewTexture.height / polygonHeight;

        #endregion
        


        #region Public Methods

        public void PrepareGraffitiBlocksData()
        {
            _graffitiBlocksData = new List<GraffitiBlockData>();

            for (int i = 0; i < layers.Count; i++)
            {
                ParseLayer(layers[i]);
            }
        }

        #endregion



        #region Private Methods

        private void ParseLayer(Texture2D layerTexture)
        {
            int blockXCount = BlocksX;
            int blockYCount = BlocksY;
            int blockID = 0;
            
            for (int j = blockYCount - 1; j >= 0; j--)
            {
                for (int i = 0; i < blockXCount; i++)
                {
                    int startXPos = i * polygonWidth;
                    int startYPos = j * polygonHeight;

                    GraffitiBlockData block = GetGraffitiBlockData(startXPos, startYPos, layerTexture);
                    
                    if (block.pixels.Count > 0)
                    {
                        block.blockID = blockID;
                        _graffitiBlocksData.Add(block);
                    }

                    blockID++;
                }
            }
        }
        
        
        private GraffitiBlockData GetGraffitiBlockData(int startXPos, int startYPos, Texture2D layerTexture)
        {
            GraffitiBlockData graffitiBlockData = new GraffitiBlockData();
            List<PixelData> pixels = new List<PixelData>();
            
            for (int x = startXPos; x < startXPos + polygonWidth; x++)
            {
                for (int y = startYPos; y < startYPos + polygonHeight; y++)
                {
                    Color pixel = layerTexture.GetPixel(x, y);
                    
                    if(pixel.a <= 0) continue;
                    
                    pixels.Add(new PixelData
                    {
                        xPos = x,
                        yPos = y,
                        color = pixel
                    });
                }
            }

            graffitiBlockData.pixels = pixels;
            return graffitiBlockData;
        }

        #endregion
    }
}

