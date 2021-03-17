using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandManager : MonoBehaviour
{
    public GameObject islandPrefab;

    public void CreateIslands(int count, List<string> dates)
    {
        float y = 0;
        float z = 0;
        float gap = 2;
        islandPrefab = (GameObject)Resources.Load("Prefabs/IslandPrefab", typeof(GameObject));

        float x_pos=0;
        foreach (var date in dates)
        {
            var platform = Instantiate(islandPrefab, new Vector3(x_pos, z, y), Quaternion.identity);
            platform.transform.GetChild(1).GetComponent<TextMesh>().text = date;
            
            x_pos += islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.size.x + gap;
        }
    }

}
