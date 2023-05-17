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
	private GameObject hostPlayer; 

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
			string message = InGameMessageGenerator.tempInGameMessage(playerA.transform.position.x, playerA.transform.position.y);
			ServerCommute.connection.sendToServer(message);
			playerB.transform.position = new Vector3(globalGameState.playersPosition[1].x, globalGameState.playersPosition[1].y, 0);
		}

        if(globalGameState.PlayerId == 2)
		{
            //hostPlayer = playerB;
			string message = InGameMessageGenerator.tempInGameMessage(playerB.transform.position.x, playerB.transform.position.y);
			ServerCommute.connection.sendToServer(message);
			playerA.transform.position = new Vector3(globalGameState.playersPosition[0].x, globalGameState.playersPosition[0].y, 0);
		}
    }
}
