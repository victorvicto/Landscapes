using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MyTerrainGenerator))]
public class MyTerrainGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MyTerrainGenerator terrainGenerator = (MyTerrainGenerator)target;

        DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
        {
            terrainGenerator.GenerateTerrain();
        }
    }
}
