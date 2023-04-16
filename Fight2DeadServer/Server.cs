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
        public static void Main(string[] args)
        {
            new Server().go();
        }

        private const int listeningPort = 8080;
        private UdpClient listener = new UdpClient(listeningPort);
        private List<ClientInfo> clients = new List<ClientInfo>();

        public Server()
        {
        }

        public void go()
        {

            int clientId = 1;

            // TODO: detect khi nao nguoi choi tat connection de thao ra khoi list 
            while (true)
            {
                Console.WriteLine("Server is listening...");

                IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] bytes = listener.Receive(ref remoteIPEndPoint);
                string command = Encoding.ASCII.GetString(bytes);


                // GOAL: REFACTOR TO STATE MACHINE  
                // STATES: 
                // waiting, receive_connect, receive_pos 

                // NOTE: 
                // right now there is only 1 room for all the players 
                // and the room players is also fixed, with id 1 and 2 
                // so the first player join in and get id 1
                // second player join in and get id 2 
                // when the player id=1 moves it send it's position to the server
                // and the server send player id=1 to player id=2
                // when the player id=2 moves it send it's position to the server
                // and the server send player id=1 to player id=1

				Console.WriteLine(command);
				// room creation 
                if (command.Equals("command:connect"))
                {
                    ClientInfo client = new ClientInfo(clientId, remoteIPEndPoint);
					sendToClient("id:"+clientId, remoteIPEndPoint);
                    clientId++;
					clients.Add(client);
                }

				// send to players 2, player1's pos 
				if(command.StartsWith("id:1")) {
					string[] tokens = command.Split(':');
					string[] xy = tokens[2].Split(',');
					if(clients.Count > 1) {
						string formatString = string.Format("{0},{1}", xy[0], xy[1]);
						sendToClient(formatString, clients[1].endPoint);
						Console.WriteLine("Send to client " + clients[1].endPoint.Address + ":" + clients[1].endPoint.Port);
					}
				}
				else if(command.StartsWith("id:2")) {
					string[] tokens = command.Split(':');
					string[] xy = tokens[2].Split(',');
					if(clients.Count > 1) {
						string formatString = string.Format("{0},{1}", xy[0], xy[1]);
						sendToClient(formatString, clients[0].endPoint);
						Console.WriteLine("Send to client " + clients[0].endPoint.Address + ":" + clients[0].endPoint.Port);
					}
				}
            }
        }

        private void sendToClient(string message, IPEndPoint endPoint)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            Console.WriteLine("Sending to " + endPoint.Address.ToString() + ":" + endPoint.Port);
            listener.Send(bytes, bytes.Length, endPoint.Address.ToString(), endPoint.Port);
        }
    }
}
