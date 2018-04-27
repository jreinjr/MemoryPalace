using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImageMenuController : MonoBehaviour {

    // The button prefab to instantiate
    public GameObject buttonPrefab;
    // Location of the image folder
    public string filePath = "Assets/Textures/";

    // Use this for initialization
    void Start () {
        BuildMenu();
	}
	
	protected void BuildMenu()
    {

    }
}
