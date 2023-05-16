using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;

namespace SocketServer
{
     public class Server
    {
        private int tcpPort = 5000;
        private int udpPort = 8000;
        private TcpListener tcpListener;

        private int playerId = 1;

        private DebugLogger dlog = new DebugLogger(); 
        private PlayerMatcher playerMatcher = new PlayerMatcher();

        public static Dictionary<string, GameRoom> rooms = new Dictionary<string, GameRoom>();

        private string dataCredentialFilePath = "../../DBConnection/DatabaseCredential.txt";

        public static MySQLDatabaseConnection dbConnection; 

        // FIXME: the server get spammed some how when client send login info  
        // FIXME: the matching is not work anymore 
        // TODO: refactor the login/register processing code

        public void run()
        {
            dbConnection = new MySQLDatabaseConnection(dataCredentialFilePath);

            initConnections();

            Thread udpListeningThread = new Thread(() => udpListening());
            udpListeningThread.Start();

            // @Note: THIS IS THE RECEIVED NEW CONNECTION LOOP
            // @Note: should it handle quit game too (the in game quit) ? 

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                Console.WriteLine("Server is listening....");
                string message = receiveNewConnectionMessage(tcpClient);
                Console.WriteLine($"Message: {message}");
                dlog.newConnectionMessageReceived(tcpClient, 1, message);


                string[] tokens = message.Split(',');
                bool isLoginMessage =   Util.getKeyFrom(tokens[0]).Equals("username") && 
										Util.getKeyFrom(tokens[1]).Equals("password");
                if(isLoginMessage)
				{
                    string username = Util.getValueFrom(tokens[0]);
                    string password = Util.getValueFrom(tokens[1]);
                    Console.WriteLine($"TODO: handle login message, username: {username}, password: {password}");
					bool loginSuccess = true;  

					if(loginSuccess)
					{
                        TCPClientConnection.sendToClient(tcpClient, "login:success");
					} else
					{
                        TCPClientConnection.sendToClient(tcpClient, "login:failed");
					}

					Thread thread = new Thread(() => loginListeningLoop(tcpClient));
					thread.Start();
				}

                bool isRegisterMessage = Util.getKeyFrom(tokens[0]).Equals("username") &&
                                         Util.getKeyFrom(tokens[1]).Equals("email") &&
                                         Util.getKeyFrom(tokens[2]).Equals("password");
                if (isRegisterMessage)
				{
                    string username = Util.getValueFrom(tokens[0]);
                    string email = Util.getValueFrom(tokens[1]);
                    string password = Util.getValueFrom(tokens[2]);
                    Console.WriteLine($"TODO: handle register message, username: {username}, email:{email}, password: {password}");

                    bool registrationSuccess = true;
                    if(registrationSuccess)
					{
                        TCPClientConnection.sendToClient(tcpClient, "registration:success");
					} else
					{
                        TCPClientConnection.sendToClient(tcpClient, "registration:failed");
					}

					Thread thread = new Thread(() => registerListeningLoop(tcpClient));
					thread.Start();
				}


            }

        }

		private void loginListeningLoop(TcpClient tcpClient)
		{
            // truong hop login: 
            // if login success -> wait for matching message
            // else wait for receive re-login message: 
            // if wait for longer than 5 minutes -> close the thread and the tcpClient 

            Console.WriteLine("TODO: Login listening loop created");

			TcpClient client = tcpClient;
			// Set up the network stream for reading and writing
			NetworkStream stream = client.GetStream();

			// Handle the client connection
			while (true)
			{
				// -> sent message: "allready"
				byte[] buffer = new byte[1024];
				Console.WriteLine("Login loop is listening...");
				int bytesRead = stream.Read(buffer, 0, buffer.Length);

				string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
				Console.WriteLine("receive in tcp loop: " + message);
				if(String.IsNullOrEmpty(message)) continue;

                string[] tokens = message.Split(',');
				bool isLoginMessage =  Util.getKeyFrom(tokens[0]).Equals("username") && 
									   Util.getKeyFrom(tokens[1]).Equals("password");
                if(isLoginMessage)
				{
					string username = Util.getValueFrom(tokens[0]);
					string password = Util.getValueFrom(tokens[1]);
					Console.WriteLine($"(inside thread) TODO: handle login message, username: {username}, password: {password}");
					bool loginSuccess = true;  

					if(loginSuccess)
					{
                        TCPClientConnection.sendToClient(tcpClient, "login:success");
					} else
					{
                        TCPClientConnection.sendToClient(tcpClient, "login:failed");
					}
				}

                bool isNumPlayersMessage = Util.getKeyFrom(tokens[0]).Equals("numPlayers");
                if (isNumPlayersMessage)
				{
					int playersNum = Int32.Parse(Util.getValueFrom(tokens[0]));

					// Console.WriteLine("Received TCP message: {0}, ip: {1}, port:{2}", message, ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address, ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Port);
					// @Refactor: refactor this into different message handlers 
					// TODO: handle in game quit game message

					GameRoom room = playerMatcher.match(playerId, playersNum, tcpClient);
					if(room != null) {
						dlog.roomCreated(room);
						room.start();
						rooms.Add(room.id, room);
					}

					playerId++;
				}

                /*
				// TODO: handle quit message and break the loop 
				Console.WriteLine("Here is tcp listening loop of player " + player.id);
				PreGameMessageHandler messageHandler = factory.whatPreGameMessage(message);
				messageHandler.handle(id, player, message);
				if(player.quitListen) break;
                */
			}

		}

