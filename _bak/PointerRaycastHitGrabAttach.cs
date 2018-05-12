using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.GrabAttachMechanics;
using VRTK;

public class PointerRaycastHitGrabAttach : VRTK_BaseGrabAttach
{
    VRTK_Pointer pointer;
    VRTK_CustomRaycast customRaycast;
    float offset;

    protected override void Initialise()
    {
        tracked = false;
        climbable = false;
        kinematic = false;
    }

    public override bool StartGrab(GameObject grabbingObject, GameObject givenGrabbedObject, Rigidbody givenControllerAttachPoint)
    {
        pointer = grabbingObject.GetComponent<VRTK_Pointer>();
        //transform.ChangeLayersRecursively("Ignore Raycast");
        //pointer.pointerRenderer.layersToIgnore -= LayerMask.NameToLayer("StickTo");
        //pointer.pointerRenderer.customRaycast.layersToIgnore += LayerMask.NameToLayer("Default");
        
        pointer = grabbingObject.GetComponent<VRTK_Pointer>();
        //GetComponent<Collider>().enabled = false;
        offset = 0f;
        return base.StartGrab(grabbingObject, givenGrabbedObject, givenControllerAttachPoint);
    }


    public override void StopGrab(bool applyGrabbingObjectVelocity)
    {
        //transform.ChangeLayersRecursively("Default");
        //pointer.pointerRenderer.customRaycast.layersToIgnore -= LayerMask.NameToLayer("Default");
        //pointer.pointerRenderer.layersToIgnore += LayerMask.NameToLayer("StickTo");
        //GetComponent<Collider>().enabled = true;

        base.StopGrab(applyGrabbingObjectVelocity);
    }

    public override void ProcessUpdate()
    {
        RaycastHit hit = pointer.pointerRenderer.GetDestinationHit();
        float realOffset = offset + Random.Range(0.0001f, 0.0003f);
        Vector3 pos = hit.point + hit.normal * realOffset;
        Quaternion rot = Quaternion.LookRotation(hit.normal * -1);
        gameObject.transform.SetPositionAndRotation(pos, rot);
    }

    private void OnCollisionStay(Collision collider)
    {
        offset += 0.01f;
        Debug.Log("intersecting");
    }
    private void OnCollisionExit(Collision collider)
    {
        offset = 0f;
    }
}