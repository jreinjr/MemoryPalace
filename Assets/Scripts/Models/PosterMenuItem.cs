using System;
using System.Collections;
using UnityEngine;

namespace MemoryPalace
{
    [Serializable]
    public class PosterMenuItem : IMenuItem
    {
        public Transform transform; // TODO: CANT SERIALIZE TRANSFORM
        public Texture2D Texture { get; protected set; }
        public Sprite Icon { get; protected set; }
        public string Label { get; protected set; }

        public PosterMenuItem(string label, Sprite icon, Texture2D texture)
        {
            this.Label = label;
            this.Icon = icon;
            this.Texture = texture;
        }

        public void ClickAction()
        {

        }
    }

}
