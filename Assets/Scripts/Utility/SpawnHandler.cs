using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    // SpawnHandler is called by Menu UI GameObjects as well as WorldController during load operation
    // SpawnHandler is responsible for creating GameObjects and registering them with WorldController
    // After creating an object, SpawnController calls ManipulateController to handle placement
    public class SpawnHandler : MonoBehaviour
    {
        // Required references
        public InventoryHandler inventoryHandler;

        static Action<GameObject> cbPrefabSpawned;
        

        // TODO: Would be nice to track which prefabs have already been loaded 
        // to avoid loading Poster / Postit prefabs more than once
        public void OnSpawnMenuButtonClicked(MenuButton menuButton, ObjectInitData objectInitdata)
        {
            GameObject prototype = CreatePrototype(objectInitdata.PrefabPath);
            // TODO: Add prototype to dictionary here

            GameObject instance = SpawnInstance(prototype, objectInitdata);

            // TODO: Call ManipulateController or otherwise handle the grab when spawning from menu
        }

        public void SpawnAllFromSaveData(List<GameObjectSaveData> savedGameObjects)
        {
            // Sets up a dictionary linking newly spawned objects with their save data
            // This is to enable setup to take place in two stages - IDs are set first, then transforms (incl. parent/child)
            Dictionary<GameObjectSaveData, GameObject> saveDataSpawnedObjectMap = new Dictionary<GameObjectSaveData, GameObject>();
            foreach (GameObjectSaveData data in savedGameObjects)
            {
                // Spawn objects:
                // 1: Load prototype
                // 2: Instantiate object
                // 3: Set Unique IDs from save file
                // 4: Callback to WorldController to log Unique IDs in dictionary
                GameObject instance = SpawnFromSaveData(data);

                saveDataSpawnedObjectMap.Add(data, instance);

            }
            foreach (GameObjectSaveData saveData in saveDataSpawnedObjectMap.Keys)
            {
                // Setup transform, including parent/child
                SetupTransform(saveDataSpawnedObjectMap[saveData], saveData.transformData);

            }
        }

        // Spawns and initializes an object given initdata and serialized transform (for saved unique ID)
        GameObject SpawnFromSaveData(GameObjectSaveData saveData)
        {
            GameObject prototype = CreatePrototype(saveData.initData.PrefabPath);
            GameObject instance = SpawnInstance(prototype, saveData.initData, saveData.transformData.myUniqueID);
            return instance;
        }

        // Now that all saved objects have been spawned, set up transforms and parent/child relationships
        void SetupTransform(GameObject go, SerializableTransform savedTransform)
        {
            // Check if we saved a UniqueID reference to a parent
            if (savedTransform.parentUniqueID != "")
            {
                go.transform.SetParent(WorldController.guidGameObjectMap[savedTransform.parentUniqueID].transform);
            }
            go.transform.SetPositionAndRotation(savedTransform.localPostition, savedTransform.localRotation);
            go.transform.localScale = savedTransform.localScale;
        }

        // Loads a prefab resource from its string path and confirms it has a SpawnableObject type for initialization
        // TODO: Would be nice to add this to a dictionary for ease of access
        // TODO: This seems unnecessarily convoluted now. Why split these functions? Is dictionary optimization necessary?
        GameObject CreatePrototype(string prefabPath)
        {
            // Load a prefab as our prototype
            GameObject prototype = (GameObject)Resources.Load(prefabPath, typeof(GameObject));
            if (prototype == null)
            {
                Debug.LogErrorFormat("Unable to find prefab with path {0}", prefabPath);
                return null;
            }
            // Get the prefab's SaveLoadableGameObject-inherited component (Poster, PostIt, Furniture, etc)
            SaveLoadableGameObject saveLoadableObject = prototype.GetComponent<SaveLoadableGameObject>();
            if (saveLoadableObject == null)
            {
                Debug.LogErrorFormat("Attempting to spawn an item ({0}) without any component derived from SaveLoadableGameObject", prefabPath);
                return null;
            }

            return prototype;
        }

        GameObject SpawnInstance(GameObject prototype, ObjectInitData initData = null, string savedUniqueID = null)
        {
            // Instantiate prototype
            GameObject instance = Instantiate(prototype);
            // Get SaveLoadable component of instance
            SaveLoadableGameObject spawnedObject = instance.GetComponent<SaveLoadableGameObject>();
            if (initData == null)
            {
                Debug.LogFormat("GameObject {0} spawning without any initialization data", prototype.name);
            }
            else
            {
                spawnedObject.Initialize(initData);
            }
            // If we are spawning an object with a saved UniqueID, set it here
            if (savedUniqueID != null)
            {
                instance.GetComponent<UniqueID>().SetUniqueIDFromSaveFile(savedUniqueID);

            }
            

            // WorldController should listen for this and add it to its dictionary
            cbPrefabSpawned(instance);

            return instance;
        }

        public void RegisterPrefabSpawnedCallback(Action<GameObject> callback)
        {
            cbPrefabSpawned += callback;
        }
        public void UnregisterPrefabSpawnedCallback(Action<GameObject> callback)
        {
            cbPrefabSpawned -= callback;
        }
    }

}