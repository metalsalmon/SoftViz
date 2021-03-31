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
        int range = 7;
        jsonReader.LoadData("aswi2017vana");

        buildManager.CreateIslands(jsonReader.allDates, range, false);

        buildManager.CreateBuildings(jsonReader.authors, jsonReader.allDates, true, true, true);
        buildManager.RenderBuildings();

        buildManager.CreatePowerLines(jsonReader.tickets);
        buildManager.RenderPowerLines();
    }

    // Update is called once per frame
    void Update()
    {
       
    }


}
