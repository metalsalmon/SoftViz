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
        foreach (var file in building.commitedFiles)
            Debug.Log(file.action + ": " + file.file + "   :::  " + file.date);

        var panel = GameObject.Find("CommitedFiles");
        GameObject textTemplate = panel.transform.GetChild(0).gameObject;
        textTemplate.SetActive(true);

        var ContentTransform = GameObject.Find("CommitedFilesContent").transform;

        foreach (Transform child in ContentTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (var commitedFile in building.commitedFiles)
        {
            textTemplate.GetComponent<Text>().text = commitedFile.action + "  :  " + commitedFile.file + "  :  " + commitedFile.date;
            Instantiate(textTemplate, ContentTransform);
        }
        textTemplate.SetActive(false);
    }

    public void SetBuilding(Building building)
    {
        this.building = building;
    }
}
