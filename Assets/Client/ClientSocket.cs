using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using System;
using System.Net;

public class ClientSocket 
{
    TcpClient client;
    public string recievedMessage;
    byte[] buf = new byte[49142];
    static ClientSocket thisobject;

    // Start is called before the first frame update

    public string ipAddress = "127.0.0.1";
    public int port = 54010;
    public string lastRecievedMessage = "";
    public bool isConnected = false;

    public ClientSocket(string ipAddress ,int port)
    {
        this.ipAddress = ipAddress;
        this.port = port;
        thisobject = this;
        try
        {
            IPAddress ip = IPAddress.Parse(ipAddress);
            client = new TcpClient();
            client.Connect(ip, port);
            if (client.Connected) {
                isConnected = true;
            }
        }
        catch (Exception e)
        {
            Debug.Log("TcpClient Connection Exception "+e);
            lastRecievedMessage = "TcpClient Connection Exception";
        }
 
    }

    public static ClientSocket getSocket()
    {
        return thisobject;
    }

    public void GetQuote(string msgtoSend)
    {
        Debug.Log("asking to server for quotes");
        // get data
        // set up async read
        if (!client.Connected) {
            isConnected = false;
            return; // early out to stop the function the cleint if not connected 
        }
        isConnected = true;
        recievedMessage = "";
        var stream = client.GetStream();
        stream.BeginRead(buf, 0, buf.Length, Message_Received, null);

        // send message
        //byte[] msg = Encoding.ASCII.GetBytes("QUOTE");
        byte[] msg = Encoding.ASCII.GetBytes(msgtoSend);
        stream.Write(msg, 0, msg.Length);

    }


    public void OnDestroy()
    {
        if (client.Connected) 
        {
            client.Close();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!string.IsNullOrEmpty(recievedMessage))
        {
            //statusText.text = recievedMessage;
            lastRecievedMessage = recievedMessage;
            recievedMessage = "";
        }
        
    }

    public void wasStartClient()
    {
        //statusText.text = "hello client";
        //SceneManager.LoadScene("CubeClientScene",LoadSceneMode.Single);
    }

    public void close() {
        if (client.Connected)
        {
            client.Close();
        }
    }

    void Message_Received(IAsyncResult res)
    { 
        if(res.IsCompleted && client.Connected)
        {
            var stream = client.GetStream();
            int bytesIn = stream.EndRead(res);

            recievedMessage = Encoding.ASCII.GetString(buf, 0, bytesIn);
            
        }
    }
}
