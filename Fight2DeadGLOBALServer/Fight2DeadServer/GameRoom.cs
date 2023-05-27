using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;
using Fight2DeadServer;
using System.Security.Principal;
using System.Runtime.CompilerServices;
using System.ComponentModel.Design;
using System.Web;
using Org.BouncyCastle.Utilities.IO;
using System.Reflection.Metadata.Ecma335;

namespace SocketServer
{
    // TODO: what a room will contained ?? 
    // Room numPlayers mode 
    // Dict of tcp players 
    // Dict of wait list 
    // Dict of udp players 

    public sealed class GameRoom
    {
        public string id { get; set; }
        public int playersNum { get; set; }
        public Dictionary<string, Player> players = new Dictionary<string, Player>();
        public Dictionary<string, Player> toUdpWaitList = new Dictionary<string, Player>();
        public Dictionary<string, Player> udpPlayers = new Dictionary<string, Player>();
        public string onlineMode = "GLOBAL"; // @Test 

        private MessageHandlerFactory factory = new MessageHandlerFactory(); 

        private DebugLogger dlog = new DebugLogger();
        // TODO: create a udp thread that have a single loop that wait for udp message

        public void start()
        {
            foreach (Player player in players.Values)
            {
                Thread thread = new Thread(() => tcpListening(player));
                thread.Start();
            }
        }

        public void udpProcess(UdpClient udpListener, string[] tokens) {
            // format: client send to server: rid,pid,x,y,state 
            string message = $"{tokens[1]},{tokens[2]},{tokens[3]},{tokens[4]}";
            int pid = Int32.Parse(tokens[1]);

            //UDPClientConnection.sendToClient(udpListener, udpPlayers["1"].endPoint.Address.ToString(), 9999, "hello");
            //UDPClientConnection.sendToOthers(udpPlayers, udpListener, pid, message);
        }

        // These message the room will do
        private void tcpListening(Player player)
        {

			// Set up the network stream for reading and writing
			TcpClient client = player.tcpClient;
			NetworkStream stream = client.GetStream();

			// Handle the client connection
			while (true)
			{
				if (player.quitListen)
				{
					Console.WriteLine("tcp client listening loop quit");
					break;
				}

				byte[] buffer = new byte[1024];
				int bytesRead = stream.Read(buffer, 0, buffer.Length);
				string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
				Console.WriteLine("receive in tcp loop: " + message);
                if (String.IsNullOrEmpty(message)) return;

				// TODO: handle quit message and break the loop 
				Console.WriteLine("Here is tcp listening loop of player " + player.id);
				PreGameMessageHandler messageHandler = factory.whatPreGameMessage(message);
				messageHandler.handle(id, player, message);
			}
        }

    }
}