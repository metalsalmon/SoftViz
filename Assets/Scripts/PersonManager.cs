using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
        ClearPanels();
        HighlightPerson(building.name, building.id);
        HighightTickets(building);
    }

    public void SetBuilding(Building building)
    {
        this.building = building;
    }

    public void HighlightPerson(string name, int id)
    {
        var persons = GameObject.FindGameObjectsWithTag("Person");
        var author = GameObject.FindGameObjectWithTag("AuthorName");

        author.transform.GetComponent<Text>().text = name;

        foreach (var person in persons)
        {
            var personManager = person.transform.GetComponent<PersonManager>();
            if (personManager.building.id == id)
            {
                person.transform.GetComponent<Renderer>().material.color = Color.black;
            }
            else
            {
                person.transform.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }
    public void HighightTickets(Building building)
    {
        Ticket ticket;
        List<Ticket> tickets = new List<Ticket>();
        var powerLines = GameObject.FindGameObjectsWithTag("PowerLine");
        foreach (GameObject obj in powerLines)
        {
            Color.RGBToHSV(obj.GetComponent<Renderer>().material.color, out H, out S, out V);
            var powerLineManager = obj.transform.GetComponent<PowerLineManager>();
            ticket = powerLineManager.getTicket();
            if (building.tickets.Any(x => x.id == ticket.id))
            {
                obj.GetComponent<Renderer>().material.color = Color.HSVToRGB(H, 1, V);
                if (ticket.start >= building.dateFrom && ticket.start <= building.dateTo || ticket.due >= building.dateFrom && ticket.due <= building.dateTo || (ticket.start < building.dateFrom && ticket.due > building.dateTo))
                {
                    Debug.Log(ticket.assignee + " ::: " + ticket.start + " - " + ticket.due + " ::: " + ticket.name + " ::: " + ticket.id);
                    tickets.Add(ticket);
                }
            }
            else
            {
                obj.GetComponent<Renderer>().material.color = Color.HSVToRGB(H, 0.1f, V);
            }

        }
        showTickets(tickets);
    }

    public void showTickets(List<Ticket> tickets)
    {
        var panel = GameObject.Find("TicketsPanel");
        GameObject textTemplate = panel.transform.GetChild(0).gameObject;
        textTemplate.SetActive(true);
        var TicketsContent = GameObject.Find("TicketsContent").transform;

        foreach (var ticket in tickets)
        {
            textTemplate.GetComponent<Text>().text = "[" + ticket.type + "] " + ticket.name + "  cas : " + ticket.spent;
            Instantiate(textTemplate, TicketsContent);
        }
        textTemplate.SetActive(false);
    }

    public void ClearPanels()
    {
        var changesContent = GameObject.Find("DetailsContent").transform;
        var ticketsContent = GameObject.Find("TicketsContent").transform;

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
