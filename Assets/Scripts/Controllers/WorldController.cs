using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

namespace MemoryPalace
{
    // WorldController is responsible for saving / loading, and keeping track 
    // of spawned objects in the world - for example furniture, posters, postits. 
    public class WorldController : MonoBehaviour
    {
        // Required references
        public SpawnHandler spawnHandler;

        public static WorldController Instance;

        public WorldSaveData worldSaveData { get; protected set; }

        public List<GameObject> spawnedGameObjects { get; protected set; }
        public static Dictionary<string, GameObject> guidGameObjectMap { get; protected set; }

        static Action<WorldSaveData> cbSave;

        private void Start()
        {
            ///////////////
            /// SINGLETON
            ///////////////
            if (Instance != null && this != Instance)
            {
                // There can be only one
                Debug.LogError("Destroying WorldController - there can be only one active WorldController in the scene");
                Destroy(this);
            }
            Instance = this;

            //////////////////////////////
            /// INITIALIZE DICTIONARIES
            //////////////////////////////
            guidGameObjectMap = new Dictionary<string, GameObject>();
            spawnedGameObjects = new List<GameObject>();

            //////////////////////////////
            /// REGISTER CALLBACKS
            //////////////////////////////
            spawnHandler.RegisterPrefabSpawnedCallback(OnPrefabSpawned);

            // Search the scene for existing GameObjects with UniqueID components.
            // Add them to the dictionary if they're not already there.
            // Eventually we may want to replace this with a scene load operation.
            RegisterExistingUniqueGameObjects();
        }

        // Only registers active GameObjects
        void RegisterExistingUniqueGameObjects()
        {
            SaveLoadableGameObject[] saveLoadableGameObjects = FindObjectsOfType<SaveLoadableGameObject>();
            for (int i = 0; i < saveLoadableGameObjects.Length; i++)
            {
                RegisterGUID(saveLoadableGameObjects[i].gameObject);
            }
        }

        // Check if the given GameObject has a UniqueID component and, if so, add it to the dictionary
        public void RegisterGUID(GameObject go)
        {
            string id = go.GetComponent<UniqueID>().ID;
            // Double check that the GameObject has a UniqueID component
            if (id == null)
            {
                Debug.LogError("Gameobject " + go.name + "lacks a UniqueID component but someone is trying to register it");
                return;
            }
            // Check if the ID exists in our dictionary already
            else if (guidGameObjectMap.ContainsKey(id))
            {
                // ID already exists in dictionary and references this GameObject
                if (guidGameObjectMap[id] == go)
                {
                    Debug.Log("Trying to register GameObject " + go.name + " in WorldController UniqueID dictionary but it already exists there");
                    return;
                }
                // Somehow the ID exists with another GameObject - duplicate ID? GameObject changed? No idea how.. but worth knowing
                else
                {
                    Debug.LogErrorFormat("Trying to register GameObject {0} with GUID {1} but the ID already exists and references ANOTHER GAMEOBJECT!!!", go, id);
                }
            }
            // Otherwise, register this GameObject
            guidGameObjectMap.Add(id, go);
        }

        public void ClearWorld()
        {
            List<string> idKeys = new List<string>();
            // Wipe changes and restart to default world layout
            foreach (string id in guidGameObjectMap.Keys)
            {
                idKeys.Add(id);
            }
            foreach (string id in idKeys)
            {
                Destroy(guidGameObjectMap[id]);
                guidGameObjectMap.Remove(id);
            }
        }

        public void Save()
        {
            ///////////////////////
            // Save current changes
            ///////////////////////
            Debug.Log("Saving");
            worldSaveData = new WorldSaveData();

            // SaveLoadableObjects will register themselves with WorldSaveData
            cbSave(worldSaveData);

            string worldSaveDataJson = JsonUtility.ToJson(worldSaveData);
            PlayerPrefs.SetString("SaveData", worldSaveDataJson);
        }

        public void Load()
        {
            ///////////////////////
            // Clear existing world
            ///////////////////////
            ClearWorld();

            ///////////////////////
            // Load saved world
            ///////////////////////
            Debug.Log("Loading");
            WorldSaveData loadData = (WorldSaveData)JsonUtility.FromJson(PlayerPrefs.GetString("SaveData"), typeof(WorldSaveData));
            // SpawnHandler takes care of instantiation, initialization and placement of all saved objects
            spawnHandler.SpawnAllFromSaveData(loadData.savedGameObjects);
        }

        void OnPrefabSpawned(GameObject go)
        {
            // Add to list of spawned objects
            spawnedGameObjects.Add(go);

            string uniqueID = go.GetComponent<UniqueID>().ID;
            if (string.IsNullOrEmpty(uniqueID))
            {
                Debug.LogFormat("Gameobject {0} spawned with no UniqueID component. Sure about this? No way to save parenting to this object");
                return;
            }

            guidGameObjectMap.Add(uniqueID, go);
        }

        public static void RegisterSaveCallback(Action<WorldSaveData> callback)
        {
            cbSave += callback;
        }
        // Disabled GameObjects will not save
        public static void UnregisterSaveCallback(Action<WorldSaveData> callback)
        {
            cbSave -= callback;
        }

    }

}
