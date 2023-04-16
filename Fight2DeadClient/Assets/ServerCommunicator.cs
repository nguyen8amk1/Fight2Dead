using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerCommunicator : MonoBehaviour
{
    // Start is called before the first frame update
    // TODO: 
    // cho nhan roomid, playerid 
    private string SERVER_IP;
    private const int PORT = 8080;
    private UdpClient serverSocket = new UdpClient();
    private int roomId, playerId; 

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
        while (true)
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(SERVER_IP), PORT);
            int listeningPort = ((IPEndPoint)serverSocket.Client.LocalEndPoint).Port;
            Debug.Log("Listening for server at port " + listeningPort);
            byte[] bytes = serverSocket.Receive(ref remoteEndPoint);
            string message = Encoding.ASCII.GetString(bytes);

            Debug.Log(message);
            if(message.StartsWith("rid:"))
			{
                // message format: rid:x,pid:x
                // TODO: get room id and player id 
                string[] idKeyPairs = message.Split(',');
                foreach (string s in idKeyPairs)
                {
                    string[] idNum = s.Split(':');
                    roomId = Int32.Parse(idNum[0]);
                    playerId = Int32.Parse(idNum[1]);
                }

				// TODO: pass that to the next scene as well 

                toNextScene();
                break;
			}
		}
    }

	private void toNextScene()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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


    // Update is called once per frame
    void Update()
    {
        
    }
}
