using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class Terraformer : MonoBehaviour
{
    Terrain terrain;
    float[,] heightMap;

    private void Awake()
    {
        terrain = GetComponent<Terrain>();
        heightMap = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapResolution, terrain.terrainData.heightmapResolution);
    }
    public void terraform(Vector3 pos, float radius, float strength)
    {
        int size = heightMap.GetLength(0);
        pos.x *= (size / GetComponent<Terrain>().terrainData.size.x);
        pos.z *= (size / GetComponent<Terrain>().terrainData.size.z);

        float distance = 0;
        Vector2 initialPos = new Vector2(pos.x, pos.z), currPos;
        for (int i = (int)(pos.x - radius / 2); i < (int)(pos.x + radius / 2); i++)
        {
            for (int j = (int)(pos.z - radius / 2); j < (int)(pos.z + radius / 2); j++)
            {
                currPos.x = i;
                currPos.y = j;
                distance = Vector2.Distance(currPos, initialPos);
                //Debug.LogWarning(i + " " + j + " " + strength * (1 - (distance / radius)));
                heightMap[j, i] -= strength * (1 - (distance/ radius)) / terrain.terrainData.size.y;
                if (heightMap[j, i] < 0) heightMap[j, i] = 0;
            }
        }

        UpdateMeshVertices(heightMap);
    }

    private void UpdateMeshVertices(float[,] heightMap)
    {
        int tileDepth = heightMap.GetLength(0);
        int tileWidth = heightMap.GetLength(1);

        float[,] h = GetComponent<Terrain>().terrainData.GetHeights(0, 0, tileDepth, tileWidth);

        int vertexIndex = 0;
        for (int zIndex = 0; zIndex < tileDepth; zIndex++)
        {
            for (int xIndex = 0; xIndex < tileWidth; xIndex++)
            {
                float height = heightMap[zIndex, xIndex];

                h[zIndex, xIndex] = height;
                vertexIndex++;
            }
        }
        GetComponent<Terrain>().terrainData.SetHeights(0, 0, h);
    }
}
