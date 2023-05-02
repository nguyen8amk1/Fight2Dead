using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMessageSender 
{
    private static ServerConnection connection = ServerConnection.Instance; 
    private static GameState globalGameState = GameState.Instance; 

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
