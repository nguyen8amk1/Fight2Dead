using SocketServer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerNetworkCommutor : MonoBehaviour
{
    // Start is called before the first frame update
    private GameState globalGameState = GameState.Instance;

    public GameObject playerA; // gaara 
    public GameObject playerB; // luffy 

    void Start()
    {
        globalGameState.playersPosition[0] = new Player(playerA.transform.position.x, playerA.transform.position.y); 
        globalGameState.playersPosition[1] = new Player(playerB.transform.position.x, playerB.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(globalGameState.PlayerId == 1)
		{
            //hostPlayer = playerA;
            //Debug.Log("player 1 sending to udp server");
            //int moveLeft = (globalGameState.player1MoveLeft) ? 1: 0;
            //int moveRight = (globalGameState.player1MoveRight) ? 1: 0;
			string message = InGameMessageGenerator.tempInGameMessage(playerA.transform.position.x, playerA.transform.position.y, globalGameState.player1State);
			ServerCommute.connection.sendToServer(message);
			playerB.transform.position = new Vector3(globalGameState.playersPosition[1].x, globalGameState.playersPosition[1].y, 0);
		}

        if(globalGameState.PlayerId == 2)
		{
            //Debug.Log("player 2 sending to udp server");
            //hostPlayer = playerB;
            //int moveLeft = 0;
            //int moveRight = (Player2.moveRight) ? 1: 0;
			string message = InGameMessageGenerator.tempInGameMessage(playerB.transform.position.x, playerB.transform.position.y, globalGameState.player2State);
			ServerCommute.connection.sendToServer(message);
			playerA.transform.position = new Vector3(globalGameState.playersPosition[0].x, globalGameState.playersPosition[0].y, 0);
		}
    }
}
