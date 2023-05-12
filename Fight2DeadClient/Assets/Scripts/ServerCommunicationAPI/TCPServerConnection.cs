using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace SocketServer
{
    public sealed class TCPServerConnection : IServerConnection
    {
        private static readonly TCPServerConnection instance = new TCPServerConnection();

        public static TCPServerConnection Instance
        {
            get
            {
                return instance;
            }
        }
        
        private TcpClient tcpClient;
        private NetworkStream tcpStream;

        private string serverIp = "127.0.0.1";
        private int tcpPort = 5000;

        private TCPServerConnection() { 
            tcpClient = new TcpClient(serverIp, tcpPort);
            tcpStream = tcpClient.GetStream();
        }

        public void sendToServer(string message) {
            byte[] tcpMessage = Encoding.ASCII.GetBytes(message);
            tcpStream.Write(tcpMessage, 0, tcpMessage.Length);
        }

        public Thread createListenToServerThread(ListenToServerFactory.MessageHandlerLambda messageHandler)
        {
            Thread listenToTCPServerThread = new Thread(() => listenToTCPServer(messageHandler));
            return listenToTCPServerThread;
        }
        
        private void listenToTCPServer(ListenToServerFactory.MessageHandlerLambda messageHandler)
        {
            // TODO: received message: "" 
            while (true)
            {
                Byte[] data = new Byte[256];

                // String to store the response ASCII representation.
                String responseData = String.Empty;

                Debug.Log("Client is listening...");

                // Read the first batch of the TcpServer response bytes.
                Int32 bytes = tcpStream.Read(data, 0, data.Length); //(**This receives the data using the byte method**)
                responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes); //(**This converts it to string**)
                Debug.Log("Received tcp data: " + responseData);

                string[] tokns = responseData.Split(',');
                messageHandler(tokns);
            }
        }

        public void close()
        {
            tcpStream.Close();
            tcpClient.Close();
        }

        public TcpClient getTcpClient() 
        {
            return tcpClient;
        }
    }
}