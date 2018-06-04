using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    // Require a UniqueID component to preserve parenting relationships on save / load
    [RequireComponent(typeof(UniqueID))]
    public abstract class SaveLoadableGameObject : MonoBehaviour
    {
        // Should be updated if prefab moves
        // This is just a reminder to implement const string in derived classes
        // TODO: Convert to Unity Asset GUID?
        public abstract string _prefabPath { get; } 

        public ObjectInitData initData { get; protected set; }
        protected SerializableTransform transformData;

        // All SaveLoadableGameObjects must be initialized from prefabs.
        // ObjectInitData contains everything this prefab need to build a specific instance. 
        // ObjectInitData contains a prefab path as a string at the minimum.
        // Derived classes may use more specific InitData 
        // For example, PosterInitData (with texture and label information)
        // TODO: Is it safe to keep this public?
        public virtual void Initialize(ObjectInitData initData)
        {

            this.initData = initData;

        }

        public virtual void Save(WorldSaveData worldSaveData)
        {
            // This would occur if we place objects in the Editor manually.
            if (initData == null)
            {

                initData = AttemptRecreateInitData();

            }

            // Record current transform in serializable format
            transformData = new SerializableTransform(this.transform);
        }

        public virtual void OnEnable()
        {
            WorldController.RegisterSaveCallback(Save);
        }

        public virtual void OnDisable()
        {
            WorldController.UnregisterSaveCallback(Save);
        }

        protected abstract ObjectInitData AttemptRecreateInitData();
        
    }
}
