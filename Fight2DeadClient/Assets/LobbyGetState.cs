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
    private bool opponentReady = false;
    private int count = 1;
	private void Start()
	{
        // listen to server 
        Thread listenToServerThread = new Thread(new ThreadStart(listenToServer));
        listenToServerThread.Start();
	}

	public void isClicked()
	{
        ready = !ready;
        string message = $"rid:{roomId},s:l,pid:{playerId},stat:{Convert.ToInt32(ready)}";
        connection.sendToServer(message);

        if(ready) {
            changeOwnStatus("Ready");
        } else
		{
            changeOwnStatus("Not ready");
		}
	}

	private void changeOwnStatus(string status)
	{
        // TODO: change the text on screen 
		Debug.Log("Own status " + status);
	}

    private void changeOtherStatus(string status)
	{
        // TODO: 
		Debug.Log("Others " + status);
	}

	private void listenToServer()
    {
        while (true)
        {
            string message = connection.receiveMessage();
            // nhan message tu server thi: "pid:{oppid},stat:{1}" 
            string[] tokens = message.Split(',');
            int pid = Int32.Parse(getValue(tokens[0]));
            int stat = Int32.Parse(getValue(tokens[1]));

            opponentReady = stat == 1;
            count = 0;
        }
    }

	private string getValue(string s)
	{
        return s.Split(':')[1];
	}

	// Update is called once per frame
	void Update()
    {
        if(count == 0)
		{
            if(opponentReady)
			{
                changeOtherStatus("Ready");
			}
            else
			{
                changeOtherStatus("Not Ready");
			}
            count = 1;
		}

        
    }
}
