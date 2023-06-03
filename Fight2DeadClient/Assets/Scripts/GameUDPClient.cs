using SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SocketServer
{
	class GameUDPClient
	{
        private static UdpClient udpClient;
        private static Thread sendThread;
        private static Thread receiveThread;
        private static bool isRunning;
		private static GameState globalGameState = GameState.Instance;

		public static void Start(string ip)
        {
            udpClient = new UdpClient();
            isRunning = true;
			// Set the server IP address and port number
			IPAddress serverIP = IPAddress.Parse(ip); // Server IP address
			int serverPort = 8000; // Server port

			// Start a new thread to send messages to the server
			sendThread = new Thread(() => SendMessages(udpClient, serverIP, serverPort));
			sendThread.Start();

			// Start a new thread to receive responses from the server
			receiveThread = new Thread(() => ReceiveResponses(udpClient, ListenToServerFactory.tempUDPListening()));
			receiveThread.Start();

			Debug.Log("UDP client started.");
        }

        public static void Stop()
        {
            isRunning = false;
            sendThread.Join();
            receiveThread.Join();
            udpClient.Close();
        }

		static void SendMessages(UdpClient udpClient, IPAddress serverIP, int serverPort)
		{
			while (isRunning)
			{
				try
				{
					// Send a message to the server
					string messageToSend = globalGameState.playerMessage;
					byte[] sendData = Encoding.ASCII.GetBytes(messageToSend);
					udpClient.Send(sendData, sendData.Length, serverIP.ToString(), serverPort);
					Debug.Log("Sent message to server: " + messageToSend);

					Thread.Sleep(1); // Wait for 1 second before sending the next message
				}
				catch (SocketException ex)
				{
					Debug.Log("SocketException: " + ex.Message);
				}
				catch (Exception ex)
				{
					Debug.Log("Exception: " + ex.Message);
				}
			}
		}

		static void ReceiveResponses(UdpClient udpClient, ListenToServerFactory.MessageHandlerLambda messageHandler)
		{
			IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

			while (isRunning)
			{
				try
				{
					// Receive response from the server
					byte[] receivedData = udpClient.Receive(ref remoteEndPoint);

					// Process the received response
					string receivedMessage = Encoding.ASCII.GetString(receivedData);
					//Debug.Log("Received response from server: " + receivedMessage);

					Debug.Log($"Received: {receivedMessage}");

					string[] tokens = receivedMessage.Split(',');
					messageHandler(tokens);
				}
				catch (SocketException ex)
				{
					Debug.Log("SocketException: " + ex.Message);
				}
				catch (Exception ex)
				{
					Debug.Log("Exception: " + ex.Message);
				}
			}
		}
    }
}
