using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshData))]
public class MeshDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MeshData meshData = (MeshData)target;

        if (GUILayout.Button("Get Data"))
            meshData.DrawMesh();

        base.OnInspectorGUI();

    }
}