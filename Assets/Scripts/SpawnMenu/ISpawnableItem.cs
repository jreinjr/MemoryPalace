using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnableItem
{
    string Name { get; set; }
    Sprite Image { get; set; }
    GameObject Prefab { get; }

    GameObject Spawn();

}

public class ItemSpawnedEventArgs : EventArgs
{
    public ItemSpawnedEventArgs(ISpawnableItem item)
    {
        Item = item;
    }

    public ISpawnableItem Item;
}