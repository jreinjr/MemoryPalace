using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UVfromColorAtlas : MonoBehaviour {

    public ColorAtlas colorAtlas;
    [Range(0, 10)]
    public int colorIndex;

    private void OnValidate()
    {
        Mesh mesh = new Mesh();
        mesh = GetComponent<MeshFilter>().sharedMesh;
        
        Vector2[] uvs = mesh.uv;
        List<Vector2> atlasUVs = new List<Vector2>();

        for (int i = 0; i < uvs.Length; i++)
        {
            atlasUVs.Add(colorAtlas.colors[colorIndex].UV);
        }

        mesh.SetUVs(0, atlasUVs);
    }
}
