using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class IconMenuController : MonoBehaviour {

    // The button prefab to instantiate
    public GameObject buttonPrefab;
    // Location of the image folder
    protected string filePath = "Textures";
    protected Object[] menuItems;

    // Use this for initialization
    void Start () {
        BuildMenu();
	}
	
	protected void BuildMenu()
    {
        menuItems = Resources.LoadAll(filePath, typeof(Sprite));
        foreach (var t in menuItems)
        {
            GameObject buttonInstance = Instantiate(buttonPrefab, transform);
            IconButton iconButton = buttonInstance.GetComponentInChildren<IconButton>();
            iconButton.iconItem.label = t.name;
            iconButton.iconItem.icon = t as Sprite;
        }
    }
}
