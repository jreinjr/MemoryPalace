using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    public class MenuPage
    {
        public string label { get; protected set; }
        public List<SpawnMenuItem> items { get; protected set; }
        static Action<string, SpawnMenuItem> cbInventoryItemAdded;

        public MenuPage(string label)
        {
            this.label = label;
            items = new List<SpawnMenuItem>();
        }

        // Throws an error if passed a null item.
        // Raises InventoryItemAddedCallback when item is added.
        public bool AddItem(SpawnMenuItem item)
        {
            if (item == null)
            {
                Debug.LogError("Trying to add a null item to a menu page");
                return false;
            }
            items.Add(item);

            if (cbInventoryItemAdded != null)
            {
                cbInventoryItemAdded(this.label, item);
            }
            return true;
        }

        public static void RegisterInventoryItemAddedCallback(Action<string, SpawnMenuItem> callback)
        {
            cbInventoryItemAdded += callback;
        }
        public static void UnregisterInventoryItemAddedCallback(Action<string, SpawnMenuItem> callback)
        {
            cbInventoryItemAdded -= callback;
        }
    }

}

