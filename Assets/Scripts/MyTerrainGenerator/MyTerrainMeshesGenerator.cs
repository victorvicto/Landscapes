using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTerrainMeshesGenerator : MonoBehaviour
{
    public int chunkRes = 121;
    [Range(1, 6)]
    public int numLODs = 1;
    public void GenerateTerrainMeshes(Texture2D heightMap, float metersBetweenVerts)
    {

        TerrainChunk testChunk = new TerrainChunk(numLODs, 0, 0, chunkRes, heightMap, metersBetweenVerts, this.transform);
    }
}
