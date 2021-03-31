using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GameObject islandPrefab;
    public GameObject buildingPrefab;
    public GameObject commitsPrefab;
    public GameObject filesPrefab;
    public GameObject changesPrefab;
    public GameObject powerlinePrefab;
    public List<Building> buildings = new List<Building>();
    public List<PowerLine> powerLines = new List<PowerLine>();
    public List<Island> islands = new List<Island>();

    public void CreateIslands(List<DateTime> dates, int range, bool showAll)
    {
        islandPrefab = (GameObject)Resources.Load("Prefabs/IslandPrefab", typeof(GameObject));
        DateTime dateTo = DateTime.MinValue;
        float gap = 2;
        float x = 0;
        float y = 0;
        float z = 0;
        DateTime StartDate = dates[0];
        bool AddIsland;

        while(StartDate < dates.Last())
        {
            islands.Add(new Island(StartDate, StartDate.AddDays(range - 1)));
            StartDate = StartDate.AddDays(range);
        }

        foreach (var island in islands)
        {
            if (showAll)
            {
                var islandInstance = Instantiate(islandPrefab, new Vector3(x, z, y), Quaternion.identity);
                islandInstance.transform.GetChild(1).GetComponent<TextMesh>().text = island.DateFrom.Value.ToString("dd.MM.yyyy") + " - " + island.DateTo.Value.ToString("dd.MM.yyyy");
                island.islandInstance = islandInstance;
                x += islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.size.x + gap;
            }
            AddIsland = true;
            foreach (var date in dates)
            {
                if (date >= island.DateFrom && date <= island.DateTo)
                {

                    if(AddIsland)
                    {
                        if (!showAll)
                        {
                            var islandInstance = Instantiate(islandPrefab, new Vector3(x, z, y), Quaternion.identity);
                            islandInstance.transform.GetChild(1).GetComponent<TextMesh>().text = island.DateFrom.Value.ToString("dd.MM.yyyy") + " - " + island.DateTo.Value.ToString("dd.MM.yyyy");
                            island.islandInstance = islandInstance;
                            x += islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.size.x + gap;
                        }
                        island.AddDate(date);
                        AddIsland = false;
                        island.show = true;
                    }
                    else
                    {
                        island.AddDate(date);
                    }
                }
            }
        }
    }

    public void RenderBuildings()
    {
        buildingPrefab = (GameObject)Resources.Load("Prefabs/BuildingPrefab", typeof(GameObject));
        commitsPrefab = (GameObject)Resources.Load("Prefabs/CommitsPrefab", typeof(GameObject));
        filesPrefab = (GameObject)Resources.Load("Prefabs/FilesPrefab", typeof(GameObject));
        changesPrefab = (GameObject)Resources.Load("Prefabs/ChangesPrefab", typeof(GameObject));
        var personSizeY = buildingPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.size.y / 2;

        foreach (var island in islands)
        {
            if(island.show)
            {
                float IslandMinX = islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.min.x + 1;
                float IslandMinZ = islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.min.z + 3;

                foreach (var building in buildings.Where(building => building.show && building.dateFrom == island.DateFrom))
                {
                    GameObject filesInstance = null;
                    GameObject changesInstance = null;
                    GameObject commitInstance = null;

                    buildingPrefab.transform.position = new Vector3(IslandMinX, 0, 0);
                
                    IslandMinX += buildingPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.size.x + 0.2f;
                    var buildingInstance = Instantiate(buildingPrefab, island.islandInstance.gameObject.transform, false);
                    buildingInstance.transform.localPosition = new Vector3(buildingInstance.transform.localPosition.x, buildingInstance.transform.localPosition.y, IslandMinZ + building.tickets.Count);


                    if (building.commits.Count != 0)
                    {
                        commitInstance = Instantiate(commitsPrefab, buildingInstance.transform, false);
                        commitInstance.transform.localPosition = new Vector3(commitInstance.transform.localPosition.x, commitInstance.transform.localPosition.y + personSizeY, commitInstance.transform.localPosition.z);
                        commitInstance.transform.localScale = new Vector3(commitInstance.transform.localScale.x, building.commits.Count/3f, commitInstance.transform.localScale.z);
                        var commitManager = commitInstance.transform.GetChild(0).GetComponent<CommitsManager>();
                        commitManager.SetBuilding(building);
                    }

                    if(building.changes.Count != 0)
                    {
                        changesInstance = Instantiate(changesPrefab, buildingInstance.transform, false);
                        changesInstance.transform.localPosition = new Vector3(changesInstance.transform.localPosition.x, personSizeY + (commitInstance != null ? commitInstance.transform.localScale.y :0), changesInstance.transform.localPosition.z);
                        changesInstance.transform.localScale = new Vector3(changesInstance.transform.localScale.x, building.changes.Count/8f, changesInstance.transform.localScale.z);
                        var changesManager = changesInstance.transform.GetChild(0).GetComponent<ChangesManager>();
                        changesManager.SetBuilding(building);
                    }
                    if(building.commitedFiles.Count != 0)
                    {
                        filesInstance = Instantiate(filesPrefab, buildingInstance.transform, false);
                        filesInstance.transform.localPosition = new Vector3(filesInstance.transform.localPosition.x, personSizeY + (commitInstance != null ? commitInstance.transform.localScale.y : 0)  + (changesInstance != null ? changesInstance.transform.localScale.y : 0), filesInstance.transform.localPosition.z);
                        filesInstance.transform.localScale = new Vector3(filesInstance.transform.localScale.x, building.commitedFiles.Count/20f, filesInstance.transform.localScale.z);
                        var filesManager = filesInstance.transform.GetChild(0).GetComponent<CommitedFilesManager>();
                        filesManager.SetBuilding(building);
                    }
                }
            }

        }
    }

    public void CreateBuildings(List<Author> authors, List<DateTime> dates, bool showCommits, bool showChanges, bool showCommitedFiles)
    {
        foreach (var island in islands)
        {
            foreach (var atuhor in authors)
            {
                buildings.Add(new Building(atuhor, island.DateFrom.Value, island.DateTo.Value, showCommits, showChanges, showCommitedFiles));
            }
        }
    }

    public void CreatePowerLines(List<Ticket> tickets)
    {
        foreach (var ticket in tickets)
        {
            powerLines.Add(new PowerLine(ticket));
        }
    }

    public void RenderPowerLines()
    {
        var lastIsland = islands[islands.Count - 1];

        float x_end = lastIsland.islandInstance.gameObject.transform.GetChild(0).GetComponent<Renderer>().bounds.max.x;
        float y_line = 0;
        powerlinePrefab = (GameObject)Resources.Load("Prefabs/PowerLinePrefab", typeof(GameObject));

        foreach (var island in islands)
        {

            float x_start = island.islandInstance.gameObject.transform.GetChild(0).GetComponent<Renderer>().bounds.min.x;
            if (y_line == 0)
                y_line = islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.max.y + 3;
            float z_start = islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.max.z + 1;

            foreach (var powerline in powerLines.Where(powerline => powerline.show && powerline.ticket.created.Value >= island.DateFrom && powerline.ticket.created.Value <= island.DateTo))
            {
                powerlinePrefab.transform.position = new Vector3(x_start, y_line, z_start);
                var lineRenderer = powerlinePrefab.transform.GetChild(0).GetComponent<LineRenderer>();
                lineRenderer.SetPosition(0, new Vector3(x_start, y_line, z_start));

                foreach (var islandItem in islands)
                {
                    if (powerline.ticket.due >= islandItem.DateFrom && powerline.ticket.due.Value <= islandItem.DateTo)
                    {
                        x_end = islandItem.islandInstance.gameObject.transform.GetChild(0).GetComponent<Renderer>().bounds.max.x;
                        lineRenderer.SetPosition(1, new Vector3(x_end, y_line, z_start));
                    }
                }

                var powerLineInstance = Instantiate(powerlinePrefab);
                y_line += 0.2f;
            }
        }
    }

}