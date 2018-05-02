/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AssetMenuPage))]
[CanEditMultipleObjects]

public class AssetMenuPageEditor : Editor {

    SerializedProperty label;
    SerializedProperty items;

    private void OnEnable()
    {
        label = serializedObject.FindProperty("label");
        items = serializedObject.FindProperty("items");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(label);
        EditorGUILayout.PropertyField(items);
        serializedObject.ApplyModifiedProperties();

    }
}
*/