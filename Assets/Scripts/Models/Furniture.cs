using System;
using UnityEngine;

namespace MemoryPalace
{
    public class Furniture : SaveLoadableGameObject
    {
        // Should be updated if Furniture prefab moves
        // TODO: Convert to Unity Asset GUID?
        public const string PrefabPath = "Prefabs/Furniture/";
        public override string _prefabPath
        {
            get
            {
                return PrefabPath;
            }
        }

        // Furniture does not need to implement Initialize because all its unique properties
        // are contained in its referenced prefab (ObjectInitData in SaveLoadableGameObject class)

        public override void Save(WorldSaveData worldSaveData)
        {
            base.Save(worldSaveData);
            GameObjectSaveData saveData = new GameObjectSaveData(initData, transformData);
            worldSaveData.savedGameObjects.Add(saveData);
        }

        protected override ObjectInitData AttemptRecreateInitData()
        {
            string guessedPrefabPath = PrefabPath + gameObject.name.Split(new char[' '])[0];
            ObjectInitData newinitData = new ObjectInitData(guessedPrefabPath);
            return newinitData;
        }
    }
}

