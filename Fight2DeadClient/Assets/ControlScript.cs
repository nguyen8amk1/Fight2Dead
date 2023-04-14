using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

public class ControlScript : MonoBehaviour
{
    public float speed = 3;
    private const string SERVER_IP = "192.168.162.212";
    private const int PORT = 8080;
    private const int listeningPort = 9000;
    private UdpClient serverSocket = new UdpClient();
    private int id = -1;
    private float x = -100, y = -100;

	// TODO: implement a selective control system of some kind 

	// Start is called before the first frame update
	void Start()
    {
        Debug.Log("Sending command to server");
        sendToServer("command:connect");

        Thread listenToServerThread = new Thread(new ThreadStart(listenToServer));
        listenToServerThread.Start();
    }

	private void listenToServer()
	{
        //int listeningPort = ((IPEndPoint)serverSocket.Client.LocalEndPoint).Port;
        // TODO: listen for server sending opponents position
        while (true)
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(SERVER_IP), PORT);
            int listeningPort = ((IPEndPoint)serverSocket.Client.LocalEndPoint).Port;
            Debug.Log("Listening for server at port " + listeningPort);
            byte[] bytes = serverSocket.Receive(ref remoteEndPoint);
            string message = Encoding.ASCII.GetString(bytes);

            // server return id
            if(message.StartsWith("id:"))
			{
                Debug.Log("Parse the id");
				string[] tokens = message.Split(':');
				id = Int32.Parse(tokens[1]);
                Debug.Log(id);
			}

            // server return pos
            Debug.Log(message);
            string pattern = @"^-?\d+(\.\d+)?,-?\d+(\.\d+)?$";
            if (Regex.IsMatch(message, pattern))
            {
				// extract x, y 
				string[] tokens = message.Split(',');
				x = float.Parse(tokens[0]);
				y = float.Parse(tokens[1]);
			}
        }
    }

	// Update is called once per frame
	void Update()
    {
        if(x != -100 && y != -100)
		{
            this.transform.position = new Vector3(x, y, 0);
		}

        // TODO: send position to the server
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            if (id > -1) { 
				string position = $"id:{id}:{this.transform.position.x},{this.transform.position.y}";
				sendToServer(position);
			}
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            if (id > -1) { 
				string position = $"id:{id}:{this.transform.position.x},{this.transform.position.y}";
				sendToServer(position);
			}
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
			if (id > -1)
			{

				string position = $"id:{id}:{this.transform.position.x},{this.transform.position.y}";
				sendToServer(position);
			}
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
			if (id > -1)
			{
				string position = $"id:{id}:{this.transform.position.x},{this.transform.position.y}";
				sendToServer(position);
			}
        }
    }

	private void sendToServer(string message)
	{
        byte[] bytes = Encoding.ASCII.GetBytes(message);
        serverSocket.Send(bytes, bytes.Length, SERVER_IP, PORT);
	}
}
