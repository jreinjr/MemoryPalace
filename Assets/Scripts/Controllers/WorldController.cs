using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MemoryPalace
{
    // WorldController is responsible for saving / loading, and keeping track 
    // of spawned objects in the world - for example furniture, posters, postits. 
    public class WorldController : MonoBehaviour
    {

        public static WorldController Instance;

        public World world;
        public List<GameObject> spawnedGameObjects { get; protected set; }
        public static Dictionary<string, GameObject> guidGameObjectMap { get; protected set; }


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


            //////////////////////////////
            /// REGISTER CALLBACKS
            //////////////////////////////
            SpawnHandler.RegisterPrefabSpawnedCallback(OnPrefabSpawned);

            // Search the scene for existing GameObjects with UniqueID components.
            // Add them to the dictionary if they're not already there.
            // Eventually we may want to replace this with a scene load operation.
            RegisterExistingUniqueGameObjects();

        }

        // Only registers active GameObjects
        void RegisterExistingUniqueGameObjects()
        {
            UniqueID[] uniqueGameObjects = FindObjectsOfType<UniqueID>();
            for (int i = 0; i < uniqueGameObjects.Length; i++)
            {
                RegisterGUID(uniqueGameObjects[i].gameObject);
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

        public void Restart()
        {
            // Wipe changes and restart to default world layout
        }

        public void Save()
        {
            ///////////////////////
            // Save current changes
            ///////////////////////
            // Parse through loaded gameobjects
            // Save ObjectTransformData for each
            // Save ObjectInitData where applicable
            // JSON
            foreach (GameObject go in guidGameObjectMap.Values)
            {
                Debug.Log("Name: " + go.name + "   GUID: " + go.GetComponent<UniqueID>().ID);
            }
            
        }

        public void Load()
        {
            ///////////////////////
            // Load saved world
            ///////////////////////
            // Parse JSON file for ObjectInitData and ObjectTransformData
            // Call SpawnHandler to spawn objects with ObjectInitData
            // Place 
            // Callback when load complete?
        }

        void OnPrefabSpawned(GameObject go)
        {
            Debug.LogFormat("Registering gameobject {0} in WorldController posterGameObjectMap", go.name);

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

    }

}
