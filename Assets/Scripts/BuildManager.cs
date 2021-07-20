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
    public bool showAll;

    public int CreateIslands(List<DateTime> dates, int range, bool showAll)
    {
        this.showAll = showAll;
        islandPrefab = (GameObject)Resources.Load("Prefabs/IslandPrefab", typeof(GameObject));
        DateTime dateTo = DateTime.MinValue;
        float gap = 2;
        float x = 0;
        float y = 0;
        float z = 0;
        var position = 0;
        DateTime StartDate = dates[0];
        bool AddIsland;
        short index = 0;

        while(StartDate <= dates.Last())
        {
            islands.Add(new Island(++index, StartDate, StartDate.AddDays(range - 1)));
            StartDate = StartDate.AddDays(range);
        }

        foreach (var island in islands)
        {
            island.positionX = x;
            if (showAll)
            {
                island.show = true;
                var islandInstance = Instantiate(islandPrefab, new Vector3(x, z, y), Quaternion.identity);
                islandInstance.transform.GetChild(1).GetComponent<TextMesh>().text = island.index + " : " + island.DateFrom.Value.ToString("dd.MM.yyyy") + " - " + island.DateTo.Value.ToString("dd.MM.yyyy");
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
                            islandInstance.transform.GetChild(1).GetComponent<TextMesh>().text = island.index + " : " + island.DateFrom.Value.ToString("dd.MM.yyyy") + " - " + island.DateTo.Value.ToString("dd.MM.yyyy");
                            island.islandInstance = islandInstance;
                            x += islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.size.x + gap;
                        }
                        island.AddDate(date);
                        AddIsland = false;
                        island.show = true;
                        island.position = position++;
                    }
                    else
                    {
                        island.AddDate(date);
                    }
                }
            }
        }

        return islands.Where(island => island.show).Count();
    }

    public void RenderBuildings()
    {
        buildingPrefab = (GameObject)Resources.Load("Prefabs/BuildingPrefab", typeof(GameObject));
        commitsPrefab = (GameObject)Resources.Load("Prefabs/CommitsPrefab", typeof(GameObject));
        filesPrefab = (GameObject)Resources.Load("Prefabs/FilesPrefab", typeof(GameObject));
        changesPrefab = (GameObject)Resources.Load("Prefabs/ChangesPrefab", typeof(GameObject));
        var personSizeY = buildingPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.size.y;
        List<Building> islandBuildings = new List<Building>();
        float minSpentTime = buildings.Select(building => building.timeSpent).Min();
        float maxSpentTime = buildings.Where(building => building.name != "unknown").Select(building => building.timeSpent).Max();

        foreach (var island in islands)
        {
            if (island.show)
            {
                var islandManager = island.islandInstance.transform.GetChild(0).GetComponent<IslandManager>();
                islandManager.setIsland(island);
                float IslandMinX = islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.min.x + 1;
                float IslandMinZ = islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.min.z + 3;
                float IslandMaxZ = islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.max.z - 2;


                //order by commits count
                islandBuildings = buildings.Where(building => building.show && building.dateFrom == island.DateFrom).OrderBy(building => building.commitsCount).ToList();

                float buildingsGap = (islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.size.x) / islandBuildings.Count();

                foreach (var building in islandBuildings)
                {
                    GameObject filesInstance = null;
                    GameObject changesInstance = null;
                    GameObject commitInstance = null;

                    buildingPrefab.transform.position = new Vector3(IslandMinX, 0, 0);
                
                    IslandMinX += buildingsGap;
                    var buildingInstance = Instantiate(buildingPrefab, island.islandInstance.gameObject.transform, false);
                    buildingInstance.transform.localPosition = new Vector3(buildingInstance.transform.localPosition.x, buildingInstance.transform.localPosition.y, Scale(building.timeSpent, minSpentTime, maxSpentTime, IslandMinZ, IslandMaxZ));
                    var personManager = buildingInstance.transform.GetChild(0).GetComponent<PersonManager>();
                    personManager.SetBuilding(building);

                    if (building.commits.Count != 0)
                    {
                        commitInstance = Instantiate(commitsPrefab, buildingInstance.transform, false);
                        commitInstance.transform.localPosition = new Vector3(commitInstance.transform.localPosition.x, commitInstance.transform.localPosition.y + personSizeY, commitInstance.transform.localPosition.z);
                        commitInstance.transform.localScale = new Vector3(commitInstance.transform.localScale.x, building.commits.Count/5f, commitInstance.transform.localScale.z);
                        var commitManager = commitInstance.transform.GetChild(0).GetComponent<CommitsManager>();
                        commitManager.SetBuilding(building);
                    }

                    if(building.changes.Count != 0)
                    {
                        changesInstance = Instantiate(changesPrefab, buildingInstance.transform, false);
                        changesInstance.transform.localPosition = new Vector3(changesInstance.transform.localPosition.x, personSizeY + (commitInstance != null ? commitInstance.transform.localScale.y :0), changesInstance.transform.localPosition.z);
                        changesInstance.transform.localScale = new Vector3(changesInstance.transform.localScale.x, building.changes.Count/20f, changesInstance.transform.localScale.z);
                        var changesManager = changesInstance.transform.GetChild(0).GetComponent<ChangesManager>();
                        changesManager.SetBuilding(building);
                    }
                    if(building.commitedFiles.Count != 0)
                    {
                        filesInstance = Instantiate(filesPrefab, buildingInstance.transform, false);
                        filesInstance.transform.localPosition = new Vector3(filesInstance.transform.localPosition.x, personSizeY + (commitInstance != null ? commitInstance.transform.localScale.y : 0)  + (changesInstance != null ? changesInstance.transform.localScale.y : 0), filesInstance.transform.localPosition.z);
                        filesInstance.transform.localScale = new Vector3(filesInstance.transform.localScale.x, building.commitedFiles.Count/30f, filesInstance.transform.localScale.z);
                        var filesManager = filesInstance.transform.GetChild(0).GetComponent<CommitedFilesManager>();
                        filesManager.SetBuilding(building);
                    }
                }
            }

        }
    }

    private float Scale(float value, float min, float max, float minScale, float maxScale)
    {
        float scaled = minScale + (float)(value - min) / (max - min) * (maxScale - minScale);
        return scaled;
    }

    public void CreateBuildings(List<Author> authors, List<DateTime> dates, bool showCommits, bool showChanges, bool showCommitedFiles)
    {
        foreach (var island in islands)
        {
            foreach (var author in authors)
            {
                buildings.Add(new Building(author, island.DateFrom.Value, island.DateTo.Value, showCommits, showChanges, showCommitedFiles));
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

        var lastIsland = islands.Where(island => island.show).Last();
        float posY = islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.max.y + 3.5f;
        float x_end;
        float z_start = islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.max.z + 1;
        powerlinePrefab = (GameObject)Resources.Load("Prefabs/PowerLinePrefab", typeof(GameObject));
        List<(float posX, float posY)> positions = new List<(float posX, float posY)>();
        var lineRenderer = powerlinePrefab.transform.GetChild(0).GetComponent<LineRenderer>();

        foreach (var powerline in powerLines)
        {
            positions.Add((int.MinValue, posY));
            posY += 0.35f;
        }

        foreach (var island in islands)
        {
            if (!showAll && !island.show)
                continue;

            float x_start = island.islandInstance.gameObject.transform.GetChild(0).GetComponent<Renderer>().bounds.min.x;      
            
            foreach (var powerline in powerLines.Where(powerline => powerline.show && powerline.ticket.assignee != "unknown" && powerline.ticket.due != null &&
                                                       powerline.ticket.start.Value.Date >= island.DateFrom.Value.Date && powerline.ticket.start.Value.Date <= island.DateTo.Value.Date))
            {

                var endIsland = islands.FirstOrDefault(island => island.dates.Contains(powerline.ticket.due.Value));
                if (endIsland == null)
                {
                    endIsland = lastIsland;
                }
                x_end = endIsland.islandInstance.gameObject.transform.GetChild(0).GetComponent<Renderer>().bounds.max.x;

                //find the first empty position for the powerline
                for(int i=0; i< positions.Count; i++)
                {
                    if(positions[i].posX < x_start && positions[i].posX < x_end)
                    {
                        posY = positions[i].posY;
                        lineRenderer.SetPosition(0, new Vector3(x_start, posY, z_start));
                        lineRenderer.SetPosition(1, new Vector3(x_end, posY, z_start));
                        positions[i] = (x_end, posY);
                        break;
                    }
                }

                var powerLineInstance = Instantiate(powerlinePrefab);
                powerLineInstance.transform.GetChild(0).GetComponent<Renderer>().material.color = PowerLineColor(powerline.ticket.type);
                var powerLineManager = powerLineInstance.transform.GetChild(0).GetComponent<PowerLineManager>();
                powerLineManager.setTicket(powerline.ticket);
            }
        }
    }

    public Color PowerLineColor(string type)
    {
        Color color;
        switch (type)
        {
            case "Task":
                color = Color.HSVToRGB(0.2f, 0.1f, 1);
                break;
            case "Bug":
                color = Color.HSVToRGB(1, 0.1f, 1);
                break;
            case "Feature":
                color = Color.HSVToRGB(0.65f, 0.1f, 1);
                break;
            case "Enhancement":
                color = Color.HSVToRGB(0.65f, 0.1f, 1);
                break;
            default:
                color = Color.HSVToRGB(0.8f, 0.1f, 1);
                break;
        }
        return color;
    }

}