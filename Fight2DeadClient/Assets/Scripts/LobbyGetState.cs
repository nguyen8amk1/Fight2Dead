using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyGetState : MonoBehaviour
{
    private ServerConnection connection = ServerConnection.Instance;

    private bool ready = false;
    private bool opponentReady = false;
    private int count = 1;

    // TODO: change the name, it's confusing 
    // text component 
    public GameObject player1StatusTextObj;
    private TextMeshProUGUI player1StatusText;

    public GameObject player2StatusTextObj;
    private TextMeshProUGUI player2StatusText;
    private Thread listenToServerThread;
	private GameState globalGameState = GameState.Instance;

	public delegate void MessageHandlerLambda(string[] tokens);

	private void Start()
	{
        player1StatusText = player1StatusTextObj.GetComponent<TextMeshProUGUI>();
        player2StatusText = player2StatusTextObj.GetComponent<TextMeshProUGUI>();

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

        TextMeshProUGUI textMesh = null;       

        if(isPlayer1)
		{
            textMesh = player1StatusText;
		} else if(isPlayer2)
		{
            textMesh = player2StatusText;
		}

		if(ready) {
			changeStatus(textMesh, "Ready");
		} else
		{
			changeStatus(textMesh, "Not ready");
		}
	}

	private void changeStatus(TextMeshProUGUI textMesh, string status)
	{
        textMesh.text = status;
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

			TextMeshProUGUI textMesh = null;       

			if(isPlayer1)
			{
				textMesh = player2StatusText;
			} else if(isPlayer2)
			{
				textMesh = player1StatusText;
			}


            if(opponentReady)
			{
                changeStatus(textMesh, "Ready");
			}
            else
			{
                changeStatus(textMesh, "Not Ready");
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
        return  player1StatusText.text.Equals("Ready") && 
				player2StatusText.text.Equals("Ready");
	}
}
