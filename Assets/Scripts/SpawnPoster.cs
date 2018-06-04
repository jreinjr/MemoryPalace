using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.UnityEventHelper;
using VRTK;
using System;

public class SpawnPoster : MonoBehaviour {

    public GameObject posterPrefab;
    GameObject posterInstance;

    private void Start()
    {
        GetComponent<VRTK_InteractGrab>().ControllerUngrabInteractableObject += new ObjectInteractEventHandler(DoInteractUngrab);
    }

    
    public void Spawn(Sprite img)
    {
        posterInstance = Instantiate(posterPrefab);
        posterInstance.GetComponent<Renderer>().material.mainTexture = img.texture;

        // Snap to grab position
        VRTK_ObjectAutoGrab autograb = GetComponent<VRTK_ObjectAutoGrab>();
        if (autograb == null)
        {
            autograb = gameObject.AddComponent<VRTK_ObjectAutoGrab>();
        }
        autograb.objectToGrab = posterInstance.GetComponent<VRTK_InteractableObject>();
        // Disable UI pointer on spawn to avoid dragging menu
        gameObject.GetComponent<VRTK_UIPointer>().enabled = false;
        Destroy(autograb, 0.1f);
    }

    private void DoInteractUngrab(object sender, ObjectInteractEventArgs e)
    {
        if (e.target == posterInstance)
        {
            // Reenable UI pointer after drop
            gameObject.GetComponent<VRTK_UIPointer>().enabled = true;
        }
    }


}
