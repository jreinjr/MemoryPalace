using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    public class Poster : SaveLoadableGameObject
    {
        // Should be updated if Poster prefab moves
        // TODO: Convert to Unity Asset GUID?
        public const string PrefabPath = "Prefabs/Poster";
        public override string _prefabPath
        {
            get
            {
                return PrefabPath;
            }
        }
        
       

        // Contains texture, label, and prefab location
        PosterInitData posterInitData;

        // This poster's texture
        Texture posterTexture;

        void Start()
        {
            posterTexture = GetComponent<Renderer>().material.mainTexture;
        }

        // TODO: Is it safe to keep this public? Maybe should be called by Start / OnEnable and protected?
        public override void Initialize(ObjectInitData initData)
        {
            // Confirm that ObjectInitData is valid PosterData
            if (initData == null)
            {
                Debug.LogError("Attempting to initialize Poster with null objectInitData");
                return;
            }
            else if (initData.GetType() != typeof(PosterInitData))
            {
                Debug.LogErrorFormat("Attempting to initialize a Poster with ObjectInitData of type {0}. Valid PosterInitData required.");
                return;
            }
            // Cast initData to PosterInitData
            posterInitData = (PosterInitData)initData;

            // Set label to PosterData.Label

            // Set texture to PosterData.Texture
            posterTexture = posterInitData.Texture;

            base.Initialize(initData);
        }

        public override void Save(WorldSaveData worldSaveData)
        {
            base.Save(worldSaveData);
            GameObjectSaveData saveData = new GameObjectSaveData(posterInitData, transformData);
            worldSaveData.savedGameObjects.Add(saveData);
        }

        protected override ObjectInitData AttemptRecreateInitData()
        {
            return new PosterInitData(PrefabPath, posterTexture.name, posterTexture);
        }
    }
}
