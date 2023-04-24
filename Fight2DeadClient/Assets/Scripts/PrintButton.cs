using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class PrintButton : MonoBehaviour
{
    // TODO: gui thong tin nhan vat cho server 
    // format: "rid:{roomId},pid:{pid},ch:{CharacterName}"
    private GameState gameState = GameState.Instance;
    private ServerConnection connection = ServerConnection.Instance;

    string[] charName = new string[] { "Ryu", "Four Arms", "Heatblast", "Venom" };
    private Thread listenToServerThread; 

	private void Start()
	{
        // init the thread 		
        listenToServerThread = new Thread(new ThreadStart(listenToServer));
        listenToServerThread.Start();
	}

	private void listenToServer()
	{
        while(true)
		{
            // TODO: what if the player choose another character or something, how should we handle that ??
            // because the message gonna ge sent again, and the list is added as well 

            //format: "pid:{0},cn:{1}", opid, charName
            string message = connection.receiveMessage();

            string[] tokens = message.Split(',');

            int pid = Int32.Parse(Util.getValueFrom(tokens[0]));
            string characterName = Util.getValueFrom(tokens[1]);

            // pid -1 = index 
            // luu character name vao game state 
            int index = pid - 1;
            gameState.addCharacterName(index, characterName);
		}
	}

	private bool allPlayerReady()
	{
        return gameState.charNameCount >= 2;
	}

	public void OnButtonClick()
    {
        if (MainCharacter.selectVal == 0)
            chosenCharacter=(charName[0]);
        else if (MainCharacter.selectVal == 1)
            chosenCharacter=(charName[1]);
        else if (MainCharacter.selectVal == 2)
            chosenCharacter=(charName[2]);
        else chosenCharacter=(charName[3]);

        string message = $"rid:{gameState.RoomId},s:ch,pid:{gameState.PlayerId},ch:{chosenCharacter}";
        Debug.Log(message);
        connection.sendToServer(message);

		int index = gameState.PlayerId - 1;
		gameState.addCharacterName(index, chosenCharacter);
    }
	private void Update()
	{
        if(allPlayerReady())
		{
            Util.toNextScene();
			listenToServerThread.Abort();
		}

	}
}
