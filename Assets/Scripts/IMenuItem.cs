using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    public interface IMenuItem
    {
        string Label { get; }
        Sprite Icon { get; }
    }
}