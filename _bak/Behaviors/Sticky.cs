using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticky : MonoBehaviour {



    private void Start()
    {
        InteractableObjectUngrabbed += PostIt_InteractableObjectUngrabbed;
        InteractableObjectGrabbed += PostIt_InteractableObjectGrabbed;

    }

    private void PostIt_InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {

        if (transform.parent.GetComponent<Poster>() != null)
        {
            transform.parent.tag = "Sticker";
        }
    }

    private void PostIt_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        VRTK_Pointer pointer = e.interactingObject.GetComponent<VRTK_Pointer>();
        //this.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        //pointer.pointerRenderer.customRaycast.layersToIgnore -= LayerMask.NameToLayer("Manipulable");
        RaycastHit hit = pointer.pointerRenderer.GetDestinationHit();
        //this.gameObject.layer = LayerMask.NameToLayer("Manipulable");
        //pointer.pointerRenderer.customRaycast.layersToIgnore += LayerMask.NameToLayer("Manipulable");
        Debug.Log(hit.collider);
        if (hit.collider.gameObject != null)
        {
            transform.parent = hit.collider.transform;
        }
        if (transform.parent.gameObject.GetComponent<Poster>() != null)
        {
            transform.parent.tag = "Poster";
        }

    }
}
