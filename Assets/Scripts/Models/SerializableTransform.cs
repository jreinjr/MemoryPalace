using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MemoryPalace
{
    [Serializable]
    public class SerializableTransform
    {
        public Vector3 localPostition;
        public Quaternion localRotation;
        public Vector3 localScale;
        public string myUniqueID;
        public string parentUniqueID;

        public SerializableTransform(Transform currentTransform)
        {
            // No parent
            if (currentTransform.parent == null)
            {
                parentUniqueID = "";
            }
            // Parent, but no UniqueID component
            else if (currentTransform.parent.GetComponent<UniqueID>() == null)
            {
                Debug.LogErrorFormat("Attempting to create ObjectTransformData for {0} but parent of object has no UniqueID component", currentTransform.gameObject.name);
                return;
            }
            // Parent has a UniqueID component
            else
            {
                this.parentUniqueID = currentTransform.parent.GetComponent<UniqueID>().ID;
            }
            // Check if transform itself has UniqueID component
            if (currentTransform.GetComponent<UniqueID>() == null)
            {
                Debug.LogErrorFormat("Attempting to save UniqueID for object {0} but no UniqueID component found", currentTransform.gameObject.name);
                return;
            }
            // Parent and self have UniqueID components
            // TODO: Is this too much error checking? Would anything except a SaveLoadableGameObject use this class?
            this.myUniqueID = currentTransform.GetComponent<UniqueID>().ID;
            this.localPostition = currentTransform.localPosition;
            this.localRotation = currentTransform.localRotation;
            this.localScale = currentTransform.localScale;            
        }
    }
}
