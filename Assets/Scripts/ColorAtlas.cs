using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class ColorAtlasPoint
{
    public string color;
    public Vector2 UV;
}

[CreateAssetMenu()]
public class ColorAtlas : ScriptableObject {

    public List<ColorAtlasPoint> colors;

    
}