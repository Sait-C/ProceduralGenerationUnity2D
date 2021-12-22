using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] int width;
    //[SerializeField] int height;
    [SerializeField] int minStoneHeight, maxStoneHeight;
    [Range(0, 100)]
    [SerializeField] float heightValue, smoothness;
    //[SerializeField] GameObject dirt, grass, stone;
    [SerializeField] Tilemap dirtTileMap, grassTileMap, stoneTileMap;
    [SerializeField] Tile dirt, grass, stone;
    float seed;
    private void Start()
    {
        seed = Random.Range(-1000000, 1000000);
        Generation();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            seed = Random.Range(-1000000, 1000000);
            Generation();
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            stoneTileMap.ClearAllTiles();
            grassTileMap.ClearAllTiles();
            dirtTileMap.ClearAllTiles();
        }
    }

    void Generation()
    {
        for (int x = 0; x < width; x++)//This will help spawn a tile on the x axis
        {
            /*
            //now for procedural generation we need to gradually increase and decrease the height value
            int minHeight = height - 1;
            int maxHeight = height + 2;

            height = Random.Range(minHeight, maxHeight);
            */
            int height = Mathf.RoundToInt(heightValue * Mathf.PerlinNoise(x / smoothness, seed));

            int minStoneSpawnDistance = height - minStoneHeight;
            int maxStoneSpawnDistance = height - maxStoneHeight;
            int totalStoneSpawnDistance = Random.Range(minStoneSpawnDistance, maxStoneSpawnDistance);
            for (int y = 0; y < height; y++)//This will help spawn a tile on the y axis
            {
                if(y < totalStoneSpawnDistance)
                {
                    //SpawnObj(stone, x, y);
                    stoneTileMap.SetTile(new Vector3Int(x, y, 0), stone);
                }
                else
                {
                    //SpawnObj(dirt, x, y);
                    dirtTileMap.SetTile(new Vector3Int(x, y, 0), dirt);
                }
            }
            if(totalStoneSpawnDistance == height)
            {
                //SpawnObj(stone, x, height);
                stoneTileMap.SetTile(new Vector3Int(x, height, 0), stone);
            }
            else
            {
                //SpawnObj(grass, x, height);
                grassTileMap.SetTile(new Vector3Int(x, height, 0), grass);
            }
        }
    }

    /*
    void SpawnObj(GameObject obj, int widht, int height)//What ever we spawn will be a child of our procedural generation gameobject
    {
        obj = Instantiate(obj, new Vector2(widht, height), Quaternion.identity);
        obj.transform.parent = this.transform;
    }
    */
}
