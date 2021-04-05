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
        foreach (var change in building.changes)
            Debug.Log(change.changes + "   :::  " + change.created);

        var panel = GameObject.Find("Changes");
        GameObject textTemplate = panel.transform.GetChild(0).gameObject;
        textTemplate.SetActive(true);

        var ContentTransform = GameObject.Find("ChangesContent").transform;

        foreach (Transform child in ContentTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (var change in building.changes)
        {
            textTemplate.GetComponent<Text>().text = change.changes + "  :  " + change.comment;
            Instantiate(textTemplate, ContentTransform);
        }
        textTemplate.SetActive(false);


        var personManager = transform.parent.parent.gameObject.transform.GetChild(0).GetComponent<PersonManager>();
        personManager.HighlightPerson(building.name);
    }

    public void SetBuilding(Building building)
    {
        this.building = building;
    }
}
