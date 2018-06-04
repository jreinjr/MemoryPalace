using System;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    [Serializable]
    public class WorldSaveData
    {
        public List<GameObjectSaveData> savedGameObjects;

        public WorldSaveData()
        {
            savedGameObjects = new List<GameObjectSaveData>();
        }
    }
}