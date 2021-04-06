using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        var personManager = transform.parent.parent.gameObject.transform.GetChild(0).GetComponent<PersonManager>();
        personManager.ClearPanels();
        personManager.HighlightPerson(building.name, building.id, building.roles);
        personManager.HighightTickets(building);

        foreach (var file in building.commitedFiles)
            Debug.Log(file.action + ": " + file.file + "   :::  " + file.date);

        GameObject.Find("WorkLabel").GetComponent<Text>().text = "Subory";
        var panel = GameObject.Find("DetailsPanel");
        GameObject textTemplate = panel.transform.GetChild(0).gameObject;
        textTemplate.SetActive(true);

        var ContentTransform = GameObject.Find("DetailsContent").transform;

        foreach (var commitedFile in building.commitedFiles)
        {
            textTemplate.GetComponent<Text>().text = commitedFile.action + "  :  " + commitedFile.file;
            Instantiate(textTemplate, ContentTransform);
        }
        textTemplate.SetActive(false);
    }

    public void SetBuilding(Building building)
    {
        this.building = building;
    }
}
