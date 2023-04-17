using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

// THIS GONNA BE A SINGLETON, kinda :v
// TODO: refactor all the server connection to this class  

public sealed class ServerConnection
{
    private static string SERVER_IP;
    private static int PORT = 8080; 
    private static UdpClient serverSocket;

    private static ServerConnection instance = null;
    private static readonly object padlock = new object();

    private ServerConnection()
	{
        SERVER_IP = getLocalIPAddress();
        serverSocket = new UdpClient();
	}

    public static ServerConnection Instance
	{
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new ServerConnection();
                }
                return instance;
            }
        }
    }

    // TODO: get put a callback inside 
	public string receiveMessage()
    {
		IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(SERVER_IP), PORT);
		int listeningPort = ((IPEndPoint)serverSocket.Client.LocalEndPoint).Port;
		Debug.Log("Listening for server at port " + listeningPort);
		byte[] bytes = serverSocket.Receive(ref remoteEndPoint);
		string message = Encoding.ASCII.GetString(bytes);

		Debug.Log(message);

        return message;
    }

    public void sendToServer(string message)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(message);
        serverSocket.Send(bytes, bytes.Length, SERVER_IP, PORT);
    }
    // Update is called once per frame

    public string getLocalIPAddress()
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


}
