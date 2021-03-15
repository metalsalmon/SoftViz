using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationManager : MonoBehaviour
{
    JsonReader jsonReader = new JsonReader();

    // Start is called before the first frame update
    void Start()
    {

        jsonReader.LoadData("aswi2017vana");

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
