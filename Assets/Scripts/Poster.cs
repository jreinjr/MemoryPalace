using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VRTK;

public class Poster : ManipulableObject, ISpawnableItem  {

    public Texture2D texture;
    public GameObject Prefab
    {
        get
        {
            try
            {
                return (GameObject)Resources.Load("Prefabs/Poster");
            }
            catch(Exception e)
            {
                Debug.Log(e.Message);
                return null;
            }
        }
    }


    private Sprite _image;
    public Sprite Image
    {
        get
        {
            return _image;
        }
        set
        {
            _image = value;
        }
    }

    private string _name;
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }


    public GameObject Spawn()
    {
        Debug.Log("Spawning poster");
        GameObject newPoster = Instantiate(Prefab);
        newPoster.GetComponent<Renderer>().material.mainTexture = _image.texture;
        return newPoster;
    }

    public override string ToString()
    {
        return "Poster " + _name;
    }

   
    //public bool hasFocus { get; private set; }
    //public Transform closeup;
    //Vector3 savedPos;
    //Quaternion savedRot;

    //public GameObject postItPrefab;
    //List<PostIt> postIts;


    private void Start()
    {
        // Sets up focus / closeup 
        //if (closeup == null)
        //{
        //    closeup = GameObject.Find("Closeup").transform;
        //}
        //hasFocus = false;
        //SaveTransform();
    }



        //void SaveTransform()
        //{
        //    savedPos = transform.position;
        //    savedRot = transform.rotation;
        //}

        //public override void OnInteractableObjectUngrabbed(InteractableObjectEventArgs e)
        //{
        //    SaveTransform();
        //    base.OnInteractableObjectUngrabbed(e);
        //}

        //public override void OnInteractableObjectUnused(InteractableObjectEventArgs e)
        //{
        //    if (!hasFocus)
        //    {
        //        SaveTransform();
        //        hasFocus = true;
        //        transform.SetParent(closeup, true);
        //        StartCoroutine(SmoothMove(transform.localPosition, transform.localRotation, closeup.localPosition, closeup.localRotation, 1, true));
        //    }
        //    else
        //    {
        //        hasFocus = false;
        //        transform.SetParent(null, true);
        //        StartCoroutine(SmoothMove(transform.position, transform.rotation, savedPos, savedRot, 1, false));
        //    }
        //    base.OnInteractableObjectUnused(e);
        //}

        //IEnumerator SmoothMove(Vector3 startPos, Quaternion startRot, Vector3 endPos, Quaternion endRot, float seconds, bool local)
        //{

        //    var t = 0.0f;
        //    while (t <= 1.0f)
        //    {
        //        t += Time.deltaTime / seconds;

        //        Vector3 newPos = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.0f, t));
        //        Quaternion newRot = Quaternion.Lerp(startRot, endRot, Mathf.SmoothStep(0.0f, 1.0f, t));

        //        if (local)
        //        {
        //            transform.localPosition = newPos;
        //            transform.localRotation = newRot;
        //        }
        //        else
        //        {
        //            transform.position = newPos;
        //            transform.rotation = newRot;
        //        }

        //        yield return null;
        //    }
        //}
}
