using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CubeData))]
public class CubeDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        CubeData cubeData = (CubeData)target;

        if (GUILayout.Button("Create"))
            cubeData.Create();

        base.OnInspectorGUI();
    }
}