using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    public class Parent : SaveLoadableGameObject
    {
        // Should be updated if Parent prefab moves
        // TODO: Convert to Unity Asset GUID?
        public const string PrefabPath = "Prefabs/Parent";
        public override string _prefabPath
        {
            get
            {
                return PrefabPath;
            }
        }

        public override void Save(WorldSaveData worldSaveData)
        {
            base.Save(worldSaveData);
            GameObjectSaveData saveData = new GameObjectSaveData(initData, transformData);
            worldSaveData.savedGameObjects.Add(saveData);
        }

        protected override ObjectInitData AttemptRecreateInitData()
        {
            return new ObjectInitData(PrefabPath);
        }
    }

}
