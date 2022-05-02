using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTerrainGenerator : MonoBehaviour
{
    // One pixel per vertex
    public Texture2D heightMap;
    public Material terrainMaterial;
    public float metersBetweenVerts = 1;

    public void GenerateTerrain()
    {
        MyTerrainMeshesGenerator meshGen = FindObjectOfType<MyTerrainMeshesGenerator>();
        meshGen.GenerateTerrainMeshes(heightMap, metersBetweenVerts, terrainMaterial);
    }
}
