using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyGetState : MonoBehaviour
{
    private ServerConnection connection = ServerConnection.Instance;

    private bool ready = false;
    private bool opponentReady = false;
    private int count = 1;

	// TODO: change the name, it's confusing 
	// text component 
	public Image player1ReadyImg;
	public Image player2ReadyImg;

	public Sprite readySprite;
	public Sprite notReadySprite;

    private Thread listenToServerThread;
	private GameState globalGameState = GameState.Instance;

	public delegate void MessageHandlerLambda(string[] tokens);

	private void Start()
	{
		player1ReadyImg.sprite = notReadySprite;
		player2ReadyImg.sprite = notReadySprite;

		initListenToServerThread();
	}

	private void initListenToServerThread()
	{
		RoomMessageHandler.MessageHandlerLambda messageHandler = (string[] tokens) =>
		{
			// TODO: put any process of the tokens here
			// received format: "pid:{oppid},stat:{1}" 

			int stat = Int32.Parse(getValue(tokens[1]));
			opponentReady = stat == 1;
			count = 0;
		};

        listenToServerThread = new Thread(() => RoomMessageHandler.listenToServer(messageHandler));
        listenToServerThread.Start();
	}

	private void OnApplicationQuit()
	{
		RoomMessageHandler.sendCloseConnection();
	}

	public void isClicked()
	{
        ready = !ready;

        RoomMessageHandler.sendLobbyMessage(ready);

        bool isPlayer1 = globalGameState.PlayerId == 1;
        bool isPlayer2 = globalGameState.PlayerId == 2;

		Image image = null;

        if(isPlayer1)
		{
			image = player1ReadyImg;
		} else if(isPlayer2)
		{
			image = player2ReadyImg;
		}

		if(ready) {
			changeStatus(image, true);
		} else
		{
			changeStatus(image, false);
		}
	}

	private void changeStatus(Image image, bool ready2Play)
	{
		if(ready2Play)
		{
			image.sprite = readySprite;
		} else
		{
			image.sprite = notReadySprite;
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
			bool isPlayer1 = globalGameState.PlayerId == 1;
			bool isPlayer2 = globalGameState.PlayerId == 2;

			Image image = null;

			if(isPlayer1)
			{
				image = player2ReadyImg;
			} else if(isPlayer2)
			{
				image = player1ReadyImg;
			}


            if(opponentReady)
			{
                changeStatus(image, true);
			}
            else
			{
                changeStatus(image, false);
			}
            count = 1;
		}

        if(allPlayerReady())
		{
            Util.toNextScene();
			listenToServerThread.Abort();
		}
        
    }

	private bool allPlayerReady()
	{
		return ready && opponentReady;
	}
}
