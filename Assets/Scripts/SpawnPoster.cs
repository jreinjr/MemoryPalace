using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.UnityEventHelper;
using VRTK;

public class SpawnPoster : MonoBehaviour {

    public GameObject poster;
    VRTK_Pointer pointer;
    VRTK_ControllerEvents_UnityEvents controllerEvents;
    public Texture texture;
    GameObject posterInstance;
    Vector3 instancePos;
    Quaternion instanceRot;
    IEnumerator drag;

    private void Start()
    {
        pointer = GetComponent<VRTK_Pointer>();
        drag = Drag();

        controllerEvents = GetComponent<VRTK_ControllerEvents_UnityEvents>();
        if (controllerEvents == null)
        {
            controllerEvents = gameObject.AddComponent<VRTK_ControllerEvents_UnityEvents>();
        }
        controllerEvents.OnTriggerPressed.AddListener(StartSpawn);
        controllerEvents.OnTriggerReleased.AddListener(Release);
        
    }

    private void StartSpawn(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("StartSpawn");
        Spawn();
    }

    private void Release(object sender, ControllerInteractionEventArgs e)
    {
        Debug.Log("Release");
        posterInstance.GetComponent<Collider>().enabled = true;
        StopCoroutine(drag);

    }

    IEnumerator Drag()
    {
        while (true)
        {
            PositionAtPointer(posterInstance);
            yield return null;
        }
    }
    

    public void Spawn()
    {
        
        posterInstance = Instantiate(poster);
        PositionAtPointer(posterInstance);
        posterInstance.GetComponent<Renderer>().material.mainTexture = texture;
        posterInstance.GetComponent<Collider>().enabled = false;
        StartCoroutine(drag);

    }

    public void PositionAtPointer(GameObject posterInstance)
    {
        RaycastHit hit = pointer.pointerRenderer.GetDestinationHit();
        // Offset slightly by normal to avoid coplanar surfaces
        instancePos = hit.point + hit.normal * Random.Range(0.0001f, 0.0003f);
        instanceRot = Quaternion.LookRotation(hit.normal * -1);
        posterInstance.transform.SetPositionAndRotation(instancePos, instanceRot);
    }

    public void SetPoster(Texture test)
    {
        Debug.Log(test.name);
        texture = test;
    }
}
