using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;


// PLAN: 
// given players their id 
// this is fixed 2 players socket server
// create a thread for each player
// the player1 move the player 2 see the updated position on the screen (mot chieu)

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
            // FOR NOT USE FIXED IP
            // string serverIP = "192.168.162.212";

            // listen for connections  

            int clientId = 1;

            // TODO: detect khi nao nguoi choi tat connection de thao ra khoi list 
            while (true)
            {
                Console.WriteLine("Server is listening...");

                IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] bytes = listener.Receive(ref remoteIPEndPoint);
                string command = Encoding.ASCII.GetString(bytes);

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
					// TODO: 
				}
            }
        }

		// currently not using 
        private void serveClient(ClientInfo clientInfo)
        {
			clients.Add(clientInfo);
			foreach(ClientInfo c in clients) {
				Console.WriteLine(c.ToString());
			}

			Console.WriteLine("Client id is " + clientInfo.id);
			string formatString = string.Format("Client Thread with id: {0} is listening to {1}:{2}", clientInfo.endPoint.Address.ToString(), clientInfo.id.ToString(), clientInfo.endPoint.Port);
			Console.WriteLine(formatString);

            while (true)
            {
                byte[] bytes = listener.Receive(ref clientInfo.endPoint);
                string position = Encoding.ASCII.GetString(bytes);
                // Console.WriteLine(position);

                // extract x, y 
                string[] tokens = position.Split(',');
                float x = float.Parse(tokens[0]);
                float y = float.Parse(tokens[1]);

                // checking 
                bool isPlayer1 = clientInfo.id == 1;
                bool isPlayer2 = clientInfo.id == 2;

                // FIXME: why player1 keep sending to himself
                // something with the thread ??
                if (isPlayer1)
                {
                    // TODO: check if the player 2 is available or not 
                    bool player2IsAvailable = clients.Count >= 2;
                    if (!player2IsAvailable)
                    {
                        continue;
                    }
					
                    formatString = string.Format("{0},{1}", x.ToString(), y.ToString());
                    int player2Index = 2 - 1;
                    sendToClient(formatString, clients[player2Index].endPoint);
                }
                else if (isPlayer2)
                {
                    formatString = string.Format("{0},{1}", x.ToString(), y.ToString());
                    int player1Index = 1 - 1;
                    sendToClient(formatString, clients[player1Index].endPoint);
                }
            }
        }

        private void sendToClient(string message, IPEndPoint endPoint)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            Console.WriteLine("Sending to " + endPoint.Address.ToString() + ":" + endPoint.Port);
            listener.Send(bytes, bytes.Length, endPoint.Address.ToString(), endPoint.Port);
        }
        // this gonna remain unused :v 
        private static string getLocalIPAddress()
        {
            {
                IPHostEntry host;
                string localIP = "?";
                host = Dns.GetHostEntry(Dns.GetHostName());

                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                    }
                }
                return localIP;
            }
        }
    }
}
