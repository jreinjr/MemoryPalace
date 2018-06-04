using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SpawnPointerRenderer : VRTK_StraightPointerRenderer {

    // Identical to VRTK_StraightPointerRenderer except we return actual ray length instead of truncated ray
    protected override float CastRayForward()
    {
        Transform origin = GetOrigin();
        Ray pointerRaycast = new Ray(origin.position, origin.forward);
        RaycastHit pointerCollidedWith;
        customRaycast = new VRTK_CustomRaycast();
        customRaycast.layersToIgnore = LayerMask.NameToLayer("Ignore Raycast");
#pragma warning disable 0618
        bool rayHit = VRTK_CustomRaycast.Raycast(customRaycast, pointerRaycast, out pointerCollidedWith, layersToIgnore, maximumLength);
#pragma warning restore 0618

        CheckRayMiss(rayHit, pointerCollidedWith);
        CheckRayHit(rayHit, pointerCollidedWith);

        float actualLength = maximumLength;
        if (rayHit && pointerCollidedWith.distance < maximumLength)
        {
            actualLength = pointerCollidedWith.distance;
        }

        return actualLength;
    }
}
