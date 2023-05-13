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
		}
	}

	private bool allPlayerReady()
	{
        return gameState.charNameCount >= 2;
	}

	public void OnButtonClick()
    {
        string chosenCharacter = "SOS";
        if (MainCharacter.selectVal == 0)
            chosenCharacter=(charName[0]);
        else if (MainCharacter.selectVal == 1)
            chosenCharacter=(charName[1]);
        else if (MainCharacter.selectVal == 2)
            chosenCharacter=(charName[2]);
        else chosenCharacter=(charName[3]);

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
