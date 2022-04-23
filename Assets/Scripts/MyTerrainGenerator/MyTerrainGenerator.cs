using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTerrainGenerator : MonoBehaviour
{
    public Texture2D heightMap;
    public float pixelsPerMeter = 1;

    public void GenerateTerrain()
    {
        MyTerrainMeshesGenerator meshGen = FindObjectOfType<MyTerrainMeshesGenerator>();
        meshGen.GenerateTerrainMeshes(heightMap, pixelsPerMeter);
    }
}
