using SocketServer;
using System;
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
	private float t = 0;

	void Start()
    {
    }

    // Update is called once per frame
    void Update()
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

        if(globalGameState.numPlayers == 2)
		{
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
					ServerCommute.connection.sendToServer(message);
				}

				// TODO: interpolate between frames for smoother movement 
				// how it should work:  
				// let's have 3 pos: start pos, end pos, new pos
				// start pos gonna lerp to endpos, if there is no new pos coming  (endpos == newpos) 
				// if there is new pos coming -> endpos = newpos,

				if(globalGameState.startPos != null && globalGameState.endPos != null)
				{
					Vector3 newPos = new Vector3(globalGameState.playersPosition[1].x, globalGameState.playersPosition[1].y, 0);

					if(compareVector3(globalGameState.endPos, newPos, 0.01f))
					{
					} else
					{
						globalGameState.startPos = globalGameState.endPos;
						globalGameState.endPos = newPos;
						t = 0;
					}

					Vector3 intermediatePos = Vector3.Lerp(globalGameState.startPos, globalGameState.endPos, t); 
					playerB.transform.position = intermediatePos;
					t += Time.deltaTime;	
				}
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
					ServerCommute.connection.sendToServer(message);
				}

				if(globalGameState.startPos != null && globalGameState.endPos != null)
				{
					Vector3 newPos = new Vector3(globalGameState.playersPosition[0].x, globalGameState.playersPosition[0].y, 0);

					if(compareVector3(globalGameState.endPos, newPos, 0.01f))
					{
					} else
					{
						globalGameState.startPos = globalGameState.endPos;
						globalGameState.endPos = newPos;
						t = 0;
					}

					Vector3 intermediatePos = Vector3.Lerp(globalGameState.startPos, globalGameState.endPos, t); 

					playerA.transform.position = intermediatePos;
					//playerA.transform.position = new Vector3(globalGameState.playersPosition[0].x, globalGameState.playersPosition[0].y, 0);
					t += Time.deltaTime;	
				}
			}

		} else if(globalGameState.numPlayers == 4)
		{
			if(globalGameState.currentTeam1Player == 1 && globalGameState.currentTeam2Player == 3)
			{
				if(globalGameState.PlayerId == 2 || globalGameState.PlayerId == 4)
				{
					playerA.transform.position = new Vector3(globalGameState.playersPosition[0].x, globalGameState.playersPosition[0].y, 0);
					playerB.transform.position = new Vector3(globalGameState.playersPosition[1].x, globalGameState.playersPosition[1].y, 0);
				}
			}
			if(globalGameState.currentTeam1Player == 2 && globalGameState.currentTeam2Player == 3)
			{
				if(globalGameState.PlayerId == 1 || globalGameState.PlayerId == 4)
				{
					playerA.transform.position = new Vector3(globalGameState.playersPosition[0].x, globalGameState.playersPosition[0].y, 0);
					playerB.transform.position = new Vector3(globalGameState.playersPosition[1].x, globalGameState.playersPosition[1].y, 0);
				}
			}
			if(globalGameState.currentTeam1Player == 1 && globalGameState.currentTeam2Player == 4)
			{
				if(globalGameState.PlayerId == 2 || globalGameState.PlayerId == 3)
				{
					playerA.transform.position = new Vector3(globalGameState.playersPosition[0].x, globalGameState.playersPosition[0].y, 0);
					playerB.transform.position = new Vector3(globalGameState.playersPosition[1].x, globalGameState.playersPosition[1].y, 0);
				}
			}
			if(globalGameState.currentTeam1Player == 2 && globalGameState.currentTeam2Player == 4)
			{
				if(globalGameState.PlayerId == 1 || globalGameState.PlayerId == 3)
				{
					playerA.transform.position = new Vector3(globalGameState.playersPosition[0].x, globalGameState.playersPosition[0].y, 0);
					playerB.transform.position = new Vector3(globalGameState.playersPosition[1].x, globalGameState.playersPosition[1].y, 0);
				}
			}

			if(globalGameState.PlayerId == globalGameState.currentTeam1Player)
			{
				if(globalGameState.onlineMode == "GLOBAL")
				{
					globalGameState.playerMessage = InGameMessageGenerator.tempInGameMessage(playerA.transform.position.x, playerA.transform.position.y, globalGameState.player1State, globalGameState.currentCharT1);

				} else if (globalGameState.onlineMode == "LAN")
				{
					string message = InGameMessageGenerator.tempInGameMessage(playerA.transform.position.x, playerA.transform.position.y, globalGameState.player1State, globalGameState.currentCharT1);
					ServerCommute.connection.sendToServer(message);
				}
				playerB.transform.position = new Vector3(globalGameState.playersPosition[1].x, globalGameState.playersPosition[1].y, 0);
			}

			if(globalGameState.PlayerId == globalGameState.currentTeam2Player)
			{
				//Debug.Log("player 2 sending to udp server");
				//hostPlayer = playerB;
				//int moveLeft = 0;
				//int moveRight = (Player2.moveRight) ? 1: 0;
				//player2State.text = $"Player2 State: {globalGameState.player2State}";
				if (globalGameState.onlineMode == "GLOBAL")
				{
					globalGameState.playerMessage = InGameMessageGenerator.tempInGameMessage(playerB.transform.position.x, playerB.transform.position.y, globalGameState.player2State, globalGameState.currentCharT2);
				} else if (globalGameState.onlineMode == "LAN")
				{
					string message = InGameMessageGenerator.tempInGameMessage(playerB.transform.position.x, playerB.transform.position.y, globalGameState.player2State, globalGameState.currentCharT2);
					ServerCommute.connection.sendToServer(message);
				}
				playerA.transform.position = new Vector3(globalGameState.playersPosition[0].x, globalGameState.playersPosition[0].y, 0);
			}
		}

		bool compareVector3(Vector3 a, Vector3 b, float epsilon)
		{
			return Vector3.Distance(a, b) <= epsilon;
		}

	}
}
