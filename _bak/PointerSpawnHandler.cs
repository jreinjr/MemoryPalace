
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.UnityEventHelper;
using VRTK;
using System;

public class PointerSpawnHandler : MonoBehaviour {

    GameObject itemInstance;

    private void Start()
    {
        GetComponent<VRTK_InteractGrab>().ControllerUngrabInteractableObject += new ObjectInteractEventHandler(DoInteractUngrab);
    }

    
    public void SpawnItem(ISpawnableItem item)
    {
        itemInstance = item.Spawn();
        itemInstance.transform.SetPositionAndRotation(transform.position, transform.rotation);
        
        // Snap item to grab position
        VRTK_ObjectAutoGrab autograb = GetComponent<VRTK_ObjectAutoGrab>();
        if (autograb == null)
        {
            autograb = gameObject.AddComponent<VRTK_ObjectAutoGrab>();
        }
        autograb.objectToGrab = itemInstance.GetComponent<VRTK_InteractableObject>();
        // Disable UI pointer on spawn to avoid dragging menu
        gameObject.GetComponent<VRTK_UIPointer>().enabled = false;
        Destroy(autograb, 0.1f);
    }

    private void DoInteractUngrab(object sender, ObjectInteractEventArgs e)
    {
        if (e.target == itemInstance)
        {
            // Reenable UI pointer after drop
            gameObject.GetComponent<VRTK_UIPointer>().enabled = true;
        }
    }


}
