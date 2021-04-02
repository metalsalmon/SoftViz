using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DatasetDropdown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var dropdown = transform.GetComponent<Dropdown>();
        DirectoryInfo dir = new DirectoryInfo("Assets\\Resources\\data\\");
        FileInfo[] info = dir.GetFiles("*.json");
        List<string> items = info.Select(f => f.Name.Replace(".json", "")).ToList();

        foreach (var item in items)
        {
            dropdown.options.Add(new Dropdown.OptionData() { text = item });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
