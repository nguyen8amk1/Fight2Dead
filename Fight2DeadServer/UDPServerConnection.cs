using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace GameSocketServer
{
    public sealed class UDPServerConnection
    {
        private const int listeningPort = 8080;
        private UdpClient listener = null;
        private static UDPServerConnection instance = null;

        private UDPServerConnection()
        {
            listener = new UdpClient(listeningPort);
        }

        public static UDPServerConnection getInstance()
        {
            if (instance == null)
            {
                instance = new UDPServerConnection();
            }
            return instance;
        }

        public void sendToClient(string message, IPEndPoint endPoint)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            Console.WriteLine("Sending to " + endPoint.Address.ToString() + ":" + endPoint.Port);
            listener.Send(bytes, bytes.Length, endPoint.Address.ToString(), endPoint.Port);
        }

        public InitialData receiveData() {
            Console.WriteLine("Server is listening...");
            IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] bytes = listener.Receive(ref remoteIPEndPoint);

            string message = Encoding.ASCII.GetString(bytes);

            return new InitialData(message, remoteIPEndPoint);
        }

        public void sendToEveryOneElse(int pid, string message, Dictionary<string, ClientInfo> clients)
        {
            foreach (var c in clients.Values)
            {
                if (c.id == pid)
                    continue;
                sendToClient(message, c.endPoint);
            }
        }
    }
}