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
    public List<(GameObject gameObject, string date)> islands = new List<(GameObject, string)>();
    public List<Building> buildings = new List<Building>();

    public void CreateIslands(List<string> dates)
    {
        float y = 0;
        float z = 0;
        float gap = 2;
        islandPrefab = (GameObject)Resources.Load("Prefabs/IslandPrefab", typeof(GameObject));

        float x_pos=0;
        foreach (var date in dates)
        {
            var island = Instantiate(islandPrefab, new Vector3(x_pos, z, y), Quaternion.identity);
            islands.Add((island, date));
            island.transform.GetChild(1).GetComponent<TextMesh>().text = date;
            
            x_pos += islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.size.x + gap;
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
            float IslandMinX = islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.min.x + 1;
            float IslandMinZ = islandPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.min.z + 3;

            foreach (var building in buildings.Where(building => building.show && building.date == island.date))
            {
                GameObject filesInstance = null;
                GameObject changesInstance = null;
                GameObject commitInstance = null;

                buildingPrefab.transform.position = new Vector3(IslandMinX, 0, 0);

                IslandMinX += buildingPrefab.transform.GetChild(0).GetComponent<Renderer>().bounds.size.x + 0.2f;
                var buildingInstance = Instantiate(buildingPrefab, island.gameObject.transform, false);
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

    public void CreateBuildings(List<Author> authors, List<string> dates)
    {
        foreach (var date in dates)
        {
            foreach (var author in authors)
            {
                buildings.Add(new Building(author, date));
            }
        }
    }


}