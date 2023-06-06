using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System;
using SocketServer;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private Text mapName;
    [SerializeField] private Image mapImage;
    [SerializeField] private Button playButton;

    // TODO: send the message for map  
    // format: "rid:{roomId},stg:{},pid:{}"
    private GameState globalGameState = GameState.Instance;

    private bool allPlayersChosen = false;

       
	private void Start()
	{
	}

	private void OnApplicationQuit()
	{
		Debug.Log("Send quit message from map choose scene");
		string quitMessage = PreGameMessageGenerator.quitMessage();
		ServerCommute.connection.sendToServer(quitMessage);
	}

	private void Update()
	{
		if (globalGameState.onlineMode.Equals("LAN"))
		{
			if (globalGameState.lobby_P1Quit)
			{
				Debug.Log("TODO: remove the P1 on screen");
			}

			if (globalGameState.lobby_P2Quit)
			{
				Debug.Log("TODO: remove the P2 on screen");
			}
		}
		else if (globalGameState.onlineMode.Equals("GLOBAL"))
		{
			if (globalGameState.lobby_P1Quit ||
				globalGameState.lobby_P2Quit)
			{
				Debug.Log("Go back to menu");
				Util.toSceneWithIndex(globalGameState.scenesOrder["MENU"]);
			}
		}

		allPlayersChosen = globalGameState.hostPlayerMapChosen && globalGameState.opponentMapChosen;
	    if(allPlayersChosen)
		{
            Util.toNextScene();
		}
	}

	public void DisplayMap(Map _newMap)
    {
        mapName.text = _newMap.mapName;
        mapImage.sprite = _newMap.mapImage;

        playButton.onClick.RemoveAllListeners();

        playButton.onClick.AddListener(() => {
            globalGameState.hostPlayerMapChosen = true;

            string message = PreGameMessageGenerator.chooseMapMessage(mapName.text);
            ServerCommute.connection.sendToServer(message);

            Debug.Log($"TODO: Send this message to server: {message}");
        });
    }
}
