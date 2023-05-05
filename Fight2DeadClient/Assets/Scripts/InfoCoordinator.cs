using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

// TODO: CHANGE ITS NAME FIRST []
// TODO: UPDATE THIS CLASS TO WORK WITH THE NEW CONNECTION CLASS []

public class InfoCoordinator : MonoBehaviour
{
	// Start is called before the first frame update
	public float speed = 3;
    private float x = -100, y = -100;

    public GameObject player1;
    public GameObject player2;

	private int opid;

	private GameState globalGameState = GameState.Instance;

	private Thread listenToServerThread;

    // Start is called before the first frame update
    void Start()
    {
		// this is for room creation 
		if (globalGameState.PlayerId == 1)
		{
			player1.AddComponent(Type.GetType("ControlScript"));
		} 
		else if(globalGameState.PlayerId == 2)
		{
			player2.AddComponent(Type.GetType("ControlScript"));
		} 

        initListenToServerThread();
    }
	private void initListenToServerThread()
	{
		RoomMessageHandler.MessageHandlerLambda messageHandler = (string[] tokens) =>
		{
			// TODO: get the x, y
			// received format: "pid:{},x:{},y:{}"

			opid = Int32.Parse(Util.getValueFrom(tokens[0]));
			x = float.Parse(Util.getValueFrom(tokens[1]));
			y = float.Parse(Util.getValueFrom(tokens[2]));

		};

        listenToServerThread = new Thread(() => RoomMessageHandler.listenToServer(messageHandler));
        listenToServerThread.Start();
	}

	private void OnApplicationQuit()
	{
		RoomMessageHandler.sendCloseConnection();
	}


    void Update()
    {
        // this is for updating the position 
        if(opid == 1)
		{
            player1.transform.position = new Vector3(x, y, 0);
		}
        if(opid == 2)
		{
            player2.transform.position = new Vector3(x, y, 0); 
		}
    }
}
