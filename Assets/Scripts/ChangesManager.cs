using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangesManager : MonoBehaviour
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
        foreach (var change in building.changes)
            Debug.Log(change.changes + "   :::  " + change.created);
    }

    public void SetBuilding(Building building)
    {
        this.building = building;
    }
}
