using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MarchingCubes))]
public class MarchingCubesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MarchingCubes marchingCubes = (MarchingCubes)target;

        if (GUILayout.Button("March"))
            marchingCubes.March();

        base.OnInspectorGUI();
    }
}