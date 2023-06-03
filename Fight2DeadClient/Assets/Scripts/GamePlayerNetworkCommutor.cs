using SocketServer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayerNetworkCommutor : MonoBehaviour
{
    // Start is called before the first frame update
    private GameState globalGameState = GameState.Instance;

    public GameObject playerA = null;
    public GameObject playerB = null;
    public static int count1 = 0;
    public static int count2 = 0;
    //public Text player1State;
    //public Text player2State;

	void Start()
    {
    }

	private void OnApplicationQuit()
	{
		if(globalGameState.onlineMode == "LAN")
		{
			GameUDPClient.Stop();
		}
	}

	// Update is called once per frame
	void Update()
    {
        if(globalGameState.numPlayers == 2)
		{
			if(globalGameState.camPlayer1 != null && globalGameState.camPlayer2 != null)
			{
				if(count1 == 0)
				{
					playerA = globalGameState.camPlayer1;
					globalGameState.playersPosition[0] = new Player(playerA.transform.position.x, playerA.transform.position.y);
					count1 = 1;
				} 
				if(count2 == 0)
				{
					playerB = globalGameState.camPlayer2;
					globalGameState.playersPosition[1] = new Player(playerB.transform.position.x, playerB.transform.position.y);
					count2 = 1; 
				}
			}

			if(globalGameState.PlayerId == 1)
			{
				//hostPlayer = playerA;
				//Debug.Log("player 1 sending to udp server");
				//int moveLeft = (globalGameState.player1MoveLeft) ? 1: 0;
				//int moveRight = (globalGameState.player1MoveRight) ? 1: 0;
				//player1State.text = $"Player1 State: {globalGameState.player1State}";
				if(globalGameState.onlineMode == "GLOBAL")
				{
					globalGameState.playerMessage = InGameMessageGenerator.tempInGameMessage(playerA.transform.position.x, playerA.transform.position.y, globalGameState.player1State, globalGameState.currentCharT1);

				} else if (globalGameState.onlineMode == "LAN")
				{
					string message = InGameMessageGenerator.tempInGameMessage(playerA.transform.position.x, playerA.transform.position.y, globalGameState.player1State, globalGameState.currentCharT1);
					//ServerCommute.connection.sendToServer(message);
					globalGameState.playerMessage = message;
				}
				playerB.transform.position = new Vector3(globalGameState.playersPosition[1].x, globalGameState.playersPosition[1].y, 0);
			}

			if(globalGameState.PlayerId == 2)
			{
				//Debug.Log("player 2 sending to udp server");
				//hostPlayer = playerB;
				//int moveLeft = 0;
				//int moveRight = (Player2.moveRight) ? 1: 0;
				//player2State.text = $"Player2 State: {globalGameState.player2State}";
				if(globalGameState.onlineMode == "GLOBAL")
				{
					globalGameState.playerMessage = InGameMessageGenerator.tempInGameMessage(playerB.transform.position.x, playerB.transform.position.y, globalGameState.player2State, globalGameState.currentCharT2);
				} else if (globalGameState.onlineMode == "LAN")
				{
					string message = InGameMessageGenerator.tempInGameMessage(playerB.transform.position.x, playerB.transform.position.y, globalGameState.player2State, globalGameState.currentCharT2);
					//ServerCommute.connection.sendToServer(message);
					globalGameState.playerMessage = message;
				}
				playerA.transform.position = new Vector3(globalGameState.playersPosition[0].x, globalGameState.playersPosition[0].y, 0);
			}

		}
    }
}
