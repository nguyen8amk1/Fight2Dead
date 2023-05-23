using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEditor;
using SocketServer;
using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;

public class MenuSelectButton : MonoBehaviour
{
    private static GameState globalGameState = GameState.Instance;
    public void clickedButton()
    {
        string clickedButtonName = EventSystem.current.currentSelectedGameObject.name;
        UnityEngine.Debug.Log(clickedButtonName + " Press");
        bool globalPressed = clickedButtonName.Equals("Global");
        bool press1v1 = clickedButtonName.Equals("1VS1");
        bool createRoomPressed = clickedButtonName.Equals("Create Room");
        bool joinRoomPressed = clickedButtonName.Equals("Join Room");

        if(createRoomPressed)
        {
            try
			{
                /*
				string exePath = "D:\\Programming\\UnityProject\\Fight2Dead\\Fight2DeadLANServer\\Fight2DeadServer\\bin\\Debug\\Fight2DeadServer.exe";
                // Create a new process start info
                ProcessStartInfo startInfo = new ProcessStartInfo();
				startInfo.FileName = exePath;
                Process.Start(startInfo);

                Thread.Sleep(1000);
                */

				ServerCommute.connection = LANTCPServerConnection.Instance;
				ServerCommute.listenToServerThread.Abort();
				ServerCommute.listenToServerThread = ServerCommute.connection.createListenToServerThread(ListenToServerFactory.tempTCPListening());
				ServerCommute.listenToServerThread.Start();

			} catch (Exception e)
			{
				UnityEngine.Debug.Log("Something wrong happens when create the room: " + e.Message);
			}
        }

        if(joinRoomPressed)
        {
            UnityEngine.Debug.Log("TODO: find a lan server");
			// connect to that lan server 
			ServerCommute.connection = LANTCPServerConnection.Instance;
			ServerCommute.listenToServerThread.Abort();
			ServerCommute.listenToServerThread = ServerCommute.connection.createListenToServerThread(ListenToServerFactory.tempTCPListening());
			ServerCommute.listenToServerThread.Start();
        }


        if(press1v1)
		{
            UnityEngine.Debug.Log("set numplayers to 2");
            globalGameState.numPlayers = 2;
            return;
		}

        bool press2v2 = clickedButtonName.Equals("2VS2");
        if(press2v2)
		{
            UnityEngine.Debug.Log("set numplayers to 4");
            globalGameState.numPlayers = 4;
            return;
		}

        if(globalPressed)
		{
            UnityEngine.Debug.Log("set online mode to global");
            globalGameState.onlineMode = "GLOBAL";
            return;
		}

        bool lanPressed = clickedButtonName.Equals("LAN");
        if(lanPressed)
		{
            UnityEngine.Debug.Log("set online mode to lan");
            globalGameState.onlineMode = "LAN";
            return;
		}

    }

}
