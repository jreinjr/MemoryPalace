using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SpawnPoster : MonoBehaviour {

    public GameObject poster;
    GameObject posterInstance;
    VRTK_Pointer pointer;

    private void Start()
    {
        pointer = GetComponent<VRTK_Pointer>();
    }

    public void Spawn()
    {
        RaycastHit hit = pointer.pointerRenderer.GetDestinationHit();
        // Offset slightly by normal to avoid coplanar surfaces
        Vector3 instancePos = hit.point + hit.normal * 0.0001f;
        Quaternion instanceRot = Quaternion.LookRotation(hit.normal * -1);
        Instantiate(poster, instancePos, instanceRot);
    }
}
