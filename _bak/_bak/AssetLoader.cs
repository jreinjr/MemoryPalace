using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(SpawnableInventory))]
public abstract class SpawnableLoader : MonoBehaviour {

    protected SpawnableInventory inventory;
    public abstract string label { get; }

    public abstract List<ISpawnableItem> Load();

}
