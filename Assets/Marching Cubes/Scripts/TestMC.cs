using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMC : MonoBehaviour
{
    public GameObject target;
    public int[] triTableIndex;
    public Transform[] cubeCorners;
    public Transform[] cubeEdges;
    public Material defaultMaterial;
    MarchTables marchTables = new();

    private void Start()
    {
        March();
    }

    public void March()
    {
        for (int j = 0; j < triTableIndex.Length; j++)
        {
            // Create triangles for current cube configuration
            for (int i = 0; marchTables.triangulation[triTableIndex[j], i] != -1; i += 3)
            {
                // Get indices of corner points A and B for each of the three edges
                // of the cube that need to be joined to form the triangle.
                int a0 = marchTables.cornerIndexAFromEdge[marchTables.triangulation[triTableIndex[j], i]];
                int b0 = marchTables.cornerIndexBFromEdge[marchTables.triangulation[triTableIndex[j], i]];

                int a1 = marchTables.cornerIndexAFromEdge[marchTables.triangulation[triTableIndex[j], i + 1]];
                int b1 = marchTables.cornerIndexBFromEdge[marchTables.triangulation[triTableIndex[j], i + 1]];

                int a2 = marchTables.cornerIndexAFromEdge[marchTables.triangulation[triTableIndex[j], i + 2]];
                int b2 = marchTables.cornerIndexBFromEdge[marchTables.triangulation[triTableIndex[j], i + 2]];

                // Draw mesh
                Vector3[] verts = new Vector3[6]
                {
                    cubeEdges[a0].transform.localPosition,
                    cubeEdges[b0].transform.localPosition,
                    cubeEdges[a1].transform.localPosition,
                    cubeEdges[b1].transform.localPosition,
                    cubeEdges[a2].transform.localPosition,
                    cubeEdges[b2].transform.localPosition,
                };
                int[] tris = new int[6]
                {
                    0,1,2,3,4,5
                };

                var m = new Mesh()
                {
                    name = "Mesh",
                    vertices = verts,
                    triangles = tris
                };

                GameObject obj = new();
                obj.transform.SetParent(target.transform);
                obj.transform.localPosition = Vector3.zero;
                obj.AddComponent<MeshRenderer>().material = defaultMaterial;
                obj.AddComponent<MeshFilter>().mesh = m;
            }
        }
    }
}
