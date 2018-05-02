using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AssetMenuController))]
[CanEditMultipleObjects]
public class AssetMenuControllerEditor : Editor {

    SerializedProperty test;
    
    private void OnEnable()
    {

        test = serializedObject.FindProperty("test");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(test);
        serializedObject.ApplyModifiedProperties();

    }
}

