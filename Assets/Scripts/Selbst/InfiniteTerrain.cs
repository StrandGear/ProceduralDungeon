using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteTerrain : MonoBehaviour
{
    public const float maxViewDistance = 300;
    public Transform viewer;

    public static Vector2 viewerPosition;
    int chunkSize;
    int chunksVisibleInViewDistance;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();

    private void Start()
    {
        chunkSize = MapGenerator_2.mapChunkSize - 1;
        chunksVisibleInViewDistance = Mathf.RoundToInt(maxViewDistance / chunkSize);
    }

    void UpdateVisibleChunks()
    {
        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

        for (int yOffset = -chunksVisibleInViewDistance; yOffset <= chunksVisibleInViewDistance; yOffset++)
        {
            for (int xOffset = -chunksVisibleInViewDistance; xOffset < chunksVisibleInViewDistance; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                if (terrainChunkDictionary.ContainsKey(viewedChunkCoord))
                {
                    //
                }
                else
                {
                    //terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk());
                }
            }
        }
    }

    public class TerrainChunk
    {
        GameObject meshObject;
        Vector2 position;

        public TerrainChunk(Vector2 coord, int size)
        {
            position = coord * size;
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);

            meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
        }
    }
}
