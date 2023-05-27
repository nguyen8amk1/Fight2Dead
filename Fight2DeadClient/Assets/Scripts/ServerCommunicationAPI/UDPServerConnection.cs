using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using System.IO;

namespace SocketServer
{
    public sealed class UDPServerConnection
    {
        private static readonly UDPServerConnection instance = new UDPServerConnection();

        public static UDPServerConnection Instance
        {
            get
            {
                return instance;
            }
        }

        public static int udpPort = 8000;
        public static UdpClient udpClient = new UdpClient();

        public static string serverIp;
        private static GameState globalGameState = GameState.Instance;

        private UDPServerConnection()
        {

        }

        public static void sendToServer(string message)
        {
            //int port = ((IPEndPoint)udpClient.Client.LocalEndPoint).Port;
            //Debug.Log($"Send to server using : {udpClient.Client.LocalEndPoint.ToString()}");
            byte[] udpMessage = Encoding.ASCII.GetBytes(message);
            udpClient.Send(udpMessage, udpMessage.Length, serverIp, udpPort);
        }

        public Thread createListenToServerThread(ListenToServerFactory.MessageHandlerLambda messageHandler)
        {
            //return new Thread(() => listenToUDPServer(messageHandler));
            return null;
        }

        public void close()
        {
            udpClient.Close();
        }


        public void inheritPortFromGLOBAL(TCPServerConnection tcpConnection)
        {
            int sourcePort = ((IPEndPoint)tcpConnection.getTcpClient().Client.LocalEndPoint).Port;
            // @Debug
            //udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, sourcePort));
        }

        public void inheritPortFromLAN(LANTCPServerConnection tcpConnection)
        {
            int sourcePort = ((IPEndPoint)tcpConnection.getTcpClient().Client.LocalEndPoint).Port;
            //udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, sourcePort));
        }


        public static void listenToUDPServer(ListenToServerFactory.MessageHandlerLambda messageHandler)
        {
            /*
            while (true)
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                int listeningPort = ((IPEndPoint)udpClient.Client.LocalEndPoint).Port;
                Debug.Log($"UDP Listening for server at {listeningPort}");
                byte[] bytes = udpClient.Receive(ref remoteEndPoint);
                string message = Encoding.ASCII.GetString(bytes);
                Debug.Log("Received udp data: " + message);


                string[] tokens = message.Split(',');
                messageHandler(tokens);
            }
            */
            //IPAddress serverIP = IPAddress.Parse("103.162.20.146"); // Server IP address
            //IPAddress serverIP = IPAddress.Parse("127.0.0.1"); // Server IP address
            //int serverPort = 8000; // Server port

            Debug.Log("UDP listening thread started");

            using (UdpClient client = new UdpClient())
            {
                IPAddress serverIP = IPAddress.Parse("103.162.20.146"); // Server IP address
                                                                        //IPAddress serverIP = IPAddress.Parse("127.0.0.1"); // Server IP address
                int serverPort = 8000; // Server port

                try
                {
                    while (true)
                    {
                        string message = "Hello server";
                        byte[] data = Encoding.ASCII.GetBytes(message);

                        client.Send(data, data.Length, serverIP.ToString(), serverPort);
                        Debug.Log($"Sent: {message}");

                        IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                        byte[] receiveData = client.Receive(ref remoteEndPoint);
                        string receivedMessage = Encoding.ASCII.GetString(receiveData);

                        Debug.Log($"Received: {receivedMessage}");

                        /*
						string[] tokens = receivedMessage.Split(',');
						messageHandler(tokens);
                        */
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }

        public static void listenToUDPServer()
        {
            /*
            while (true)
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                int listeningPort = ((IPEndPoint)udpClient.Client.LocalEndPoint).Port;
                Debug.Log($"UDP Listening for server at {listeningPort}");
                byte[] bytes = udpClient.Receive(ref remoteEndPoint);
                string message = Encoding.ASCII.GetString(bytes);
                Debug.Log("Received udp data: " + message);


                string[] tokens = message.Split(',');
                messageHandler(tokens);
            }
            */
            //IPAddress serverIP = IPAddress.Parse("103.162.20.146"); // Server IP address
            //IPAddress serverIP = IPAddress.Parse("127.0.0.1"); // Server IP address
            //int serverPort = 8000; // Server port

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