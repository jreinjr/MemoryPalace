using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MemoryPalace
{
    [Serializable]
    public class PosterInitData : ObjectInitData
    {
        // TODO: Using Editor code to get texturePath from Texture
        // NO BUENO!
        [SerializeField]
        private string texturePath;
        public Texture Texture { get; protected set; }

        [SerializeField]
        private string label;
        public string Label {
            get
            {
                return label;
            }
            protected set
            {
                label = value;
            }
        }

        public PosterInitData(string prefabPath, string label, Texture texture) : base(prefabPath)
        {
            this.PrefabPath = prefabPath;
            this.Label = label;
            this.Texture = texture;

            // Get texture path relative to Resources folder
            this.texturePath = AssetDatabase.GetAssetPath(texture).Split(new string[1] { "Resources/"}, StringSplitOptions.None)[1];
        }
    }
}
