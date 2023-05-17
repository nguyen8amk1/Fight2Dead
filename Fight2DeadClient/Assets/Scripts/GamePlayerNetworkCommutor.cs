using SocketServer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerNetworkCommutor : MonoBehaviour
{
    // Start is called before the first frame update
    private GameState globalGameState = GameState.Instance;

    public GameObject playerA; 
    public GameObject playerB; 
	private GameObject hostPlayer; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: determine who is player 1 and who is player 2 
        // because there is a problem where the other player get locked at 0 0  
        // -> figure out how to sync the position of the two 

        if(globalGameState.PlayerId == 1)
		{
            //hostPlayer = playerA;
			string message = InGameMessageGenerator.tempInGameMessage(playerA.transform.position.x, playerA.transform.position.y);
			ServerCommute.connection.sendToServer(message);
			playerA.transform.position = new Vector3(globalGameState.playersPosition[1].x, globalGameState.playersPosition[1].y, 0);
		}
        if(globalGameState.PlayerId == 2)
		{
            //hostPlayer = playerB;
			string message = InGameMessageGenerator.tempInGameMessage(playerB.transform.position.x, playerB.transform.position.y);
			ServerCommute.connection.sendToServer(message);
			playerB.transform.position = new Vector3(globalGameState.playersPosition[0].x, globalGameState.playersPosition[0].y, 0);
		}
    }
}
