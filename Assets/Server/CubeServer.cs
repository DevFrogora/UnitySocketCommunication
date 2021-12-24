using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeServer : MonoBehaviour
{
    bool cubeClicked = false;
    void Start()
    {
        cubeClicked = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeOnClick()
    {
        Debug.Log("Cube Server got clicked");
        //Debug.Log("cube called");
        if (!cubeClicked)
        {
            Debug.Log("On");
            cubeClicked = true;
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            Debug.Log("OFF");
            GetComponent<Renderer>().material.color = Color.white;
            cubeClicked = false;
        }
    }
}
