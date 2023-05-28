using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Net.NetworkInformation;

namespace SocketServer
{
     public class Server
    {
        private int tcpPort = 5500;
        private int udpPort = 8000;
        private TcpListener tcpListener;

        private int playerId = 1;

        private DebugLogger dlog = new DebugLogger(); 
        private PlayerMatcher playerMatcher = new PlayerMatcher();

        public static Dictionary<string, GameRoom> rooms = new Dictionary<string, GameRoom>();

        //private string dataCredentialFilePath = "../../DBConnection/DatabaseCredential.txt";

        public static MySQLDatabaseConnection dbConnection;
		private List<Player> twoPlayersRoomWaitList = new List<Player>();
		private List<Player> fourPlayersRoomWaitList = new List<Player>();

		// FIXME: the server get spammed some how when client send login info  
		// FIXME: the matching is not work anymore 
        private void initConnections() {
            //Thread udpListeningThread = new Thread(() => udpListening());
            //udpListeningThread.Start();

            // Create a TCP listener
            tcpListener = new TcpListener(IPAddress.Any, tcpPort);
            tcpListener.Start();
        }

		public void run()
        {
			initConnections();

            Thread udpListeningThread = new Thread(() => udpListening());
            udpListeningThread.Start();

			Thread createRoomThread = new Thread(() => createRoom());
			createRoomThread.Start();

			Console.WriteLine($"THIS IS THE HOST IP: {getHostIP()}");
			Console.WriteLine("GIVE THE HOST IP TO WHOEVER YOU WANT TO JOIN THE ROOM");


            while (true)
            {
				Console.WriteLine($"Listening for client on port {tcpPort}: ");
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
				Console.WriteLine("Have a client, and create new thread");

				Thread clientThread = new Thread(() =>  handleClient(tcpClient));
				clientThread.Start();
            }

        }
		static string getHostIP()
		{
			string ipAddress = string.Empty;

			foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && netInterface.OperationalStatus == OperationalStatus.Up)
				{
					foreach (UnicastIPAddressInformation ip in netInterface.GetIPProperties().UnicastAddresses)
					{
						if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
						{
							ipAddress = ip.Address.ToString();
							break;
						}
					}
				}

				if (!string.IsNullOrEmpty(ipAddress))
					break;
			}

			return ipAddress;
		}

		private void handleClient(TcpClient tcpClient)
		{
			TcpClient client = tcpClient;
			// Set up the network stream for reading and writing
			NetworkStream stream = client.GetStream();

			// Handle the client connection
			while (true)
			{
				Console.WriteLine("Server is listening....");
				string message = receiveNewConnectionMessage(tcpClient);
				if (message == null) break;
				Console.WriteLine($"Message: {message}");
				dlog.newConnectionMessageReceived(tcpClient, 1, message);

				string[] tokens = message.Split(',');

				//handleLoginMessage(tokens, tcpClient, true);
				//handleRegisterMessage(tokens, tcpClient, true);

				bool isNumPlayersMessage = Util.getKeyFrom(tokens[0]).Equals("numsPlayer") && Util.getKeyFrom(tokens[1]).Equals("username");
				if (isNumPlayersMessage)
				{
					int playersNum = Int32.Parse(Util.getValueFrom(tokens[0]));
					string username = Util.getValueFrom(tokens[1]);

					// Console.WriteLine("Received TCP message: {0}, ip: {1}, port:{2}", message, ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address, ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port);
					// @Refactor: refactor this into different message handlers 
					// TODO: handle in game quit game message

					// what to do: stored the player in a global wait list and match it there  
					// have another thread waiting for enough player 
					if (playersNum == 2)
					{
						twoPlayersRoomWaitList.Add(new Player(playerId.ToString(), username, tcpClient));
						Console.WriteLine("Current 2player waiting count: " + twoPlayersRoomWaitList.Count);
					}
					if (playersNum == 4)
					{
						fourPlayersRoomWaitList.Add(new Player(playerId.ToString(), username, tcpClient));
					}
					playerId++;
					Console.WriteLine("Terminate login listening loop");
					return;
				}
			}
		}

		private void createRoom()
		{
			while(true)
			{
				if(twoPlayersRoomWaitList.Count >= 2)
				{
					GameRoom room = playerMatcher.match(twoPlayersRoomWaitList, 2);
					if(room != null) {
						dlog.roomCreated(room);
						room.start();
						rooms.Add(room.id, room);
					}
				}
				if(fourPlayersRoomWaitList.Count >= 4)
				{
					GameRoom room = playerMatcher.match(fourPlayersRoomWaitList, 4);
					if(room != null) {
						dlog.roomCreated(room);
						room.start();
						rooms.Add(room.id, room);
					}
				}
			}

		}

		private string receiveNewConnectionMessage(TcpClient tcpClient) {
            NetworkStream stream = tcpClient.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

			if (bytesRead > 0)
			{
				StringBuilder messageBuilder = new StringBuilder();
				for (int i = 0; i < bytesRead; i++)
				{
					char receivedChar = (char)buffer[i];
					if (receivedChar == '\r' || receivedChar == '\n')
					{
						// Termination character found, process the received message
						string receivedMessage = messageBuilder.ToString();
						return receivedMessage;
					}
					else
					{
						messageBuilder.Append(receivedChar);
					}
				}

				// End of received data reached without encountering a termination character
				// You can choose to handle this case accordingly
				return messageBuilder.ToString();
			} else
			{
				Console.WriteLine("DITME DEO CO CAI LON GI HET V");
			}
			return null;
			/*
			if (bytesRead > 0)
				return Encoding.ASCII.GetString(buffer, 0, bytesRead);
			*/
        }

        private void udpListening()
        {
			UdpClient udpListener = new UdpClient(udpPort);
            while (true)
            {
                if (udpListener.Available > 0)
                {
					//Console.WriteLine($"Listening at port: {udpPort}");
                    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                    byte[] buffer = udpListener.Receive(ref remoteEP);
                    string message = Encoding.ASCII.GetString(buffer);
					//Console.WriteLine($"Receive: " + message);
                    string[] tokens = message.Split(',');

                    string rid = tokens[0];

                    //dlog.messageReceived("ditme", 3, message);
                    rooms[rid].udpProcess(udpListener, tokens);
                }
            }
		}

        static void Main(string[] args)
        {
            new Server().run();
        }
    }

}