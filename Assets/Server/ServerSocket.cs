using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ServerSocket :MonoBehaviour
{


    //TcpListener server;
    //TcpClient client;
    //IEnumerator doClient = null;

    //void Client_Connected(IAsyncResult res )
    //{
    //    Debug.Log("trying to get client");
    //     client = server.EndAcceptTcpClient(res);
    //    Debug.Log("ok got client");

    //}

    //private void Update()
    //{
    //    if(client != null && doClient == null)
    //    {
    //            doClient = DOClient();
    //        Debug.Log("update calling coroutine");
    //        StartCoroutine(doClient);
    //        Debug.Log("going out");

    //    }
    //}

    //IEnumerator DOClient()
    //{
    //    int bytesReceived = 0;

    //    byte[] buf = new byte[49152];
    //    Debug.Log("we are in  coroutine");
    //    Debug.Log("getting stream");
    //    var stream = client.GetStream();
    //    do
    //    {


    //        bytesReceived = stream.Read(buf, 0, buf.Length);
    //        if (bytesReceived > 0)
    //        {
    //            Debug.Log("Encoding buff");
    //            string msg = Encoding.ASCII.GetString(buf, 0, bytesReceived);
    //            if (msg == "QUOTE")
    //            {
    //                Debug.Log("in msg");
    //                byte[] quoteOut = Encoding.ASCII.GetBytes("I am from server");
    //                stream.Write(quoteOut, 0, quoteOut.Length);
    //            }
    //        }
    //        Debug.Log("are we returning");
    //        yield return null;

    //    } while (bytesReceived > 0);

    //    Debug.Log("resseting");
    //    doClient = null;
    //    client.Close();
    //    client = null;


    //}

    //void Start() 
    //{
    //    string ipAddress = "127.0.0.1";
    //    int port = 54010;
    //    Debug.Log("start");

    //    IPAddress ip = IPAddress.Parse(ipAddress);

    //    server = new TcpListener(IPAddress.Any, port);
    //    server.Start();

    //    Debug.Log("Waiting for clinet on ip: " + ipAddress + " port : " + port);

    //    server.BeginAcceptTcpClient(Client_Connected, null);

    //}

    //Quotes quotes;
    TcpListener server;
    TcpClient client;
    IEnumerator doClient = null;

    int counter = 1;


    public string ipAddress = "127.0.0.1";
    public int port = 54010;

    void Client_Connected(IAsyncResult res)
    {
        Debug.Log("i am in  callback");
        client = server.EndAcceptTcpClient(res);
    }

    void Update()
    {
        if (client != null && doClient == null)
        {
            doClient = DoClient();
            StartCoroutine(doClient);
        }
    }

    IEnumerator DoClient()
    {
        int bytesReceived = 0;
        byte[] buf = new byte[49152];
        var stream = client.GetStream();

        do
        {
            bytesReceived = stream.Read(buf, 0, buf.Length);
            if (bytesReceived > 0)
            {
                string msg = Encoding.ASCII.GetString(buf, 0, bytesReceived);
                if (msg == "QUOTE")
                {
                    byte[] quoteOut = Encoding.ASCII.GetBytes("I am server meow "+counter++);
                    Debug.Log("we sended msg"+counter);
                    stream.Write(quoteOut, 0, quoteOut.Length);
                }
            }

            yield return null;
        }
        while (bytesReceived > 0);
        Debug.Log("after do while loop ");
        // Reset everything to defaults
        doClient = null;
        client.Close();
        client = null;
        if (client == null) {
            Debug.Log("client is null");
        }

        Debug.Log("Waiting for new client on ip: " + ipAddress + " port : " + port);
        // Accept a new client
        server.BeginAcceptTcpClient(Client_Connected, null);
    }

    void Start()
    {
        Debug.Log("Started server");
        //quotes = new Quotes(wisdomFile);

        IPAddress ip = IPAddress.Parse(ipAddress);
        server = new TcpListener(IPAddress.Any, port);
        server.Start();

        Debug.Log("Waiting for client on ip: " + ipAddress + " port : " + port);
        server.BeginAcceptTcpClient(Client_Connected, null);
    }



}
