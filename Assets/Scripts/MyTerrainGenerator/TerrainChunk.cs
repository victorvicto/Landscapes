using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainChunk
{
    // One mesh per LOD
    public GameObject[] meshes;

    public Transform parent;

    public int chunkXIndex;
    public int chunkZIndex;

    public int startXPixel;
    public int startZPixel;

    // coordinates of bottom left corner
    public float startXPos;
    public float startZPos;
    public float size;


    public TerrainChunk(int numLODs, int _chunkXIndex, int _chunkZIndex, int chunkRes, Texture2D heightMap, float metersBetweenVerts, Transform _parent, Material terrainMaterial)
    {
        meshes = new GameObject[numLODs];

        parent = _parent;

        chunkXIndex = _chunkXIndex;
        chunkZIndex = _chunkZIndex;

        float terrainWidth = heightMap.width * metersBetweenVerts;
        Debug.Log(terrainWidth);
        float terrainHeight = heightMap.height * metersBetweenVerts;
        Debug.Log(terrainWidth);
        size = (chunkRes - 1) * metersBetweenVerts;
        startXPixel = chunkXIndex * (chunkRes - 1);
        startZPixel = chunkZIndex * (chunkRes - 1);
        startXPos = startXPixel * metersBetweenVerts - (terrainWidth / 2);
        startZPos = (terrainHeight / 2) - (startZPixel * metersBetweenVerts); // Doing this so that the map is drawn from TOP left to bottom right

        GenerateMeshes(chunkRes, metersBetweenVerts, heightMap, terrainMaterial);
    }

    void GenerateMeshes(int chunkRes, float metersBetweenVerts, Texture2D heightMap, Material terrainMaterial)
    {
        int numLODs = meshes.Length;
        // Looping over the level of details to have multiple versions of each chunk, one per LOD
        for (int LOD = 1; LOD <= numLODs; LOD++)
        {
            // Initialising the dots position at negative half of terrain size to have terrain centered
            float currentXPos = startXPos;
            float currentZPos = startZPos;

            int verticesPerLine = ((chunkRes - 1) / LOD) + 1;
            MeshData meshData = new MeshData(verticesPerLine, verticesPerLine);
            int vertexIndex = 0;

            for (int z = 0; z < chunkRes; z += LOD)
            {
                for (int x = 0; x < chunkRes; x += LOD)
                {
                    int currentXPixel = startXPixel + x;
                    int currentZPixel = startZPixel + z;
                    float height = heightMap.GetPixel(currentXPixel, currentZPixel).r * 20;
                    meshData.vertices[vertexIndex] = new Vector3(currentXPos, height, currentZPos);
                    meshData.uvs[vertexIndex] = new Vector2(currentXPixel / (float)(heightMap.width), currentZPixel / (float)(heightMap.height));

                    if (x < chunkRes - 1 && z < chunkRes - 1)
                    {
                        meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                        meshData.AddTriangle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                    }

                    vertexIndex++;
                    currentXPos += metersBetweenVerts;
                }
                currentXPos = startXPos;
                currentZPos -= metersBetweenVerts; // Again, we use minus because we start from TOP left to bottom right
            }

            GameObject meshObject = new GameObject("Terrain Chunk " + chunkXIndex + "-" + chunkZIndex + " LOD" + LOD);
            meshObject.transform.parent = parent;
            MeshRenderer meshRenderer = meshObject.AddComponent<MeshRenderer>();
            MeshFilter meshFilter = meshObject.AddComponent<MeshFilter>();
            meshRenderer.material = terrainMaterial;
            meshFilter.mesh = meshData.CreateMesh();
            meshes[LOD - 1] = meshObject;

        }
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    int triangleIndex;

    public MeshData(int meshWidth, int meshHeight)
    {
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }

}
