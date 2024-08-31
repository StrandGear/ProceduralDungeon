using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (MapGenerator_2))]
public class MapGenratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator_2 mapGen = (MapGenerator_2) target;

        if (DrawDefaultInspector()) //if values were updated
        {
            if (mapGen.autoUpdate)
                mapGen.GenerateMap();
        }

        //DrawDefaultInspector();

        if (GUILayout.Button("Generate"))
            mapGen.GenerateMap();
    }
}
