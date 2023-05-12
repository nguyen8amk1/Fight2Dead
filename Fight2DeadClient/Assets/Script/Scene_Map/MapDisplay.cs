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
    private GameState playerInfo = GameState.Instance;
    private Thread listenToServerThread;

    private bool allPlayersChosen = false;
    private bool otherPlayerMakeChoice = false;
    private bool hostPlayerMakeChoice = false;
       
	private void Start()
	{
        listenToServerThread = new Thread(new ThreadStart(listenToServer));
        listenToServerThread.Start();
	}

	private void OnApplicationQuit()
	{
        Debug.Log("TODO: send quit message from map chose scene");
	}

	private void listenToServer()
	{
        while(true)
		{

		}
	}

	private void Update()
	{
		allPlayersChosen = hostPlayerMakeChoice && otherPlayerMakeChoice;
	    if(allPlayersChosen)
		{
            // TODO: to next scene 
            Util.toNextScene();
            listenToServerThread.Abort();
            // Test only
            allPlayersChosen = false;
            hostPlayerMakeChoice = false;
		}
	}

	public void DisplayMap(Map _newMap)
    {
        mapName.text = _newMap.mapName;
        mapImage.sprite = _newMap.mapImage;

        playButton.onClick.RemoveAllListeners();

        playButton.onClick.AddListener(() => {
            hostPlayerMakeChoice = true;

            string message = $"rid:{playerInfo.RoomId},stg:{mapName.text},pid:{playerInfo.PlayerId}";

            Debug.Log($"TODO: Send this message to server: {message}");
        });
    }
}
