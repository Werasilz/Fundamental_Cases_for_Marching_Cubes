using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCases : MonoBehaviour
{
    void Start()
    {
        MeshData[] meshDatas = GetComponentsInChildren<MeshData>();
        foreach (var item in meshDatas)
            item.DrawMesh();
    }
}
