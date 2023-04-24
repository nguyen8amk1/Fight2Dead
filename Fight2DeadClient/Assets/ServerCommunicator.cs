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
    private ServerConnection connection = ServerConnection.Instance;
    private int roomId, playerId;
    private bool tns = false;
    private GameState gameState = GameState.Instance;
    private Thread listenToServerThread;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Sending command to server");
        connection.sendToServer("command:connect");

        listenToServerThread = new Thread(new ThreadStart(listenToServer));
        listenToServerThread.Start();
    }
	private void OnApplicationQuit()
	{
        string quitMessage = "quit"; 
        connection.sendToServer(quitMessage);
	}

	private void listenToServer()
    {
        while (true)
        {
            string message = connection.receiveMessage();
            if(message.StartsWith("rid:"))
			{
                // message format: rid:x,pid:x
                string[] idKeyPairs = message.Split(',');
				string[] ridPair = idKeyPairs[0].Split(':');
                string[] pidPair = idKeyPairs[1].Split(':');

				gameState.RoomId = Int32.Parse(ridPair[1]);
				gameState.PlayerId = Int32.Parse(pidPair[1]);

                tns = true;   
                break;
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (tns)
		{
            Util.toNextScene();
            listenToServerThread.Abort();
		}

    }
}
