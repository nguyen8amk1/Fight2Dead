using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace SocketServer
{
    public sealed class LANTCPServerConnection : IServerConnection
    {
        private static LANTCPServerConnection instance = null;

        public static LANTCPServerConnection Instance
        {
            get
            {
                if(instance == null)
				{
                    instance = new LANTCPServerConnection();
				}
                return instance;
            }
        }
        
        private TcpClient tcpClient;
        private NetworkStream tcpStream;

        private string serverIp = "127.0.0.1";
        private int tcpPort = 5500;

        private LANTCPServerConnection() { 
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
                try
				{
					Int32 bytes = tcpStream.Read(data, 0, data.Length); //(**This receives the data using the byte method**)
					responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes); //(**This converts it to string**)
					Debug.Log("Received tcp data: " + responseData);

					string[] tokns = responseData.Split(',');
					messageHandler(tokns);
				} catch (Exception ex)
				{
					Debug.Log("Network closed: " + ex.Message);
                    break;
				}
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
