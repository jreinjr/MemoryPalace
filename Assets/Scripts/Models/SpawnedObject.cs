using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    public abstract class SpawnedObject : MonoBehaviour
    {
        public abstract void Initialize(ObjectInitData objectInitData);
    }
}
