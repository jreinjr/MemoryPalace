using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class AssetItem  {
    public string label;
    public Sprite icon;
    public ISpawnableItem reference;


    public AssetItem(string label, ISpawnableItem reference)
    {
        this.label = label;
        this.reference = reference;

        //Texture2D thumb = AssetPreview.GetMiniThumbnail(reference);
        //this.icon = Sprite.Create(thumb, new Rect(0, 0, thumb.width, thumb.height), Vector2.zero);
    }

    public AssetItem (string label, ISpawnableItem reference, Sprite icon)
    {
        this.label = label;
        this.icon = icon;
        this.reference = reference;
    }
}
