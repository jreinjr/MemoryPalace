using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    [Serializable]
    public class PosterData : ObjectInitData
    {
        public Texture2D Texture { get; protected set; }
        public string Label { get; protected set; }

        public PosterData(string prefabPath, string label, Texture2D texture)
        {
            this.PrefabPath = prefabPath;
            this.Label = label;
            this.Texture = texture;
        }
    }
}
