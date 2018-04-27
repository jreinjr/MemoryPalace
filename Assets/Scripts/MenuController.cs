using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class MenuController : MonoBehaviour {

    // The button prefab to instantiate
    public GameObject buttonPrefab;
    // Location of the image folder
    public string filePath = "Assets/Textures/";
    // Location of Spawn Poster script
    public GameObject sceneScripts;
    SpawnPoster spawnPoster;

	// Use this for initialization
	void Start () {
        if (sceneScripts) spawnPoster = sceneScripts.GetComponent<SpawnPoster>();
        
        DirectoryInfo dir = new DirectoryInfo(filePath);
        var filters = new string[] { "jpg", "jpeg", "png" };
        List<FileInfo> imagesFound = new List<FileInfo>();

        foreach (var filter in filters)
        {
            imagesFound.AddRange(dir.GetFiles(string.Format("*.{0}", filter)));
        }
        
        foreach (FileInfo f in imagesFound)
        {

            Texture image = AssetDatabase.LoadAssetAtPath<Texture>(filePath + f.Name);
            GameObject buttonInstance = Instantiate(buttonPrefab, transform);
            buttonInstance.GetComponentInChildren<Text>().text = image.name;
            buttonInstance.GetComponent<Button>().onClick.AddListener(delegate { spawnPoster.SetPoster(image); });
        }
        
	}

}
