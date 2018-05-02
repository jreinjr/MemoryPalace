using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VRTK;

public class PostIt : VRTK_UIDraggableItem, IPointerEnterHandler, IPointerExitHandler {

    [HideInInspector]
    public GameObject posterObject;

    public void OnPointerEnter(PointerEventData eventData)
    {
        posterObject.GetComponent<VRTK_InteractableObject>().isGrabbable = false;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        posterObject.GetComponent<VRTK_InteractableObject>().isGrabbable = true;
    }
}
