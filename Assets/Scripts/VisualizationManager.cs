using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisualizationManager : MonoBehaviour
{
    JsonReader jsonReader = new JsonReader();
    BuildManager buildManager = new BuildManager();
    List<Building> buildings = new List<Building>();

    // Start is called before the first frame update
    void Start()
    {

        jsonReader.LoadData("aswi2017vana");

        buildManager.CreateIslands(jsonReader.dates);

        buildManager.CreateBuildings(jsonReader.authors, jsonReader.dates);
        buildManager.RenderBuildings();  
    }

    // Update is called once per frame
    void Update()
    {
       
    }


}
