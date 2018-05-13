using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    [Serializable]
    public class ObjectInitData
    {
        [SerializeField]
        private string prefabPath;
        public string PrefabPath
        {
            get
            {
                return prefabPath;
            }
            protected set
            {
                prefabPath = value;
            }
        }

        public ObjectInitData(string prefabPath)
        {
            this.PrefabPath = prefabPath;
        }
    }
}

