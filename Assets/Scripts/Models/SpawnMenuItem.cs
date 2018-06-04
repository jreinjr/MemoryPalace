using System;
using System.Collections;
using UnityEngine;

namespace MemoryPalace
{
    public class SpawnMenuItem
    {
        // Human-readable label
        public string Label { get; protected set; }
        // Icon to display in the menu
        public Sprite Icon { get; protected set; }
        // Optional argument to SpawnMenuItem constructor
        // Contains (at minimum) path to a prefab (as string)
        // Optionally contains class-specific info like Texture, Label, etc
        public ObjectInitData ObjectInitData { get; protected set; }

        public SpawnMenuItem(string label, Sprite icon, ObjectInitData objectInitData = null)
        {
            this.Label = label;
            this.Icon = icon;
            this.ObjectInitData = objectInitData;
        }
    }
}
