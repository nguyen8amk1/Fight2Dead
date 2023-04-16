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

        public Server()
        {
        }

        public void go()
        {

            // GOAL: REFACTOR TO STATE MACHINE  
            // STATES: 
            // waiting, receive_new_connect, receive_pos 

            // NOTE: 
            // right now there is only 1 room for all the players 
            // and the room players is also fixed, with id 1 and 2 
            // so the first player join in and get id 1
            // second player join in and get id 2 
            // when the player id=1 moves it send it's position to the server
            // and the server send player id=1 to player id=2
            // when the player id=2 moves it send it's position to the server
            // and the server send player id=1 to player id=1
            

            // TODO: refactor to CHATGPT 
            while (true)
            {
                Console.WriteLine("Server is listening...");
                IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] bytes = listener.Receive(ref remoteIPEndPoint);
                string command = Encoding.ASCII.GetString(bytes);
                
                currentState = nextState(command);

                // DO THE WORK
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
