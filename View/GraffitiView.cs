using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using DG.Tweening;
using UnityEngine;

namespace View
{
    public class GraffitiView : MonoBehaviour
    {
        #region Fields

        [Header("Components")] 
        [SerializeField] private Renderer _renderer = null;

        [Header("Options")] 
        [SerializeField] private AnimationCurve _displayCurve = null;

        private Texture2D _holst;
        
        #endregion



        #region Public methods

        public void Setup(int width, int height)
        {
            _holst = MakeTexture(width, height);
            _renderer.material.mainTexture = _holst;
        }


        public void DisplayBlock(GraffitiBlockData blockData)
        {
            List<PixelData> pixels = blockData.pixels;
            
            DisplayPixels(pixels, blockData.Progress);
        }

        #endregion

        

        #region Private methods

        private Texture2D MakeTexture(int width, int height)
        {
            Texture2D texture = new Texture2D(width, height, TextureFormat.ARGB32, false);

            Color fillColor = Color.clear;
            Color[] fillPixels = new Color[width * height];
 
            for (int i = 0; i < fillPixels.Length; i++)
            {
                fillPixels[i] = fillColor;
            }
 
            texture.SetPixels(fillPixels);
            texture.Apply();
            
            return texture;
        }
        
        
        private void DisplayPixels(List<PixelData> pixels, float progress)
        {
            for (int i = 0; i < pixels.Count; i++)
            {
                PixelData pixel = pixels[i];
                Color color = pixel.color;
                if(color.a <= 0) continue;
                
                color.a *= _displayCurve.Evaluate(progress);
                    
                _holst.SetPixel(pixel.xPos, pixel.yPos, color);
            }
                
            _holst.Apply();
        }
        
        #endregion
    }
}
