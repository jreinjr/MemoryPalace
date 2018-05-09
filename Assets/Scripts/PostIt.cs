using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VRTK;

public class PostIt : ManipulableObject, ISpawnableItem {

    public Color color;
    public GameObject attachedToObject;

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

    public GameObject Prefab
    {
        get
        {
            try
            {
                return (GameObject)Resources.Load("Prefabs/PostIt");
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return null;
            }
        }
    }


    public void GenerateThumbnail()
    {
        // Creates a new icon texture and fills it with given color
        Texture2D iconTex = new Texture2D((int)SpawnButton.iconSize.width, (int)SpawnButton.iconSize.height);
        var fillColorArray = iconTex.GetPixels();
        for (int i = 0; i < fillColorArray.Length; i++)
        {
            fillColorArray[i] = color;
        }
        iconTex.SetPixels(fillColorArray);
        iconTex.Apply();

        _image = Sprite.Create(iconTex, SpawnButton.iconSize, Vector2.zero);
    }

    public override string ToString()
    {
        return "Postit " + color;
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    attachedToObject.GetComponent<VRTK_InteractableObject>().isGrabbable = false;

    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    attachedToObject.GetComponent<VRTK_InteractableObject>().isGrabbable = true;
    //}

    public GameObject Spawn()
    {
        GameObject newPoster = Instantiate(Prefab);
        newPoster.GetComponent<Renderer>().material.color = color;
        return newPoster;
    }

    private void Start()
    {
        InteractableObjectUngrabbed += PostIt_InteractableObjectUngrabbed;
        InteractableObjectGrabbed += PostIt_InteractableObjectGrabbed;
        
    }

    private void PostIt_InteractableObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        
        if (transform.parent.GetComponent<Poster>() != null)
        {
            transform.parent.tag = "Sticker";
        }
    }

    private void PostIt_InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        VRTK_Pointer pointer = e.interactingObject.GetComponent<VRTK_Pointer>();
        //this.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        //pointer.pointerRenderer.customRaycast.layersToIgnore -= LayerMask.NameToLayer("Manipulable");
        RaycastHit hit = pointer.pointerRenderer.GetDestinationHit();
        //this.gameObject.layer = LayerMask.NameToLayer("Manipulable");
        //pointer.pointerRenderer.customRaycast.layersToIgnore += LayerMask.NameToLayer("Manipulable");
        Debug.Log(hit.collider);
        if (hit.collider.gameObject != null)
        {
            transform.parent = hit.collider.transform;
        }
        if (transform.parent.gameObject.GetComponent<Poster>() != null)
        {
            transform.parent.tag = "Poster";
        }
        
    }
}
