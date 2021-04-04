using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    bool mainView = true;
    bool allowRotation = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    float inputX, inputZ;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - 20, transform.position.y, transform.position.z);
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
            mainView = false;
            transform.position = new Vector3(transform.position.x, 30, 0);
            transform.Rotate(new Vector3(83, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.E) && !mainView)
        {
            mainView = true;
            transform.position = new Vector3(transform.position.x, 4, -15);
            transform.Rotate(new Vector3(-83, 0, 0));
        }

        if(allowRotation)
        {
            transform.Rotate(Vector3.forward, 10.0f * Time.deltaTime);

        }

    }

}
