using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTerrainMeshesGenerator : MonoBehaviour
{
    public int chunkRes = 121;
    [Range(1, 6)]
    public int numLODs = 1;
    public void GenerateTerrainMeshes(Texture2D heightMap, float metersBetweenVerts, Material terrainMaterial)
    {
        if ((heightMap.width - 1) % 120 != 0 || (heightMap.height - 1) % 120 != 0)
        {
            Debug.Log("Heightmap doesn't have the right proportions (should be multiples of 120 + 1)");
            return;
        }

        int numXChunks = (heightMap.width - 1) / 120;
        int numZChunks = (heightMap.height - 1) / 120;
        for (int chunkXIndex = 0; chunkXIndex < numXChunks; chunkXIndex++)
        {
            for (int chunkZIndex = 0; chunkZIndex < numZChunks; chunkZIndex++)
            {
                TerrainChunk testChunk = new TerrainChunk(numLODs, chunkXIndex, chunkZIndex, chunkRes, heightMap, metersBetweenVerts, this.transform, terrainMaterial);
            }
        }
    }
}
