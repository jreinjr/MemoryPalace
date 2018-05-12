using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueID : MonoBehaviour {

	public string ID { get; protected set; } // Globally unique id

    private void Awake()
    {
        ID = Guid.NewGuid().ToString();
    }
}
