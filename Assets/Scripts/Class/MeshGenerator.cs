using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public int width = 20;
    public int depth = 20;

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    float waveAnimationTimer = 0f;
    public float waveHeight;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        //CreateQuad();
        CreateGrid();
        UpdateMesh();

        //Dynamically set the camera so the whole mesh is visible
        Camera.main.transform.position = new Vector3(width / 2, 10, depth / 5);
        Camera.main.transform.LookAt(vertices[vertices.Length / 2]);
    }

    /// <summary>
    /// Create a group of quads automated to generate a grid of quads.
    /// </summary>
    void CreateGrid()
    {
        vertices = new Vector3[(1 + width) * (1 + depth)];

        for (int z = 0, i = 0; z <= depth; z++)
            for (int x = 0; x <= width; x++, i++)
                vertices[i] = new Vector3(x, Mathf.Sin(z), z);

        triangles = new int[6 * width * depth];

        int currentVertice = 0;
        int numOfTriangles = 0;

        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                triangles[numOfTriangles + 0] = currentVertice + 0;
                triangles[numOfTriangles + 1] = currentVertice + width + 1;
                triangles[numOfTriangles + 2] = currentVertice + 1;
                triangles[numOfTriangles + 3] = currentVertice + 1;
                triangles[numOfTriangles + 4] = currentVertice + width + 1;
                triangles[numOfTriangles + 5] = currentVertice + width + 2;

                numOfTriangles += 6;
                currentVertice++;
            }
            currentVertice++;
        }
    }

    /// <summary>
    /// Create a single Quad by hand.
    /// We've started with this code and then switched to "Create Grid"
    /// </summary>
    void CreateQuad()
    {
        vertices = new Vector3[]
        {
            new Vector3(0,0,0),
            new Vector3(0,0,1),
            new Vector3(1,0,0),
            new Vector3(1,0,1)
        };

        triangles = new int[]
        {
            0, 1, 2,
            1, 3, 2
        };
    }

    /// <summary>
    /// Update the Mesh Data
    /// </summary>
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }

    void UpdateQuadWaveAnimation()
    {
        waveAnimationTimer += Time.deltaTime;

        for (int z = 0, i = 0; z <= width; ++z)
        {
            for (int x = 0; x <= width; ++x, ++i)
            {
                float baseHeight = Mathf.Cos(z + waveAnimationTimer) + Mathf.Sin(x + waveAnimationTimer);
                //float baseHeight = Mathf.Sin(z);
                //y = Noise(x)
                vertices[i].y = baseHeight * waveHeight;
                //vertices[i].y = Mathf.Sin(x);
            }
        }
    }

    /// <summary>
    /// Draw vertice position in the scene view
    /// Deactivate to improve performance
    /// </summary>
    private void OnDrawGizmos()
    {
        if (vertices == null) return;
        for (int i = 0; i < vertices.Length; i++)
            Gizmos.DrawSphere(vertices[i], 0.1f);
    }
    void Update()
    {
        UpdateQuadWaveAnimation();
        UpdateMesh();
    }

}
