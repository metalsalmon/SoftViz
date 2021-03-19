using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommitedFilesManager : MonoBehaviour
{
    Building building;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown()
    {
        foreach (var file in building.commitedFiles)
            Debug.Log(file.action + ": " + file.file) ;
    }

    public void SetBuilding(Building building)
    {
        this.building = building;
    }
}
