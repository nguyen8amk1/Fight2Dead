using SocketServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MatchingSceneMessageHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private bool tns = false;
    private GameState gameState = GameState.Instance;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Sending establish new connection message to server");
        string numPlayersMessage = PreGameMessageGenerator.matchingSceneMessage(gameState.numPlayers);
        ServerCommute.connection.sendToServer(numPlayersMessage);
    }

	private void OnApplicationQuit()
	{
        Debug.Log("Matching scene close");
	}

    // Update is called once per frame
    void Update()
    {
        if(gameState.receiveRidPid)
		{
			tns = true;
            gameState.receiveRidPid = false; 
		}

        if (tns)
		{
            Util.toNextScene();
		}

    }
}
