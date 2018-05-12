using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    [Serializable]
    public class ObjectTransformData
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
        public string parentGUID;
    }
}
