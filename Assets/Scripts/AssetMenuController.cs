using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AssetPropertyType
{
    Asset,
    FolderContents
}


public class AssetMenuController : MonoBehaviour {

    public struct MenuPage
    {
        string filePath;
        string iconPath;
        string referencePath;
    }

    
    /*
    public AssetPropertyType iconPropertyType;
    public string iconFolderPath;
    public Sprite iconAsset;

    public AssetPropertyType referencePropertyType;
    public string referenceFolderPath;
    public System.Type referenceType;
    public GameObject referenceAsset;
    */
    public GameObject buttonPrefab;

    // Location of the image folder
    protected string filePath = "Textures";
    public GameObject posterPrefab;
    public AssetMenuPage test;


    public AssetMenuPage[] menuPages;
    protected AssetItem[] assets;

    protected List<AssetMenuButton> menuButtons;

    void Start()
    {
        //AssetMenuPage menuPage = ScriptableObject.CreateInstance<AssetMenuPage>();
        //menuPage.label = "Posters";
        //GetAssets(filePath, typeof(Sprite));
        BuildMenu(menuPages[0]);
    }

   
    protected void BuildMenu(AssetMenuPage menuPage)
    {
        foreach (AssetItem t in menuPage.items)
        {
            AssetMenuButton assetMenuButton = Instantiate(buttonPrefab, transform).GetComponentInChildren<AssetMenuButton>();
            assetMenuButton.assetItem = t;
            menuButtons.Add(assetMenuButton);
        }
    }

    protected void ClearMenu()
    {
        foreach(AssetMenuButton b in menuButtons)
        {
            Destroy(b.gameObject);
            menuButtons.Remove(b);
        }
    }

    /*
    protected AssetItem[] GetAssets(string filepath, System.Type assetType)
    {
        Object[] folderItems = Resources.LoadAll(filePath, assetType);
        assets = new AssetItem[folderItems.Length];
        for (int i = 0; i < folderItems.Length; i++)
        {
            assets[i] = new AssetItem(folderItems[i].name, posterPrefab, (Sprite)folderItems[i]);
        }
        return assets;
    }
    */
}
