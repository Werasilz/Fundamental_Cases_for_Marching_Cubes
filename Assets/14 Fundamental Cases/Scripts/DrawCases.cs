using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCases : MonoBehaviour
{
    //https:developer.nvidia.com/gpugems/gpugems3/part-i-geometry/chapter-1-generating-complex-procedural-terrains-using-gpu
    // Cases 1 : 1000 0000
    // Cases 2 : 1001 0000
    // Cases 3 : 1010 0000
    // Cases 4 : 1000 0010
    // Cases 5 : 0001 1001
    // Cases 6 : 1001 0010
    // Cases 7 : 0101 0010
    // Cases 8 : 1001 1001
    // Cases 9 : 1000 1101
    // Cases 10 : 1100 0011
    // Cases 11 : 1000 1011
    // Cases 12 : 0101 1001
    // Cases 13 : 1010 0101
    // Cases 14 : 0001 1101

    void Start()
    {
        CubeData[] cubeDatas = GetComponentsInChildren<CubeData>();
        foreach (var item in cubeDatas)
            item.Create();
    }
}
