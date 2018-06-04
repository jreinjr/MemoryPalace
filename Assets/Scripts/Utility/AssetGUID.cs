using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetGUID : MonoBehaviour {
    [MenuItem("AssetDatabase/AssetPathToGUID")]
    static void Doit()
    {
        string t = AssetDatabase.AssetPathToGUID("Assets/Resources/latvia-latin.png");

        UnityEngine.Object o = new Object();
        o.GetInstanceID();

        Debug.Log(t);
    }
}
