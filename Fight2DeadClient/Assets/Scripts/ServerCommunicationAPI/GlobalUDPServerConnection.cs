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
	class GlobalUDPServerConnection
	{
        // ListenToServerFactory.MessageHandlerLambda messageHandler
        private static GameState globalGameState = GameState.Instance;
		private static bool isRunning = false;
        public static void listenToUDPServer(ListenToServerFactory.MessageHandlerLambda messageHandler)
        {

            Debug.Log("UDP listening thread started");


            /*
            using (UdpClient client = new UdpClient())
            {
                //udpClient = client;
                IPAddress serverIP = IPAddress.Parse("103.162.20.146"); // Server IP address
                int serverPort = 8000; // Server port

                try
                {
                    while (true)
                    {
						//string message = "hello server";
						string message = globalGameState.playerMessage;
						byte[] data = Encoding.ASCII.GetBytes(message);

						client.Send(data, data.Length, serverIP.ToString(), serverPort);
						Debug.Log($"Sent: {message}");
                        
                        if(globalGameState.sendP1Info)
						{
                            // TODO: send p1 info 
							string message = globalGameState.player1InfoMessage;
							byte[] data = Encoding.ASCII.GetBytes(message);

							client.Send(data, data.Length, serverIP.ToString(), serverPort);
							Debug.Log($"Sent: {message}");
                            globalGameState.sendP1Info = false;
						}
                        else if(globalGameState.sendP2Info)
						{
                            // TODO: send p1 info 
							string message = globalGameState.player2InfoMessage;
							byte[] data = Encoding.ASCII.GetBytes(message);

							client.Send(data, data.Length, serverIP.ToString(), serverPort);
							Debug.Log($"Sent: {message}");
                            globalGameState.sendP2Info = false;
						}

                        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                        byte[] receiveData = client.Receive(ref remoteEndPoint);
                        string receivedMessage = Encoding.ASCII.GetString(receiveData);

                        Debug.Log($"Received: {receivedMessage}");

                        string[] tokens = receivedMessage.Split(',');
                        messageHandler(tokens);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
				*/
				isRunning = true;
				// Create a UDP client
				UdpClient udpClient = new UdpClient();

				// Set the server IP address and port number
                IPAddress serverIP = IPAddress.Parse("103.162.20.146"); // Server IP address
                int serverPort = 8000; // Server port
				/*
				IPAddress serverIP = IPAddress.Parse("127.0.0.1");
				int serverPort = 1234;
				*/

				// Start a new thread to send messages to the server
				Thread sendThread = new Thread(() => SendMessages(udpClient, serverIP, serverPort));
				sendThread.Start();

				// Start a new thread to receive responses from the server
				Thread receiveThread = new Thread(() => ReceiveResponses(udpClient, messageHandler));
				receiveThread.Start();

				Console.WriteLine("UDP client started.");

				// Wait for user input to stop the client
				Console.WriteLine("Press enter to stop the client.");
				Console.ReadLine();

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
                    Console.WriteLine("Sent message to server: " + messageToSend);

                    //Thread.Sleep(1000); // Wait for 1 second before sending the next message
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("SocketException: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
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
                    //Console.WriteLine("Received response from server: " + receivedMessage);

					Debug.Log($"Received: {receivedMessage}");

					string[] tokens = receivedMessage.Split(',');
					messageHandler(tokens);
                }
                catch (SocketException ex)
                {
                    Console.WriteLine("SocketException: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                }
            }
        }
    }
}
