using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class AssetMenuPage : ScriptableObject  {
    public string label;
    public AssetItem[] items;
}