		private void registerListeningLoop(TcpClient tcpClient)
		{
			TcpClient client = tcpClient;
			// Set up the network stream for reading and writing
			NetworkStream stream = client.GetStream();

			// Handle the client connection
			while (true)
			{
				// -> sent message: "allready"
				byte[] buffer = new byte[1024];
				int bytesRead = stream.Read(buffer, 0, buffer.Length);

				string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
				Console.WriteLine("receive in tcp loop: " + message);
				if(String.IsNullOrEmpty(message)) continue;

                string[] tokens = message.Split(',');
                bool isRegisterMessage = Util.getKeyFrom(tokens[0]).Equals("username") &&
                                         Util.getKeyFrom(tokens[1]).Equals("email") &&
                                         Util.getKeyFrom(tokens[2]).Equals("password");
                if (isRegisterMessage)
				{
                    string username = Util.getValueFrom(tokens[0]);
                    string email = Util.getValueFrom(tokens[1]);
                    string password = Util.getValueFrom(tokens[2]);
                    Console.WriteLine($"TODO: handle register message, username: {username}, email:{email}, password: {password}");
                    bool registrationSuccess = true;
                    if(registrationSuccess)
					{
                        TCPClientConnection.sendToClient(tcpClient, "registration:success");
					} else
					{
                        TCPClientConnection.sendToClient(tcpClient, "registration:failed");
					}
				}

				bool isLoginMessage =  Util.getKeyFrom(tokens[0]).Equals("username") && 
									   Util.getKeyFrom(tokens[1]).Equals("password");
                if(isLoginMessage)
				{
					string username = Util.getValueFrom(tokens[0]);
					string password = Util.getValueFrom(tokens[1]);
					Console.WriteLine($"(inside thread) TODO: handle login message, username: {username}, password: {password}");
					bool loginSuccess = true;  

					if(loginSuccess)
					{
                        TCPClientConnection.sendToClient(tcpClient, "login:success");
					} else
					{
                        TCPClientConnection.sendToClient(tcpClient, "login:failed");
					}

					Thread thread = new Thread(() => loginListeningLoop(tcpClient));
					thread.Start();
					return;
				}

                /*
				// TODO: handle quit message and break the loop 
				Console.WriteLine("Here is tcp listening loop of player " + player.id);
				PreGameMessageHandler messageHandler = factory.whatPreGameMessage(message);
				messageHandler.handle(id, player, message);
				if(player.quitListen) break;
                */
			}
		}

		private string receiveNewConnectionMessage(TcpClient tcpClient) {
            NetworkStream stream = tcpClient.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            if(bytesRead > 0)
				return Encoding.ASCII.GetString(buffer, 0, bytesRead);
            throw new Exception("DITME DEO CO CAI LON GI HET V");
        }

        private void initConnections() {
            // Thread udpListeningThread = new Thread(() => udpListening());
            // udpListeningThread.Start();

            // Create a TCP listener
            tcpListener = new TcpListener(IPAddress.Any, tcpPort);
            tcpListener.Start();
        }

        private void udpListening()
        {
            UdpClient udpListener = new UdpClient(udpPort);

            while (true)
            {
                if (udpListener.Available > 0)
                {
                    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                    byte[] buffer = udpListener.Receive(ref remoteEP);
                    string message = Encoding.ASCII.GetString(buffer);
                    string[] tokens = message.Split(',');

                    string rid = Util.getValueFrom(tokens[0]);
                    string pid = Util.getValueFrom(tokens[1]);

                    dlog.messageReceived(pid, 3, message);
                    rooms[rid].process(tokens);
                }
            }

        }

        static void Main(string[] args)
        {
            new Server().run();
        }
    }

}