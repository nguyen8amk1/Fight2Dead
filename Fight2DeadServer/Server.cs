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
        private List<ClientInfo> unmatchedClients = new List<ClientInfo>();
        private State currentState = State.NEW;
        private int clientId = 1;
        private int roomId = 1;

        private List<GameRoom> rooms = new List<GameRoom>();
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

                Console.WriteLine(command);

                switch (currentState)
                {
                    case State.RECEIVE_NEW_CONNECTION:
                    {
                        Console.WriteLine("Connection Received, player id is " + clientId);
                        ClientInfo client = new ClientInfo(clientId, remoteIPEndPoint);
                        clientId++;
                        unmatchedClients.Add(client);

                        playerMatching();
                    }
                    break;
                    case State.RECEIVE_ROOM_PACKET: {
                        // TODO: guide the message to the right room 

                        // get roomid 
                        // index = roomid -1 
                        // remove the room part in the command string 
                        Console.WriteLine("Receive Room packet");
                        // int rid = ;
                        // int index = rid - 1;
                        // rooms[index].process(command);
                    } 
                    break;
                    default:
                        throw new Exception("SOME FUCKING RANDOM STATES IS APPEAR");
                }

                Console.WriteLine(currentState);
            }
        }

        private void playerMatching() {
            if(unmatchedClients.Count >= 2) {
                int lastIndex = unmatchedClients.Count - 1;
                List<ClientInfo> clients = new List<ClientInfo> { unmatchedClients[lastIndex -1], unmatchedClients[lastIndex]};
                GameRoom room = new GameRoom(roomId, clients, listener);
                rooms.Add(room);

                // send packet with rid to both clients
                foreach(ClientInfo c in clients) {
                    string formatedString = string.Format("rid:{0},pid:{1}", roomId, c.id);
                    sendToClient(formatedString, c.endPoint);
                }

                roomId++;

                // remove the matched clients 
                unmatchedClients.RemoveAt(unmatchedClients.Count -1);
                unmatchedClients.RemoveAt(unmatchedClients.Count -1);

            } else {
                ClientInfo lastestClient = unmatchedClients[unmatchedClients.Count -1];
                string formatedString = string.Format("You're id= {0} waiting for another player to match with you!!!", lastestClient.id);
                sendToClient(formatedString, lastestClient.endPoint);
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
