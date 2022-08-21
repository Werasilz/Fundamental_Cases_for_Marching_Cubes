using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingCubes : MonoBehaviour
{
    MarchTables marchTables = new();

    public Vector4[] points;
    public List<Triangle> triangles;
    public Vector4[] cubeCorners;
    [Range(2, 100)]
    public int numPointsPerAxis = 30;
    public float isoLevel = 0.5f;
    Vector3Int id;
    public int cubeIndex = 0;

    int indexFromCoord(int x, int y, int z)
    {
        // print(string.Format("{0},{1},{2}", x, y, z));
        print(z * numPointsPerAxis * numPointsPerAxis + y * numPointsPerAxis + x);
        return z * numPointsPerAxis * numPointsPerAxis + y * numPointsPerAxis + x;
    }

    Vector3 interpolateVerts(Vector4 v1, Vector4 v2)
    {
        float t = (isoLevel - v1.w) / (v2.w - v1.w);
        return v1 + t * (v2 - v1);
    }

    public void March()
    {
        // Tri count
        int numPoints = numPointsPerAxis * numPointsPerAxis * numPointsPerAxis;
        int numVoxelsPerAxis = numPointsPerAxis - 1;
        int numVoxels = numVoxelsPerAxis * numVoxelsPerAxis * numVoxelsPerAxis;
        int maxTriangleCount = numVoxels * 5;
        points = new Vector4[numPoints];
        triangles = new List<Triangle>(maxTriangleCount);

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new Vector4(Random.Range(-15f, 0f), Random.Range(-15f, 0f), Random.Range(-15f, 0f), Random.Range(-15f, 0f));
        }

        id = Vector3Int.zero;

        // 8 corners of the current cube
        cubeCorners = new Vector4[8]
        {
        points[indexFromCoord(id.x, id.y, id.z)],
        points[indexFromCoord(id.x + 1, id.y, id.z)],
        points[indexFromCoord(id.x + 1, id.y, id.z + 1)],
        points[indexFromCoord(id.x, id.y, id.z + 1)],
        points[indexFromCoord(id.x, id.y + 1, id.z)],
        points[indexFromCoord(id.x + 1, id.y + 1, id.z)],
        points[indexFromCoord(id.x + 1, id.y + 1, id.z + 1)],
        points[indexFromCoord(id.x, id.y + 1, id.z + 1)]
        };

        // Calculate unique index for each cube configuration.
        // There are 256 possible values
        // A value of 0 means cube is entirely inside surface; 255 entirely outside.
        // The value is used to look up the edge table, which indicates which edges of the cube are cut by the isosurface.

        // if (cubeCorners[0].w < isoLevel) cubeIndex |= 1;
        // if (cubeCorners[1].w < isoLevel) cubeIndex |= 2;
        // if (cubeCorners[2].w < isoLevel) cubeIndex |= 4;
        // if (cubeCorners[3].w < isoLevel) cubeIndex |= 8;
        // if (cubeCorners[4].w < isoLevel) cubeIndex |= 16;
        // if (cubeCorners[5].w < isoLevel) cubeIndex |= 32;
        // if (cubeCorners[6].w < isoLevel) cubeIndex |= 64;
        // if (cubeCorners[7].w < isoLevel) cubeIndex |= 128;

        // Create triangles for current cube configuration
        for (int i = 0; marchTables.triangulation[cubeIndex, i] != -1; i += 3)
        {
            // Get indices of corner points A and B for each of the three edges
            // of the cube that need to be joined to form the triangle.
            int a0 = marchTables.cornerIndexAFromEdge[marchTables.triangulation[cubeIndex, i]];
            int b0 = marchTables.cornerIndexBFromEdge[marchTables.triangulation[cubeIndex, i]];

            int a1 = marchTables.cornerIndexAFromEdge[marchTables.triangulation[cubeIndex, i + 1]];
            int b1 = marchTables.cornerIndexBFromEdge[marchTables.triangulation[cubeIndex, i + 1]];

            int a2 = marchTables.cornerIndexAFromEdge[marchTables.triangulation[cubeIndex, i + 2]];
            int b2 = marchTables.cornerIndexBFromEdge[marchTables.triangulation[cubeIndex, i + 2]];

            Triangle tri;
            tri.vertexA = interpolateVerts(cubeCorners[a0], cubeCorners[b0]);
            tri.vertexB = interpolateVerts(cubeCorners[a1], cubeCorners[b1]);
            tri.vertexC = interpolateVerts(cubeCorners[a2], cubeCorners[b2]);
            triangles.Add(tri);
        }

        // Mesh mesh;

        // var vertices = new Vector3[maxTriangleCount * 3];
        // var meshTriangles = new int[maxTriangleCount * 3];

        // for (int i = 0; i < numTris; i++)
        // {
        //     for (int j = 0; j < 3; j++)
        //     {
        //         meshTriangles[i * 3 + j] = i * 3 + j;
        //         vertices[i * 3 + j] = tris[i][j];
        //     }
        // }
        // mesh.vertices = vertices;
        // mesh.triangles = meshTriangles;
    }

    [System.Serializable]
    public struct Triangle
    {
        public Vector3 vertexC;
        public Vector3 vertexB;
        public Vector3 vertexA;
    };
}
