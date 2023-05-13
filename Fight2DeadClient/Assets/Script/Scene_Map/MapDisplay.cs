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
        Debug.Log("TODO: send quit message from map chose scene");
	}

	private void Update()
	{
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
