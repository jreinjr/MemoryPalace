using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class ManipulableObject : VRTK_InteractableObject {

    protected VRTK_ControllerEvents controllerEvents;

    public override void OnInteractableObjectGrabbed(InteractableObjectEventArgs e)
    {
        controllerEvents = e.interactingObject.GetComponent<VRTK_ControllerEvents>();
        if (controllerEvents == null)
        {
            Debug.Log("Required component missing: VRTK Controller Events on " + e.interactingObject.name);
        }
        else
        {
            controllerEvents.TouchpadPressed += new ControllerInteractionEventHandler(DoTouchpadPressed);
        }

        base.OnInteractableObjectUngrabbed(e);
    }


    public override void OnInteractableObjectUngrabbed(InteractableObjectEventArgs e)
    {
        if (controllerEvents != null)
        {
            Debug.Log("oops");
            controllerEvents.TouchpadPressed -= DoTouchpadPressed;
            controllerEvents = null;
        }
        base.OnInteractableObjectUngrabbed(e);
    }

    protected virtual void DoTouchpadPressed(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("Touchpad pressed" + e.touchpadAxis);
    }
}
