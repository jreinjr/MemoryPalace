using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VRTK;

public class Poster : ManipulableObject, ISpawnableItem  {

    public Transform closeup;
    public GameObject postItPrefab;
    public Texture2D texture;
    public bool hasFocus { get; private set; }
    VRTK_ControllerEvents controllerEvents;
    Vector3 savedPos;
    Quaternion savedRot;
    List<PostIt> postIts;
    // Reference to the prefab this should sit on
    GameObject prefab;

    GameObject ISpawnableItem.prefab
    {
        get
        {
            return prefab;
        }

        set
        {
            return;
        }
    }

    // Reference to this poster's AssetItem
    public AssetItem posterAssetItem;

    public GameObject Spawn()
    {
        GameObject posterInstance = Instantiate(prefab);
        posterInstance.GetComponent<Renderer>().material.mainTexture = texture;
        return posterInstance;
    }

    private void Start()
    {
        prefab = Resources.Load("Prefabs/Poster") as GameObject;
        texture = posterAssetItem.icon.texture;

        postIts = new List<PostIt>();

        /*
        PostIt postItInstance = Instantiate(postItPrefab, transform.Find("Canvas")).GetComponent<PostIt>();
        postItInstance.posterObject = gameObject;
        postIts.Add(postItInstance);
        */
        if (closeup == null)
        {
            closeup = GameObject.Find("Closeup").transform;
        }
        hasFocus = false;
        SaveTransform();
    }

    void SaveTransform()
    {
        savedPos = transform.position;
        savedRot = transform.rotation;
    }

    public override void OnInteractableObjectUngrabbed(InteractableObjectEventArgs e)
    {
        SaveTransform();
        base.OnInteractableObjectUngrabbed(e);
    }

    public override void OnInteractableObjectUnused(InteractableObjectEventArgs e)
    {
        if (!hasFocus)
        {
            SaveTransform();
            hasFocus = true;
            transform.SetParent(closeup, true);
            StartCoroutine(SmoothMove(transform.localPosition, transform.localRotation, closeup.localPosition, closeup.localRotation, 1, true));
        }
        else
        {
            hasFocus = false;
            transform.SetParent(null, true);
            StartCoroutine(SmoothMove(transform.position, transform.rotation, savedPos, savedRot, 1, false));
        }
        base.OnInteractableObjectUnused(e);
    }

    public override void OnInteractableObjectUsed(InteractableObjectEventArgs e)
    {

        base.OnInteractableObjectUsed(e);
    }

    protected override void Update()
    {
        base.Update();
        //if (savedTransform != null) Debug.Log(savedTransform.position);
    }

    IEnumerator SmoothMove(Vector3 startPos, Quaternion startRot, Vector3 endPos, Quaternion endRot, float seconds, bool local)
    {
        
        var t = 0.0f;
        while (t <= 1.0f)
        {
            t += Time.deltaTime / seconds;

            Vector3 newPos = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.0f, t));
            Quaternion newRot = Quaternion.Lerp(startRot, endRot, Mathf.SmoothStep(0.0f, 1.0f, t));

            if (local)
            {
                transform.localPosition = newPos;
                transform.localRotation = newRot;
            }
            else
            {
                transform.position = newPos;
                transform.rotation = newRot;
            }
            
            yield return null;
        }
    }

    
}
