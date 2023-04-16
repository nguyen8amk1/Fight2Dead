using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class LobbyGetState : MonoBehaviour
{
    private ServerConnection connection = ServerConnection.Instance;

    // this is the text on the screen, will be changed 
    public string player1Status;
    public string player2Status;
    public static int roomId, playerId;
    private bool ready = false;

    public void isClicked()
	{
        ready = !ready;
        string message = $"rid:{roomId},pid:{playerId},s:l,stat:{Convert.ToInt32(ready)}";
        connection.sendToServer(message);
        // listen to server 
        Thread listenToServerThread = new Thread(new ThreadStart(listenToServer));
        listenToServerThread.Start();


        if(ready) {
            changePlayer1Status("Ready");
        } else
		{
            changePlayer1Status("Not ready");
		}
	}

	private void changePlayer1Status(string status)
	{
        // TODO: 
		Debug.Log(status);
	}

	private void listenToServer()
    {
        while (true)
        {
            string message = connection.receiveMessage();
			// TODO: nhan message tu server thi: "pid:{oppid},1" 
        }
    }

    // Update is called once per frame
    void Update()
    {

        
    }
}
