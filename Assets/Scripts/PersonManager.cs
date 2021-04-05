using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonManager : MonoBehaviour
{
    Building building;
    float H, S, V;
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
        Ticket ticket;

        var powerLines = GameObject.FindGameObjectsWithTag("PowerLine");
        foreach (GameObject obj in powerLines)
        {
            Color.RGBToHSV(obj.GetComponent<Renderer>().material.color, out H, out S, out V);
            var powerLineManager = obj.transform.GetComponent<PowerLineManager>();
            ticket = powerLineManager.getTicket();
            if (ticket.assignee == building.name)
            {
                obj.GetComponent<Renderer>().material.color = Color.HSVToRGB(H, 1, V);
                if(ticket.start >= building.dateFrom && ticket.start <= building.dateTo || ticket.due >= building.dateFrom && ticket.due <= building.dateTo || ( ticket.start < building.dateFrom && ticket.due > building.dateTo))
                    Debug.Log(ticket.start + " - " + ticket.due + " ::: " + ticket.name);
            }
            else
            {
                obj.GetComponent<Renderer>().material.color = Color.HSVToRGB(H, 0.1f, V);
            }

        }

    }

    public void SetBuilding(Building building)
    {
        this.building = building;
    }



}
