using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator_2 : MonoBehaviour
{
    public enum DrawMode
    {
        NoiseMap,
        ColorMap,
        Mesh
    };
    public DrawMode drawMode;

    public const int mapChunkSize = 241; //actual mesh size in 240
    [Range(0,6)]
    public int levelOfDetail;
    public float noiseScale;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public AnimationCurve meshHeightCurve;

    public float meshHeightMultiplier;

    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public TerrainType[] regions;

    private List<Vector2> grassUVcoordinates = new List<Vector2>();
    public float minGrassHeight, maxGrassHeight;
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];

        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currHeight <= regions[i].height)
                    {
                        colorMap[y * mapChunkSize + x] = regions[i].color;
                        break;
                    }
                   if (regions[i].name == "Grass")
                    {
                        //generate grass shader 
/*                        maxGrassHeight = regions[i].height;
                        minGrassHeight = regions[i-1].height;*/
                    }
/*                      else
                        grassUVcoordinates.Add(new Vector2(x, 0));*/
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode.Equals(DrawMode.NoiseMap))
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        else if (drawMode.Equals(DrawMode.ColorMap))
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        else if (drawMode.Equals(DrawMode.Mesh))
        {
            display.DrawMesh(MeshGeneratorSebastian.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail), TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize), minGrassHeight);
        }
    }

    private void OnValidate()
    {
        if (lacunarity < 1)
            lacunarity = 1;
        if (octaves < 0)
            octaves = 0;
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}