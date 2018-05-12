using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    [Serializable]
    public class Poster : SpawnedObject
    {
        // TODO: Is it safe to keep this public? Maybe should be called by Start / OnEnable and protected?
        public override void Initialize(ObjectInitData objectInitData)
        {
            // Confirm that ObjectInitData is valid PosterData
            // Set label to PosterData.Label
            // Set texture to PosterData.Texture
        }
    }
}
