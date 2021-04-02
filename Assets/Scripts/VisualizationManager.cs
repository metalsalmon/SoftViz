using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class VisualizationManager : MonoBehaviour
{
    JsonReader jsonReader;
    BuildManager buildManager = new BuildManager();
    int range = 7;
    bool ShowAllIslands = false;
    //string dataset = "aswi2017vana";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Build()
    {
        buildManager.CreateIslands(jsonReader.allDates, range, ShowAllIslands);

        buildManager.CreateBuildings(jsonReader.authors, jsonReader.allDates, true, true, true);
        buildManager.RenderBuildings();

        buildManager.CreatePowerLines(jsonReader.tickets);
        buildManager.RenderPowerLines();
    }

    public void GetInputDays(string days)
    {

        if (Int32.TryParse(days, out range))
        {
            ClearObjects();
            Build();
        }
        
    }

    public void ShowAllDates(bool value)
    {
        ClearObjects();
        ShowAllIslands = value;
        Build();
    }

    public void ClearObjects()
    {
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Island");
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }

        allObjects = GameObject.FindGameObjectsWithTag("PowerLine");
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }

        buildManager = new BuildManager();
    }

    public void ChooseDataset(Dropdown dropdown)
    {
        ClearObjects();
        LoadDataset(dropdown.options[dropdown.value].text);
        Build();
    }

    public void LoadDataset(string dataset)
    {
        jsonReader = new JsonReader();
        jsonReader.LoadData(dataset);
    }

}
