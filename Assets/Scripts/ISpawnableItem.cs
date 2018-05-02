using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnableItem
{
    GameObject prefab { get; set; }

    GameObject Spawn();
}