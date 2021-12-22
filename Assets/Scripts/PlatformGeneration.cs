using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour
{
    [SerializeField] int width, height;
    [SerializeField] int minHeight, maxHeight;
    [SerializeField] int spikeSpawnHight;
    [SerializeField] int repeatNum;
    [SerializeField] GameObject dirt, grass, spike;

    private void Start()
    {
        Generation();
    }
    
    void Generation()
    {
        int repeatValue = 0;
        for (int x = 0; x < width; x++)//This will help spawn a tile on the x axis
        {
            if(repeatValue == 0)
            {
                height = Random.Range(minHeight, maxHeight);

                GenerateFlatPlatform(x);
                repeatValue = repeatNum;
            }
            else
            {
                GenerateFlatPlatform(x);
                repeatValue--;
            }
        }
    }

    void GenerateFlatPlatform(int x)
    {
        for (int y = 0; y < height; y++)//This will help spawn a tile on the y axis
        {
            SpawnObj(dirt, x, y);
        }
        if(height < spikeSpawnHight)
        {
            SpawnObj(grass, x, height);
            SpawnObj(spike, x, height + 1);
        }
        else
        {
            SpawnObj(grass, x, height);
        }
    }

    void SpawnObj(GameObject obj, int widht, int height)//What ever we spawn will be a child of our procedural generation gameobject
    {
        obj = Instantiate(obj, new Vector2(widht, height), Quaternion.identity);
        obj.transform.parent = this.transform;
    }
}
