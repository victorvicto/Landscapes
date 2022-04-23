using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTerrainMeshesGenerator : MonoBehaviour
{
    public int numChunksHorizontal = 1;
    public int numChunksVertical = 1;
    public int chunkRes = 121;
    [Range(1, 6)]
    public int numLOD = 1;
    public void GenerateTerrainMeshes(Texture2D heightMap, float pixelsPerMeter)
    {
        float terrainWidth = heightMap.width;
        float terrainHeight = heightMap.height;
        float horizontalDistBetweenVerts = terrainWidth / ((numChunksHorizontal - 1) * chunkRes);
        float verticalDistBetweenVerts = terrainHeight / ((numChunksVertical - 1) * chunkRes);
        for (int LOD = 1; LOD <= numLOD; LOD++)
        {

        }
    }
}
