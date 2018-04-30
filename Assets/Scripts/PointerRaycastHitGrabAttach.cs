using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.GrabAttachMechanics;
using VRTK;

public class PointerRaycastHitGrabAttach : VRTK_BaseGrabAttach
{
    VRTK_Pointer pointer;
    float offset;

    protected override void Initialise()
    {
        tracked = false;
        climbable = false;
        kinematic = false;
    }

    public override bool StartGrab(GameObject grabbingObject, GameObject givenGrabbedObject, Rigidbody givenControllerAttachPoint)
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        pointer = grabbingObject.GetComponent<VRTK_Pointer>();
        //GetComponent<Collider>().enabled = false;
        offset = 0f;
        return base.StartGrab(grabbingObject, givenGrabbedObject, givenControllerAttachPoint);
    }

    public override void StopGrab(bool applyGrabbingObjectVelocity)
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        //GetComponent<Collider>().enabled = true;

        base.StopGrab(applyGrabbingObjectVelocity);
    }

    public override void ProcessUpdate()
    {
        RaycastHit hit = pointer.pointerRenderer.GetDestinationHit();
        float realOffset = offset + Random.Range(0.0001f, 0.0003f);
        Vector3 pos = hit.point + hit.normal * realOffset;
        Quaternion rot = Quaternion.LookRotation(hit.normal * -1);
        transform.SetPositionAndRotation(pos, rot);
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