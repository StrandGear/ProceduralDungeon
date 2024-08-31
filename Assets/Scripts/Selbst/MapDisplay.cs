using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    public Material grassShaderMaterial;

    public void DrawTexture(Texture2D texture)
    {
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
    public void DrawMesh(MeshData meshData, Texture2D texture,  float minGrassHeight = 0, float maxGrassHeight = 0)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;

        grassShaderMaterial.SetFloat("_GrassHeight", minGrassHeight);

        /*        Vector3[] UVShaderCoords = new Vector3[meshFilter.sharedMesh.vertices.Length];
                for (int i = 0; i < meshFilter.sharedMesh.vertices.Length; i++)
                {
                    if (meshFilter.sharedMesh.vertices[i].y < maxGrassHeight || meshFilter.sharedMesh.vertices[i].y > minGrassHeight)
                    {
                        UVShaderCoords[i].x = meshFilter.sharedMesh.vertices[i].x;
                        UVShaderCoords[i].y = meshFilter.sharedMesh.vertices[i].y;
                        UVShaderCoords[i].z = meshFilter.sharedMesh.vertices[i].z;
                    }
                    else
                    {
                        UVShaderCoords[i].x = 0;
                        UVShaderCoords[i].y = 0;
                        UVShaderCoords[i].z = 0;
                    }
                }*/
    }

    public void DrawShader(Vector3[] UVShaderCoords, Mesh mesh)
    {
        mesh.SetUVs(1, UVShaderCoords);
        print("shading");
        grassShaderMaterial.SetFloat("_GrassHeight", 1);
        //grassShaderMaterial.SetFloatArray("_TerrainHeights", UVcoords);
        //grassShaderMaterial.SetTexture("_TerrainHeights", mesh.uv2);
    }
}
