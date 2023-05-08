using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private Text mapName;
    [SerializeField] private Image mapImage;
    [SerializeField] private Button playButton;

    // TODO: send the message for map  
    // format: "rid:{roomId},stg:{},pid:{}"
    private GameState globalGameState = GameState.Instance;
    private Thread listenToServerThread;

    private bool allPlayersChosen = false;
    private bool otherPlayerMakeChoice = false;
    private bool hostPlayerMakeChoice = false;
       
	private void Start()
	{
        initListenToServerThread();
	}
	private void initListenToServerThread()
	{
		RoomMessageHandler.MessageHandlerLambda messageHandler = (string[] tokens) =>
		{
            // received format: 
            otherPlayerMakeChoice = true;
		};

        listenToServerThread = new Thread(() => RoomMessageHandler.listenToServer(messageHandler));
        listenToServerThread.Start();
	}

	private void OnApplicationQuit()
	{
        RoomMessageHandler.sendCloseConnection();
	}

	private void Update()
	{
		allPlayersChosen = hostPlayerMakeChoice && otherPlayerMakeChoice;
	    if(allPlayersChosen)
		{
            // TODO: to next scene 
            listenToServerThread.Abort();
            Util.toNextScene();
		}
	}

	public void DisplayMap(Map _newMap)
    {
        mapName.text = _newMap.mapName;
        mapImage.sprite = _newMap.mapImage;

        playButton.onClick.RemoveAllListeners();

        playButton.onClick.AddListener(() => {
            hostPlayerMakeChoice = true;
            RoomMessageHandler.sendChooseMapMessage(mapName.text);
        });
    }
}
