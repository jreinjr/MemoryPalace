using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VRTK;

public class Poster : VRTK_InteractableObject  {

    public override void OnInteractableObjectUsed(InteractableObjectEventArgs e)
    {
        base.OnInteractableObjectUsed(e);
        Destroy(gameObject);
    }

}
