using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    bool mainView = true;
    public UnityEngine.UI.Slider slider;

    // Start is called before the first frame update
    void Start()
    {

    }

    float inputX, inputZ;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && slider.value != slider.maxValue)
        {
            clearPanels();
            transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
            var overviewCamera = GameObject.Find("OverviewCamera");
            overviewCamera.transform.position = new Vector3(transform.position.x, overviewCamera.transform.position.y, overviewCamera.transform.position.z);
            slider.value += 1;
        }

        if (Input.GetKeyDown(KeyCode.A) && transform.position.x != 0)
        {
            clearPanels();
            transform.position = new Vector3(transform.position.x - 20, transform.position.y, transform.position.z);
            var overviewCamera = GameObject.Find("OverviewCamera");
            overviewCamera.transform.position = new Vector3(transform.position.x, overviewCamera.transform.position.y, overviewCamera.transform.position.z);
            slider.value -= 1;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z + 20);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z-20);
        }



        if (Input.GetKeyDown(KeyCode.Q) && mainView)
        {
            var overviewCamera = GameObject.Find("OverviewCamera");
            overviewCamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            overviewCamera.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
            mainView = false;
            transform.position = new Vector3(transform.position.x, 30, 0);
            transform.Rotate(new Vector3(83, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.E) && !mainView)
        {
            var overviewCamera = GameObject.Find("OverviewCamera");
            overviewCamera.transform.position = new Vector3(transform.position.x, 12, transform.position.z);
            overviewCamera.transform.rotation =  Quaternion.Euler(90, transform.rotation.y, transform.rotation.z);
            mainView = true;
            transform.position = new Vector3(transform.position.x, 4, -15);
            transform.Rotate(new Vector3(-83, 0, 0));
        }

    }

    public void clearPanels()
    {
        GameObject.Find("VisualizationManager").GetComponent<VisualizationManager>().ClearPanels();
    }
    public void OnSliderChange(Slider slider)
    {
        transform.position = new Vector3(20 * slider.value, transform.position.y, transform.position.z);
        var overviewCamera = GameObject.Find("OverviewCamera");

        overviewCamera.transform.position = new Vector3(transform.position.x, overviewCamera.transform.position.y, overviewCamera.transform.position.z);
    }
}
