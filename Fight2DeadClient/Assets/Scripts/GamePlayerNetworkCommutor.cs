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
            int moveLeft = (Player1.moveLeft) ? 1: 0;
            int moveRight = (Player1.moveRight) ? 1: 0;
			string message = InGameMessageGenerator.tempInGameMessage(playerA.transform.position.x, playerA.transform.position.y, 1, moveLeft, moveRight);
			ServerCommute.connection.sendToServer(message);
			playerB.transform.position = new Vector3(globalGameState.playersPosition[1].x, globalGameState.playersPosition[1].y, 0);
		}

        if(globalGameState.PlayerId == 2)
		{
            //Debug.Log("player 2 sending to udp server");
            //hostPlayer = playerB;
            int moveLeft = (Player2.moveLeft) ? 1: 0;
            int moveRight = (Player2.moveRight) ? 1: 0;
			string message = InGameMessageGenerator.tempInGameMessage(playerB.transform.position.x, playerB.transform.position.y, 2, moveLeft, moveRight);
			ServerCommute.connection.sendToServer(message);
			playerA.transform.position = new Vector3(globalGameState.playersPosition[0].x, globalGameState.playersPosition[0].y, 0);
		}
    }
}
