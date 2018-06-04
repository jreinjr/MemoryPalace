using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    public class MenuItem {

        public string Label { get; protected set; }

        public MenuItem(string label)
        {
            this.Label = label;
        }
    }
}
