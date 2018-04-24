using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class SpawnPoster : MonoBehaviour {

    public GameObject poster;
    GameObject posterInstance;
    VRTK_Pointer pointer;
    Texture texture;

    private void Start()
    {
        pointer = GetComponent<VRTK_Pointer>();
    }

    public void Spawn()
    {
        RaycastHit hit = pointer.pointerRenderer.GetDestinationHit();
        if (pointer.IsStateValid()) { }
        // Offset slightly by normal to avoid coplanar surfaces
        Vector3 instancePos = hit.point + hit.normal * 0.0001f;
        Quaternion instanceRot = Quaternion.LookRotation(hit.normal * -1);
        posterInstance = Instantiate(poster, instancePos, instanceRot);
        posterInstance.GetComponent<Renderer>().material.mainTexture = texture;
        
    }

    public void SetPoster(Texture test)
    {
        texture = test;
    }
}
