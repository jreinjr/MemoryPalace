using System;
using UnityEngine;

namespace MemoryPalace
{
    [RequireComponent(typeof(UniqueID))]
    public class Furniture : MonoBehaviour
    {
        [HideInInspector]
        public FurnitureData furnitureData { get; protected set; }


    }
}

