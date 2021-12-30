using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    Text statusText;
    ClientSocket clientSocket;
    string ipAddress = "127.0.0.1";
    int port = 54010;

    InputField inputFieldServerIP;
    InputField inputFieldServerPort;

    void Start()
    {
        statusText = GameObject.Find("SocketStatusText").GetComponent<Text>();

        inputFieldServerIP = GameObject.Find("InputFieldServerIP").GetComponent<InputField>();
        inputFieldServerPort = GameObject.Find("InputFieldServerPort").GetComponent<InputField>();

        inputFieldServerIP.text = ipAddress;
        inputFieldServerPort.text = port.ToString();


    }

    // Update is called once per frame
    void Update()
    {
        if (clientSocket == null) return;
        if (!string.IsNullOrEmpty(clientSocket.recievedMessage))
        {
            Debug.Log("We recieved some msg from server");
            //statusText.text = recievedMessage;
            clientSocket.lastRecievedMessage = clientSocket.recievedMessage;
            statusText.text = statusText.text + clientSocket.lastRecievedMessage;
            clientSocket.recievedMessage = "";
            //clientSocket.close();
        }
    }

    public void StartClient(string level)
    {
        
        Debug.Log(inputFieldServerIP.text);
        Debug.Log(inputFieldServerPort.text);

        //ipAddress = inputFieldServerIP.text;
        //port = int.Parse(inputFieldServerPort.text);
        

        if (clientSocket == null)
        {
            clientSocket = new ClientSocket(ipAddress, port);
            
            statusText.text = "clientSocket null ";
            if (clientSocket.isConnected)
            {
                statusText.text = "Connected";
                clientSocket.GetQuote("QUOTE");
            }
            else {
                statusText.text = "Client not connected "+clientSocket.lastRecievedMessage;
            }
        }
        else {
            if (clientSocket.isConnected)
            {
                clientSocket.GetQuote("QUOTE");
                statusText.text = "Okay its connected " + clientSocket.lastRecievedMessage;
            }
            else {
                statusText.text = "Client not connected "+clientSocket.lastRecievedMessage;
            }
            
        }
    }

    public void StartServer()
    {
        //ipAddress = inputFieldServerIP.text;
        //port = int.Parse(inputFieldServerPort.text);


        //statusText.text = "hello server";
        SceneManager.LoadScene("CubeServerScene", LoadSceneMode.Single);

    }


    private void OnDestroy()
    {
        if (clientSocket == null) return;
        clientSocket.OnDestroy();
    }
}
