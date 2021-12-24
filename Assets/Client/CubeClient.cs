using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class CubeClient : MonoBehaviour
{
    ClientSocket clientSocket;

    bool cubeClicked = false;
    void Start()
    {
        cubeClicked = false;
        //clientSocket = new ClientSocket();

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void changeOnClick(ClientSocket clientSocket)
    {
        this.clientSocket = clientSocket;
        Debug.Log("Cube Client got clicked");
        //Debug.Log("cube called");
        if (clientSocket.isConnected) {
            if (!cubeClicked)
            {
                Debug.Log("MSG: On True "+clientSocket.lastRecievedMessage);
                cubeClicked = true;
                GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.white;
                cubeClicked = false;
            }
        }

    }


}
