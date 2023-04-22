using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

public class InfoCoordinator : MonoBehaviour
{
    // TODO: refactor to state machine 
    // the states are: 

    // TODOO: add roomid into string;

    // Start is called before the first frame update
    public float speed = 3;
    private string SERVER_IP;
    private const int PORT = 8080;
    private UdpClient serverSocket = new UdpClient();
    private int id = -1;
    private float x = -100, y = -100;
    public GameObject player1;
    public GameObject player2;

    private int whatPlayer = 0;
    private int whatPlayerPos = 0;

    // Start is called before the first frame update
    void Start()
    {
		SERVER_IP = getLocalIPAddress();
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

            // TODO: get room id 
            // server return id
            // phai vao dc phong thi moi co id 
            if (message.StartsWith("rid:"))
            {
                Debug.Log("Parse the id");
                string[] tokens = message.Split(':');
                id = Int32.Parse(tokens[1]);
                Debug.Log(id);
                whatPlayer = id;
            }

            // server return pos
            // TODO: change this to make this work again 
            Debug.Log(message);
            string pattern = @"^-?\d+(\.\d+)?,-?\d+(\.\d+)?$";
            if (Regex.IsMatch(message, pattern))
            {
                // extract x, y 
                string[] tokens = message.Split(',');
                x = float.Parse(tokens[0]);
                y = float.Parse(tokens[1]);

                // TODO: set the x, y of opponent player object 
                if(id == 1)
				{
                    whatPlayerPos = 2;
				}
                if(id == 2)
				{
                    whatPlayerPos = 1;
				}
            }
        }
    }

	private void sendToServer(string message)
	{
        byte[] bytes = Encoding.ASCII.GetBytes(message);
        serverSocket.Send(bytes, bytes.Length, SERVER_IP, PORT);
	}
    // Update is called once per frame

    private string getLocalIPAddress()
    {
        {
            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }
    }

    void Update()
    {
        // this is for room creation 
        if(whatPlayer == 1)
		{
            player1.AddComponent(Type.GetType("ControlScript"));
            player1.GetComponent<ControlScript>().id = id;
            player1.GetComponent<ControlScript>().serverSocket = serverSocket;
            player1.GetComponent<ControlScript>().SERVER_IP = SERVER_IP;
            whatPlayer = 0;
		} 
        else if(whatPlayer == 2)
		{
            player2.AddComponent(Type.GetType("ControlScript"));
            player2.GetComponent<ControlScript>().id = id;
            player2.GetComponent<ControlScript>().serverSocket = serverSocket;
            player2.GetComponent<ControlScript>().SERVER_IP = SERVER_IP;
            whatPlayer = 0; 
		} 

        // this is for updating the position 
        if(whatPlayerPos == 1)
		{
            player1.transform.position = new Vector3(x, y, 0); 
		}
        if(whatPlayerPos == 2)
		{
            player2.transform.position = new Vector3(x, y, 0); 
		}
    }
}
