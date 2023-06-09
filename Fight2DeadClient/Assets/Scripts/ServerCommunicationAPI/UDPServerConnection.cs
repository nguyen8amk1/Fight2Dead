using System;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using System.IO;

namespace SocketServer
{
	public sealed class UDPServerConnection : IServerConnection
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

        public void sendToServer(string message)
        {
            //int port = ((IPEndPoint)udpClient.Client.LocalEndPoint).Port;
            //Debug.Log($"Send to server using : {udpClient.Client.LocalEndPoint.ToString()}");
            byte[] udpMessage = Encoding.ASCII.GetBytes(message);
            udpClient.Send(udpMessage, udpMessage.Length, serverIp, udpPort);
        }

        public Thread createListenToServerThread(ListenToServerFactory.MessageHandlerLambda messageHandler)
        {
            return new Thread(() => listenToUDPServer(messageHandler));
        }

        public void close()
        {
            udpClient.Close();
        }

        public void inheritPortFromLAN(LANTCPServerConnection tcpConnection)
        {
            int sourcePort = ((IPEndPoint)tcpConnection.getTcpClient().Client.LocalEndPoint).Port;
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, sourcePort));
        }


        public static void listenToUDPServer(ListenToServerFactory.MessageHandlerLambda messageHandler)
        {

            Debug.Log("UDP listening thread started");
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
        }
    }
}