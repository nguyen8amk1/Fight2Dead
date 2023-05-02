using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMessageSender
{
    private static ServerConnection connection = ServerConnection.Instance; 

    public static void sendEstablishNewConnectionWithServerMessage() 
    { 
        connection.sendToServer("command:connect");
    }

    public static void sendCloseConnectionWithServerMessage() 
    {
        connection.sendToServer("quit");
    }

}
