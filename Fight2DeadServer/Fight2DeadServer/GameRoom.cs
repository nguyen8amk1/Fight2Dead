using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;
using Fight2DeadServer;

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
        public Dictionary<string, IPEndPoint> udpPlayers = new Dictionary<string, IPEndPoint>();
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

        public void process(string[] tokens) {
            // TODO: actually send the position data back to the other clients  
            // receive format: rid:{},pid:{},x:,y:
            // send format: "pid:{},x:{},y:{}"
            int pid = Int32.Parse(Util.getValueFrom(tokens[1]));
            int x = Int32.Parse(Util.getValueFrom(tokens[2]));
            int y = Int32.Parse(Util.getValueFrom(tokens[3]));
            string message = $"pid:{pid},x:{x},y:{y}";
            UDPClientConnection.sendToOthers(players, pid, message);
            Console.WriteLine("TODO: send position data to other players using UDP");
        }

        // These message the room will do
        private void tcpListening(Player player)
        {
            try
            {
                TcpClient client = player.tcpClient;
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

                    // TODO: handle quit message and break the loop 
                    Console.WriteLine("Here is tcp listening loop of player " + player.id);
                    PreGameMessageHandler messageHandler = factory.whatPreGameMessage(message);
                    messageHandler.handle(id, player, message);
                    if(player.quitListen) break;
                }

                // Clean up the network stream and client socket
                stream.Dispose();
                client.Close();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur while handling the client
                Console.WriteLine("Error handling client: " + ex.Message);
            }
        }

    }
}