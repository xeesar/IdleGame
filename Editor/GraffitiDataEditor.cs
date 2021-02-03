using Data;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(GraffitiData))]
    public class GraffitiDataEditor : UnityEditor.Editor
    {
        #region Fields
        
        private GraffitiData _graffitiData;
        
        #endregion



        #region Unity Lifecycle

        private void OnEnable()
        {
            _graffitiData = (GraffitiData)target;
        }


        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            DisplayTextureSize();
            DrawButtons();
            DrawGraffitiTexture();
            
        }

        #endregion



        #region Private Methods

        private void DisplayTextureSize()
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            int width = _graffitiData.previewTexture ? _graffitiData.previewTexture.width : 0;
            int height = _graffitiData.previewTexture ? _graffitiData.previewTexture.height : 0;

            GUILayout.FlexibleSpace();
            GUILayout.Label($"Width: {width}");
            GUILayout.FlexibleSpace();
            GUILayout.Label($"Height: {height}");
            GUILayout.FlexibleSpace();
            
            EditorGUILayout.EndHorizontal();
        }
        
        
        private void DrawButtons()
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Calculate Blocks Count"))
            {
                CalculateBlocksCount();
                EditorUtility.SetDirty(target);
            }
                
            EditorGUILayout.EndHorizontal();
        }


        private void CalculateBlocksCount()
        {
            int blocksCount = 0;
            var layers = _graffitiData.layers;

            for (int i = 0; i < layers.Count; i++)
            {
                blocksCount += ParseLayer(layers[i]);
            }

            _graffitiData.blocksCount = blocksCount;
        }
        
        
        private int ParseLayer(Texture2D layerTexture)
        {
            int blockXCount = _graffitiData.BlocksX;
            int blockYCount = _graffitiData.BlocksY;
            int blocksCount = 0;
            
            for (int j = blockYCount - 1; j >= 0; j--)
            {
                for (int i = 0; i < blockXCount; i++)
                {
                    int startXPos = i * _graffitiData.polygonWidth;
                    int startYPos = j * _graffitiData.polygonHeight;

                    int pixels = GetGraffitiBlockData(startXPos, startYPos, layerTexture);

                    if (pixels > 0)
                    {
                        blocksCount++;
                    }
                }
            }

            return blocksCount;
        }
        
        
        private int GetGraffitiBlockData(int startXPos, int startYPos, Texture2D layerTexture)
        {
            int pixels = 0;
            for (int x = startXPos; x < startXPos + _graffitiData.polygonWidth; x++)
            {
                for (int y = startYPos; y < startYPos + _graffitiData.polygonHeight; y++)
                {
                    Color pixel = layerTexture.GetPixel(x, y);
                    
                    if(pixel.a <= 0) continue;

                    pixels++;
                }
            }

            return pixels;
        }
        
        
        private void DrawGraffitiTexture()
        {
            EditorGUILayout.Space();
            
            GUILayout.Label(_graffitiData.previewTexture);
        }

        #endregion
    }
}

