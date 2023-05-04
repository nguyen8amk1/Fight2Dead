using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMessageHandler
{
    private static ServerConnection connection = ServerConnection.Instance; 
    private static GameState globalGameState = GameState.Instance; 

	public delegate void MessageHandlerLambda(string[] tokens);

	// @Note: This gonna be room message receiver loop (lobby, choose character, map select, loading screen);
    // -> scene like waiting for match is not gonna use this (for now) 

	public static void listenToServer(MessageHandlerLambda messageHandler)
	{
        // TODO: receive format: "" 
        while (true)
        {
            string message = connection.receiveMessage();
            string[] tokens = message.Split(',');

            // TODO: find a common format that's suitable for everything message
			int pid = Int32.Parse(Util.getValueFrom(tokens[0]));
			bool isQuitMessage = tokens[1].StartsWith("s:quit");

			if (isQuitMessage)
			{
				Debug.Log($"Player with id:{pid} quit the game");
			} else
			{
				messageHandler(tokens);
			}
        }
	}


    public static void sendLobbyMessage(bool stat) 
    { 
        string lobbyStatMessage = $"rid:{globalGameState.RoomId},s:l,pid:{globalGameState.PlayerId},stat:{Convert.ToInt32(stat)}";
        connection.sendToServer(lobbyStatMessage); 
    }


    // TODO: HAVE THE GAME MODE IN GLOBAL AND THE CLOSE CONNECTION WILL SEND BASE ON THAT 
    public static void sendCloseConnection() 
    {
        switch(globalGameState.onlineMode)
		{
            case "LAN":
			{
				// TODO: isRoomOwner still not working  
				string LANQuitMessage = $"rid:{globalGameState.RoomId},lan:quit,owner:{globalGameState.isRoomOwner},pid:{globalGameState.PlayerId}";
				connection.sendToServer(LANQuitMessage);
			} 
			break;
            case "GLOBAL":
			{
				string GlobalQuitMessage = $"rid:{globalGameState.RoomId},gbl:quit,pid:{globalGameState.PlayerId}";
				connection.sendToServer(GlobalQuitMessage);
			} 
			break;

            default:
                throw new Exception("SOMETHING WRONG WITH THE GAME MODE (LAN/GLOBAL)");
		}
    }

    public static void sendChooseCharacterMessage() 
    {
        // TODO:
    }
   
    public static void sendChooseStageMessage() 
    { 
        // TODO: 
    }
}
