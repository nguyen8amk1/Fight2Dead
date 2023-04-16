using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;


namespace TestSocket
{
    public class Server
    {
        private const int listeningPort = 8080;
        private UdpClient listener = new UdpClient(listeningPort);
        private List<ClientInfo> clients = new List<ClientInfo>();
        private State currentState = State.NEW;
        private int clientId = 1;

        // TODO: PLAYER MATCHING
        // con che do 2 nguoi choi, 4 nguoi choi 
        // HOW IT'S GONNA WORK, hien tai chi co 1 phong duy nhat 

        public Server()
        {
        }

        public void go()
        {
            while (true)
            {
                Console.WriteLine("Server is listening...");
                IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] bytes = listener.Receive(ref remoteIPEndPoint);
                string command = Encoding.ASCII.GetString(bytes);
                
                currentState = nextState(command);

                switch (currentState)
                {
                    case State.RECEIVE_NEW_CONNECTION:
                    {
                        Console.WriteLine("Connection Received, player id is " + clientId);
                        ClientInfo client = new ClientInfo(clientId, remoteIPEndPoint);
                        sendToClient("id:" + clientId, remoteIPEndPoint);
                        clientId++;
                        clients.Add(client);
                        break;
                    }
                    case State.RECEIVE_POSITION:
                    {
                        // send to players 2, player1's pos 
                        // state: receive position
                        string[] tokens = command.Split(':');
                        string[] xy = tokens[2].Split(',');
                        string x = xy[0];
                        string y = xy[1];

                        // TODO: this can be refactor even more 
                        if (command.StartsWith("id:1"))
                        {
                            sendPosToClientWithId(2, x, y);
                        }
                        else if (command.StartsWith("id:2"))
                        {
                            sendPosToClientWithId(1, x, y);
                        }
                        break;
                    }
                    default:
                        throw new Exception("SOME FUCKING RANDOM STATES IS APPEAR");
                }

                Console.WriteLine(currentState);
            }
        }

        private void sendPosToClientWithId(int id, string x, string y) {
            if (clients.Count > 1)
            {
                string formatString = string.Format("{0},{1}", x, y);
                sendToClient(formatString, clients[id-1].endPoint);
            }
        }

        private State nextState(string command) {
            return MessageParser.parse(command);
        }

        private void sendToClient(string message, IPEndPoint endPoint)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            Console.WriteLine("Sending to " + endPoint.Address.ToString() + ":" + endPoint.Port);
            listener.Send(bytes, bytes.Length, endPoint.Address.ToString(), endPoint.Port);
        }
        public static void Main(string[] args)
        {
            new Server().go();
        }
    }

}
