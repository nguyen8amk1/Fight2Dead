using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SocketServer
{
	class GlobalUDPServerConnection
	{
        // ListenToServerFactory.MessageHandlerLambda messageHandler
        private static GameState globalGameState = GameState.Instance;
        public static void listenToUDPServer(ListenToServerFactory.MessageHandlerLambda messageHandler)
        {

            Debug.Log("UDP listening thread started");
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
                        
                        /*
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
                        */

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
            }
        }
	}
}
