using System;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    MarchTables marchTables = new();
    public GameObject target;
    public Transform[] vertex;
    public Transform[] edge;
    public List<int> density = new List<int>();
    public int surfaceLevel;
    public int cubeIndex;
    public List<Vector3> vertices;

    private void Start()
    {
        DrawMesh();
    }

    public void DrawMesh()
    {
        cubeIndex = 0;

        if (density[0] < surfaceLevel) cubeIndex |= 1;
        if (density[1] < surfaceLevel) cubeIndex |= 2;
        if (density[2] < surfaceLevel) cubeIndex |= 4;
        if (density[3] < surfaceLevel) cubeIndex |= 8;
        if (density[4] < surfaceLevel) cubeIndex |= 16;
        if (density[5] < surfaceLevel) cubeIndex |= 32;
        if (density[6] < surfaceLevel) cubeIndex |= 64;
        if (density[7] < surfaceLevel) cubeIndex |= 128;

        int amount = 0;
        for (int i = 0; marchTables.triangulation[cubeIndex, i] != -1; i++)
            amount++;
        vertices = new List<Vector3>(amount);

        for (int i = 0; marchTables.triangulation[cubeIndex, i] != -1; i += 3)
        {
            int a0 = marchTables.triangulation[cubeIndex, i];
            int b0 = marchTables.triangulation[cubeIndex, i];

            int a1 = marchTables.triangulation[cubeIndex, i + 1];
            int b1 = marchTables.triangulation[cubeIndex, i + 1];

            int a2 = marchTables.triangulation[cubeIndex, i + 2];
            int b2 = marchTables.triangulation[cubeIndex, i + 2];

            Vector3 vertexPos1 = (edge[a0].transform.position + edge[b0].transform.position) / 2;
            Vector3 vertexPos2 = (edge[a1].transform.position + edge[b1].transform.position) / 2;
            Vector3 vertexPos3 = (edge[a2].transform.position + edge[b2].transform.position) / 2;

            vertices.Add(vertexPos3);
            vertices.Add(vertexPos2);
            vertices.Add(vertexPos1);
        }

        target.GetComponent<MeshFilter>().mesh = CreateMesh();
    }

    Mesh CreateMesh()
    {
        int[] tri = new int[vertices.Count];
        for (int i = 0; i < tri.Length; i++)
            tri[i] = i;

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
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, Vector3.one);

        for (int i = 0; i < vertex.Length; i++)
            Gizmos.DrawWireSphere(vertex[i].position, 0.05f);

        for (int i = 0; i < edge.Length; i++)
            Gizmos.DrawWireSphere(edge[i].position, 0.05f);
    }
}
