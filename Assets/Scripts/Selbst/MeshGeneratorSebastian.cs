using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGeneratorSebastian 
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve, int levelOfDetail)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float halfWidth = (width - 1) / 2f;
        float halfHeight = (height - 1) / 2f;

        int meshSimplificationIncrement = (levelOfDetail == 0)?1:levelOfDetail * 2;
        int verticesPerLine = (width - 1) / meshSimplificationIncrement + 1;

        MeshData meshData = new MeshData(width,height);
        int vertexIndex = 0;

        for (int y = 0; y < height; y+= meshSimplificationIncrement)
        {
            for (int x = 0; x < width; x+= meshSimplificationIncrement)
            {
                meshData.vertices[vertexIndex] = new Vector3(x - halfWidth, heightCurve.Evaluate(heightMap[x,y]) * heightMultiplier, y - halfHeight);
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

                //terrainMesh.SetUVs(1, heights); // Set vertex attribute (UVs) to store terrain height
                //grassMaterial.SetFloatArray("_TerrainHeights", heights); // Pass heights to shader

            if (x < width - 1 && y < height -1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine, vertexIndex + verticesPerLine + 1);
                    meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + 1);

                }

                vertexIndex++;
            }
        }
        return meshData;
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
        uvs = new Vector2[meshWidth * meshWidth];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    public void AddTriangle (int a, int b, int c)
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