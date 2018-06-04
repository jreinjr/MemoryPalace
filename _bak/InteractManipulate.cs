using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.UnityEventHelper;
using VRTK;
using System;

public class InteractManipulate : MonoBehaviour {

    VRTK_Pointer pointer;
    VRTK_ControllerEvents eventz;
    VRTK_ControllerEvents_UnityEvents controllerEvents;
    VRTK_InteractTouch_UnityEvents touchEvents;
    Vector3 instancePos;
    Quaternion instanceRot;
    IEnumerator drag;
    GameObject manipulatedObject;

    private void Start()
    {
        pointer = GetComponent<VRTK_Pointer>();

        controllerEvents = GetComponent<VRTK_ControllerEvents_UnityEvents>();
        if (controllerEvents == null)
        {
            controllerEvents = gameObject.AddComponent<VRTK_ControllerEvents_UnityEvents>();
        }
         
        touchEvents = GetComponent<VRTK_InteractTouch_UnityEvents>();
        if (touchEvents == null)
        {
            touchEvents = gameObject.AddComponent<VRTK_InteractTouch_UnityEvents>();
        }

        controllerEvents.OnTouchpadAxisChanged.AddListener(DoTouchpadAxisChanged);
    }

    private void DoTouchpadAxisChanged(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log(e);
    }

    /// <summary>
    /// The IsObjectManipulable method is used to check if a given game object is of type `ManipulableObject` and whether the object is enabled.
    /// </summary>
    /// <param name="obj">The game object to check to see if it's manipulable.</param>
    /// <returns>Is true if the given object is of type `ManipulableObject`.</returns>
    public virtual bool IsObjectManipulable(GameObject obj)
    {
        if (obj != null)
        {
            ManipulableObject io = obj.GetComponentInParent<ManipulableObject>();
            if (io != null)
            {
                return io.enabled;
            }
        }
        return false;
    }

}
