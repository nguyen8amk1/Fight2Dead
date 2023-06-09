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
using System.Net;
using System.IO;
using UnityEngine.UI;

public class MenuSelectButton : MonoBehaviour
{
    private static GameState globalGameState = GameState.Instance;
    public InputField ipInputField;

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
                string currentDirectory = Directory.GetCurrentDirectory();
				UnityEngine.Debug.Log(currentDirectory);

                // Create a relative path to the file
                string relativePath = Path.Combine(currentDirectory, "LanServer", "Fight2DeadServer.exe");

                // Check if the file exists
                if (File.Exists(relativePath))
                {
					// Create a new process start info
					ProcessStartInfo startInfo = new ProcessStartInfo();
					startInfo.FileName = relativePath;
					Process.Start(startInfo);

					Thread.Sleep(1000);

					ServerCommute.connection = LANTCPServerConnection.Instance;

					ServerCommute.listenToServerThread.Abort();
					ServerCommute.listenToServerThread = ServerCommute.connection.createListenToServerThread(ListenToServerFactory.tempTCPListening());
					ServerCommute.listenToServerThread.Start();
                }
                else
                {
                    UnityEngine.Debug.Log("File does not exist.");
                }


			} catch (Exception e)
			{
				UnityEngine.Debug.Log("Something wrong happens when create the room: " + e.Message);
			}
        }

        if(joinRoomPressed)
        {
            UnityEngine.Debug.Log("TODO: find a lan server");


            // connect to that lan server 
            // TODO: get the ip in the input text  

            string serverIpAddress = ipInputField.text.Trim(); // TODO: get the ip from a form that user typed into

            if (!string.IsNullOrEmpty(serverIpAddress))
            {
                //Console.WriteLine("Found TCP server at IP: {0}", serverIpAddress);
                LANTCPServerConnection.serverIp = serverIpAddress; 
				ServerCommute.connection = LANTCPServerConnection.Instance;
				ServerCommute.listenToServerThread.Abort();
				ServerCommute.listenToServerThread = ServerCommute.connection.createListenToServerThread(ListenToServerFactory.tempTCPListening());
				ServerCommute.listenToServerThread.Start();
            }
            else
            {
                UnityEngine.Debug.Log("No TCP server found on the local network.");
            }
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
            UDPServerConnection.serverIp = "103.162.20.146";
            return;
		}

        bool lanPressed = clickedButtonName.Equals("LAN");
        if(lanPressed)
		{
            UnityEngine.Debug.Log("set online mode to lan");
            globalGameState.onlineMode = "LAN";
            UDPServerConnection.serverIp = "127.0.0.1";
            return;
		}

    }

    private static string SearchTcpServerOnLocalNetwork(int serverPort)
    {
        // Get the local IP addresses of the machine
        IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

        // Iterate over the local IP addresses
        foreach (IPAddress localIP in localIPs)
        {
            // Skip non-IPv4 and loopback addresses
            if (localIP.AddressFamily != AddressFamily.InterNetwork || IPAddress.IsLoopback(localIP))
                continue;

            // Create a TCP client socket
            TcpClient client = new TcpClient();
            try
            {
                // Attempt to connect to the server at the specified IP address and port
                client.Connect(localIP, serverPort);

                // Get the IP address of the server
                IPAddress serverIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address;

                return serverIP.ToString();
            }
            catch (SocketException)
            {
                // No server found at this IP address
                Console.WriteLine("Error: No server found");
            }
            finally
            {
                // Close the TCP client socket
                client.Close();
            }
        }

        return null; // No server found
    }

}
