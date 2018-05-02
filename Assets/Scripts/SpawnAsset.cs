using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.UnityEventHelper;
using VRTK;
using System;

public class SpawnAsset : MonoBehaviour {

    GameObject assetInstance;

    private void Start()
    {
        GetComponent<VRTK_InteractGrab>().ControllerUngrabInteractableObject += new ObjectInteractEventHandler(DoInteractUngrab);
    }

    
    public void Spawn(AssetItem asset)
    {
        assetInstance = asset.reference.Spawn();

        // Snap to grab position
        VRTK_ObjectAutoGrab autograb = GetComponent<VRTK_ObjectAutoGrab>();
        if (autograb == null)
        {
            autograb = gameObject.AddComponent<VRTK_ObjectAutoGrab>();
        }
        autograb.objectToGrab = assetInstance.GetComponent<VRTK_InteractableObject>();
        // Disable UI pointer on spawn to avoid dragging menu
        gameObject.GetComponent<VRTK_UIPointer>().enabled = false;
        Destroy(autograb, 0.1f);
    }

    private void DoInteractUngrab(object sender, ObjectInteractEventArgs e)
    {
        if (e.target == assetInstance)
        {
            // Reenable UI pointer after drop
            gameObject.GetComponent<VRTK_UIPointer>().enabled = true;
        }
    }


}
