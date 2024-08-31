/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteTerrain : MonoBehaviour
{
    public int width;
    public int depth;
    public int chunkDimension;
    public float terrainHeight = 15;
    public Gradient terrainColorGradient;
    public Material mTerrain;

    private void Start()
    {
        StartCoroutine(GenerateTerrain());

        //Dynamically set the camera so the whole mesh is visible
        Camera.main.transform.position = new Vector3(width / 2f * chunkDimension, 3 * terrainHeight, (depth / 10f) * chunkDimension);
        Camera.main.transform.LookAt(new Vector3(chunkDimension * (width/2f), 0, chunkDimension * (depth/2f)));
    }
    IEnumerator GenerateTerrain()
    {
        for (int z = 0; z < depth; z++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject gm = new GameObject("Chunk_" + x + "_" + z, typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshGenerator));
                gm.transform.parent = transform;
                gm.transform.localPosition = new Vector3(x * chunkDimension, 0 , z * chunkDimension);
                gm.GetComponent<MeshRenderer>().material = mTerrain;
                gm.GetComponent<MeshGenerator>().Init(chunkDimension, terrainHeight, x, z, terrainColorGradient);
                yield return new WaitForEndOfFrame();
            }
            yield return true;
        }
    }
}
*/