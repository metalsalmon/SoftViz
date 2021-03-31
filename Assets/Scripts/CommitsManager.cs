using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommitsManager : MonoBehaviour
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
        foreach(var commit in building.commits)
            Debug.Log(commit.message + "   :::  " + commit.created);

    }

    public void SetBuilding(Building building)
    {
        this.building = building;
    }
}
