using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEditor;
using SocketServer;
using System;
//using System.Diagnostics;
using TreeEditor;
using System.Threading;
using System.Net.Sockets;
using System.Text;

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
			UnityEngine.Debug.Log("launch a lan server process");
            //string exePath = "D:\\Programming\\UnityProject\\Fight2Dead\\Fight2DeadClient\\LANServer\\Debug\\Fight2DeadServer.exe";
            // Create a new process start info
            //ProcessStartInfo startInfo = new ProcessStartInfo();
            //startInfo.FileName = exePath;

            // TODO: have to close the global tcp thread; 
            // start a new lan tcp thread 
            // try to figure out how to do it 

            // @Test
            /*
            TcpClient testClient = new TcpClient("127.0.0.1", 5500);
            byte[] tcpMessage = Encoding.ASCII.GetBytes("ditme");
            testClient.GetStream().Write(tcpMessage, 0, tcpMessage.Length);
            */

			ServerCommute.connection = LANTCPServerConnection.Instance;
            ServerCommute.listenToServerThread.Abort();
            ServerCommute.listenToServerThread = ServerCommute.connection.createListenToServerThread(ListenToServerFactory.tempTCPListening());

            // Still have some error 

            //TCPServerConnection.LAN_MODE = true;
            //TCPServerConnection.Instance.close();
			//TCPServerConnection.Instance.changeToLAN("127.0.0.1", 5500);

            /*

			try

			{
				// Start the process
				//Process process = Process.Start(startInfo);
				//process.WaitForInputIdle();
				//UnityEngine.Debug.Log("The executable has been opened successfully.");
				//UnityEngine.Debug.Log("change the tcp connection to lan as well");

                Thread.Sleep(1000);
                /*
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.Log("Error occurred while launching the executable: " + ex.Message);
			}
                */

        }

        if(joinRoomPressed)
        {
            // TODO: 
				// find a lan server 
				// connect to that lan server 
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
