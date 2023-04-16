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
    private ServerConnection connection = ServerConnection.Instance;
    private int roomId, playerId;
    private bool tns = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Sending command to server");
        connection.sendToServer("command:connect");

        Thread listenToServerThread = new Thread(new ThreadStart(listenToServer));
        listenToServerThread.Start();
    }

    private void listenToServer()
    {
        while (true)
        {
            string message = connection.receiveMessage();
            if(message.StartsWith("rid:"))
			{
                // message format: rid:x,pid:x
                // TODO: get room id and player id 
                string[] idKeyPairs = message.Split(',');
				string[] ridPair = idKeyPairs[0].Split(':');
                string[] pidPair = idKeyPairs[1].Split(':');

				roomId = Int32.Parse(ridPair[1]);
				playerId = Int32.Parse(pidPair[1]);

                // TODO: pass roomid and playerid to the next scene as well 
                LobbyGetState.roomId = roomId;
                LobbyGetState.playerId = playerId;

                tns = true;   
                break;
			}
		}
    }

	private void toNextScene()
	{
	}

    // Update is called once per frame
    void Update()
    {
        if(tns)
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
