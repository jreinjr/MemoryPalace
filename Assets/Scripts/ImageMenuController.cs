using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class ImageMenuController : MonoBehaviour {

    // The button prefab to instantiate
    public GameObject buttonPrefab;
    // Location of the image folder
    private string filePath = "Textures";
    private Object[] sprites;

    // Use this for initialization
    void Start () {
        BuildMenu();
	}
	
	protected void BuildMenu()
    {
        sprites = Resources.LoadAll(filePath, typeof(Sprite));
        foreach (var t in sprites)
        {
            GameObject buttonInstance = Instantiate(buttonPrefab, transform);
            ImageButton imageButton = buttonInstance.GetComponent<ImageButton>();
            imageButton.label = t.name;
            imageButton.img = t as Sprite;
        }
        
    }
    
}
