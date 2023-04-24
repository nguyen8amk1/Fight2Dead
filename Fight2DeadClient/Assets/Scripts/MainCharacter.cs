using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MainCharacter : MonoBehaviour 
{
    public GameObject char0, char1, char2, char3;
    public static int selectVal = 0;

    private ServerConnection connection = ServerConnection.Instance;
    private Thread listenToServerThread;
    private GameState globalGameState = GameState.Instance;
    // Start is called before the first frame update

    // TODO: when the player close the game send a close connection message to the server 

    void Start()
    {
        // TODO: create a listening thread 
		listenToServerThread = new Thread(new ThreadStart(listenToServer));
		listenToServerThread.Start();
    }

	private void OnApplicationQuit()
	{
		// sending format:  "rid:{},s:q,pid:{}"
        string formatedMessage = $"rid:{globalGameState.RoomId},s:q,pid:{globalGameState.PlayerId}";
        connection.sendToServer(formatedMessage);
	}

	private void listenToServer()
	{
        while (true)
        {
            string message = connection.receiveMessage();
            // received format: "pid:{},s:q"
            string[] tokens = message.Split(',');
            int pid = Int32.Parse(Util.getValueFrom(tokens[1]));

            // TODO: DO SOMETHING ELSE HERE 
            Debug.Log($"Player with id {pid} has close the connection with the game");
        }
	}


	// Update is called once per frame
	void Update()
    {
        switch (selectVal)
        {
            case 0:
                char0.SetActive(true);
                char1.SetActive(false);
                char2.SetActive(false);
                char3.SetActive(false);
                break;
            case 1:
                char0.SetActive(false);
                char1.SetActive(true);
                char2.SetActive(false);
                char3.SetActive(false);
                break;
            case 2:
                char0.SetActive(false);
                char1.SetActive(false);
                char2.SetActive(true);
                char3.SetActive(false);
                break;
            case 3:
                char0.SetActive(false);
                char1.SetActive(false);
                char2.SetActive(false);
                char3.SetActive(true);
                break;
            default:
                break;

        }
    }
}
