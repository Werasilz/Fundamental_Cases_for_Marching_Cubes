using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeData : MonoBehaviour
{
    TriangulationTables table = new();

    public Vector3[] vertexPos;
    public Vector3[] edgePos;
    public List<int> density = new List<int>();
    public int surfaceLevel = 1;
    public int cubeIndex;
    private List<Vector3> vertices;

    public void Create()
    {
        cubeIndex = 0;

        // Find cube index
        if (density[0] < surfaceLevel) cubeIndex |= 1;
        if (density[1] < surfaceLevel) cubeIndex |= 2;
        if (density[2] < surfaceLevel) cubeIndex |= 4;
        if (density[3] < surfaceLevel) cubeIndex |= 8;
        if (density[4] < surfaceLevel) cubeIndex |= 16;
        if (density[5] < surfaceLevel) cubeIndex |= 32;
        if (density[6] < surfaceLevel) cubeIndex |= 64;
        if (density[7] < surfaceLevel) cubeIndex |= 128;

        cubeIndex = Mathf.Abs(cubeIndex - 255);

        // List for store vertex
        int amount = 0;
        for (int i = 0; table.triangulation[cubeIndex, i] != -1; i++)
            amount++;
        vertices = new List<Vector3>(amount);

        // Get result vertex to draw
        for (int i = 0; table.triangulation[cubeIndex, i] != -1; i += 3)
        {
            int a0 = table.triangulation[cubeIndex, i];
            int b0 = table.triangulation[cubeIndex, i];

            int a1 = table.triangulation[cubeIndex, i + 1];
            int b1 = table.triangulation[cubeIndex, i + 1];

            int a2 = table.triangulation[cubeIndex, i + 2];
            int b2 = table.triangulation[cubeIndex, i + 2];

            Vector3 vertexPos1 = (edgePos[a0] + edgePos[b0]) / 2;
            Vector3 vertexPos2 = (edgePos[a1] + edgePos[b1]) / 2;
            Vector3 vertexPos3 = (edgePos[a2] + edgePos[b2]) / 2;

            vertices.Add(vertexPos1);
            vertices.Add(vertexPos2);
            vertices.Add(vertexPos3);
        }

        GetComponent<MeshFilter>().mesh = CreateMesh();
    }

    Mesh CreateMesh()
    {
        // Get triangles amount
        int[] tri = new int[vertices.Count];
        for (int i = 0; i < tri.Length; i++)
            tri[i] = i;

        // Create mesh
        var m = new Mesh()
        {
            name = "Mesh",
            vertices = vertices.ToArray(),
            triangles = tri,
        };

        return m;
    }

    private void OnDrawGizmos()
    {
        // Cube gizmos
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, Vector3.one);

        GUIStyle style = new GUIStyle();
        style.fontSize = 20;

        // Vertex
        style.normal.textColor = Color.red;
        for (int i = 0; i < vertexPos.Length; i++)
            UnityEditor.Handles.Label(transform.localPosition + vertexPos[i], "v" + i, style);
        style.normal.textColor = Color.green;

        // Edge
        for (int i = 0; i < edgePos.Length; i++)
            UnityEditor.Handles.Label(transform.localPosition + edgePos[i], "e" + i, style);

        // Index
        style.normal.textColor = Color.black;
        for (int i = 0; i < edgePos.Length; i++)
            UnityEditor.Handles.Label(transform.localPosition, "Cube Index: " + cubeIndex.ToString(), style);
    }
}
