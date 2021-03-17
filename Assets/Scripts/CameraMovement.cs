using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float inputX, inputZ;
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");

        if(inputX != 0)
        {
            move();
        }
    }

    private void move()
    {
        transform.position += transform.right * inputX * 5 ;
    }
}
