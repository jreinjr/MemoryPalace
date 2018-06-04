using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    [Serializable]
    public class GameObjectSaveData
    {
        public ObjectInitData initData;
        public SerializableTransform transformData;

        public GameObjectSaveData(ObjectInitData initData, SerializableTransform transformData)
        {
            this.initData = initData;
            this.transformData = transformData;
        }
    }

}
