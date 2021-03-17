using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    public GameObject islandPrefab;
    
    float x = 0;
    float y = 0;
    float z = 0;

    public void CreateIslands(int count, List<string> dates)
    {
        islandPrefab = (GameObject)Resources.Load("Prefabs/IslandPrefab", typeof(GameObject));

        float x_pos=0;
        foreach (var date in dates)
        {
            var platform = Instantiate(islandPrefab, new Vector3(x_pos, z, y), Quaternion.identity);

            x_pos += 15;
        }
    }

}
