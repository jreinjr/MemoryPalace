using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    // SpawnController is called by Menu UI GameObjects
    // SpawnController is responsible for creating GameObjects and registering them with WorldController
    // After creating an object, SpawnController calls ManipulateController to handle placement
    public class SpawnHandler : MonoBehaviour
    {
        // Required references
        public InventoryHandler inventoryHandler;

        static Action<GameObject> cbPrefabSpawned;
        

        // TODO: Would be nice to track which prefabs have already been loaded 
        // to avoid loading Poster / Postit prefabs more than once
        public void OnSpawnMenuButtonClicked(ObjectInitData objectInitdata)
        {
            GameObject prototype = CreatePrototype(objectInitdata.PrefabPath);
            // TODO: Add prototype to dictionary here

            GameObject instance = SpawnInstance(prototype, objectInitdata);

            // TODO: Call ManipulateController or otherwise handle the grab when spawning from menu
        }

        // Loads a prefab resource from its string path and confirms it has a SpawnableObject type for initialization
        // TODO: Would be nice to add this to a dictionary for ease of access
        GameObject CreatePrototype(string prefabPath)
        {
            // Load a prefab as our prototype
            GameObject prototype = (GameObject)Resources.Load(prefabPath, typeof(GameObject));
            if (prototype == null)
            {
                Debug.LogErrorFormat("Unable to find prefab with path {0}", prefabPath);
                return null;
            }
            // Get the prefab's SpawnedObject-inherited component (Poster, PostIt, Furniture, etc)
            SpawnedObject spawnedObject = prototype.GetComponent<SpawnedObject>();
            if (spawnedObject == null)
            {
                Debug.LogErrorFormat("Attempting to spawn an item ({0}) without any component derived from SpawnedObject", prefabPath);
                return null;
            }

            return prototype;
        }

        GameObject SpawnInstance(GameObject prototype, ObjectInitData initData = null)
        {
            // Instantiate prototype
            GameObject instance = Instantiate(prototype);
            SpawnedObject spawnedObject = prototype.GetComponent<SpawnedObject>();

            if (initData == null)
            {
                Debug.LogFormat("GameObject {0} spawning without any initialization data", prototype.name);
            }
            else
            {
                spawnedObject.Initialize(initData);
            }
            
            // WorldController should listen for this and add it to its dictionary
            cbPrefabSpawned(instance);

            return instance;
        }

        public static void RegisterPrefabSpawnedCallback(Action<GameObject> callback)
        {
            cbPrefabSpawned += callback;
        }
        public static void UnregisterPrefabSpawnedCallback(Action<GameObject> callback)
        {
            cbPrefabSpawned -= callback;
        }
    }

}