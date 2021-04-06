using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        var personManager = transform.parent.parent.gameObject.transform.GetChild(0).GetComponent<PersonManager>();
        personManager.ClearPanels();
        personManager.HighlightPerson(building.name, building.id, building.roles);
        personManager.HighightTickets(building);

        foreach (var change in building.changes)
            Debug.Log(change.changes + "   :::  " + change.created);

        GameObject.Find("WorkLabel").GetComponent<Text>().text = "Manazment";

        var panel = GameObject.Find("DetailsPanel");
        GameObject textTemplate = panel.transform.GetChild(0).gameObject;
        textTemplate.SetActive(true);

        var ContentTransform = GameObject.Find("DetailsContent").transform;

        foreach (var change in building.changes)
        {
            textTemplate.GetComponent<Text>().text = change.changes + "  :  " + change.comment;
            Instantiate(textTemplate, ContentTransform);
        }
        textTemplate.SetActive(false);
    }

    public void SetBuilding(Building building)
    {
        this.building = building;
    }
}
