using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

        var panel = GameObject.Find("Commits");
        GameObject textTemplate = panel.transform.GetChild(0).gameObject;
        textTemplate.SetActive(true);

        var ContentTransform = GameObject.Find("CommitsContent").transform;

        foreach (Transform child in ContentTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (var commit in building.commits)
        {
            textTemplate.GetComponent<Text>().text = commit.created + "  :  " +commit.message;
            Instantiate(textTemplate, ContentTransform);
        }
        textTemplate.SetActive(false);

    }

    public void SetBuilding(Building building)
    {
        this.building = building;
    }
}
