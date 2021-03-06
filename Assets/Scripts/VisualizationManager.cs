using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class VisualizationManager : MonoBehaviour
{
    JsonReader jsonReader;
    BuildManager buildManager = new BuildManager();
    int range = 7;
    bool ShowAllIslands = false, showCommits = true, showChanges = true, showCommitedFiles = true;
    string dataset = "aswi2017vana";
    [SerializeField]
    public UnityEngine.UI.Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        LoadDataset(dataset);
        Build();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Build()
    {
        ClearPanels();
        ClearObjects();
        slider.maxValue = buildManager.CreateIslands(jsonReader.allDates, range, ShowAllIslands) - 1;

        buildManager.CreateBuildings(jsonReader.authors, jsonReader.allDates, showCommits, showChanges, showCommitedFiles);
        buildManager.RenderBuildings();

        buildManager.CreatePowerLines(jsonReader.tickets);
        buildManager.RenderPowerLines();
    }

    //date range input
    public void GetInputDays(string days)
    {
        int daysInt;
        if (Int32.TryParse(days, out daysInt) && daysInt > 0)
        {
            range = daysInt;
            ChangeCameraPosition();
        }
        
    }

    //place camera in front of the first day of chosen data range
    public void ChangeCameraPosition()
    {
        var camera = GameObject.Find("MainCamera");
        if(buildManager.islands.FirstOrDefault(island => island.positionX == camera.transform.position.x).dates.Count != 0)
        {
            var dateFrom = buildManager.islands.FirstOrDefault(island => island.positionX == camera.transform.position.x).dates[0].Date;

            Build();
            var island = buildManager.islands.FirstOrDefault(island => island.dates.Contains(dateFrom));

            var xPos = island.positionX;
            slider.value = island.position;
            camera.transform.position = new Vector3(xPos, camera.transform.position.y, camera.transform.position.z);
        }
    }

    public void ShowAllDates(bool value)
    {
        ShowAllIslands = value;
        Build();
    }

    public void ShowCommits(bool value)
    {
        showCommits = value;
        Build();
    }

    public void ShowChanges(bool value)
    {
        showChanges = value;
        Build();
    }

    public void ShowFiles(bool value)
    {
        showCommitedFiles = value;
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
        LoadDataset(dropdown.options[dropdown.value].text);
        var camera = GameObject.Find("MainCamera");
        var overviewCamera = GameObject.Find("OverviewCamera");
        var IslandIndex = buildManager.islands.FirstOrDefault(island => island.positionX == camera.transform.position.x).index;
        Build();
        var island = buildManager.islands.FirstOrDefault(island => island.index == IslandIndex);
        if(island == null)
        {
            camera.transform.position = new Vector3(0, camera.transform.position.y, camera.transform.position.z);
            overviewCamera.transform.position = new Vector3(0, overviewCamera.transform.position.y, overviewCamera.transform.position.z);
            slider.value = 0;
        }
        else
        {
            camera.transform.position = new Vector3(island.positionX, camera.transform.position.y, camera.transform.position.z);
            overviewCamera.transform.position = new Vector3(island.positionX, overviewCamera.transform.position.y, overviewCamera.transform.position.z);
            slider.value = island.position;

        }    

    }

    public void LoadDataset(string dataset)
    {
        jsonReader = new JsonReader();
        jsonReader.LoadData(dataset);
    }
    public void SetOverviewCamera(int islandCount)
    {
        var overviewCamera = GameObject.Find("OverviewCamera");
        var mainCamera = GameObject.Find("MainCamera");

        overviewCamera.transform.position = new Vector3(mainCamera.transform.position.x, overviewCamera.transform.position.y, overviewCamera.transform.position.z);
    }

    public void ClearPanels()
    {
        var changesContent = GameObject.Find("DetailsContent").transform;
        var ticketsContent = GameObject.Find("TicketsContent").transform;
        GameObject.Find("WorkLabel").GetComponent<Text>().text = "";

        foreach (Transform child in changesContent)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in ticketsContent)
        {
            Destroy(child.gameObject);
        }

    }

}
