using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public void Start()
    {

    }



    public void GenerateThumbnail()
    {
        // Creates a new icon texture and fills it with given color
        Texture2D iconTex = new Texture2D((int)MenuButton.iconSize.width, (int)MenuButton.iconSize.height);
        var fillColorArray = iconTex.GetPixels();
        for (int i = 0; i < fillColorArray.Length; i++)
        {
            fillColorArray[i] = color;
        }
        iconTex.SetPixels(fillColorArray);
        iconTex.Apply();

        _image = Sprite.Create(iconTex, MenuButton.iconSize, Vector2.zero);
    }
}
