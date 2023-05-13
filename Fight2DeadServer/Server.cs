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
        public void run()
        {
            initConnections();

            Thread udpListeningThread = new Thread(() => udpListening());
            udpListeningThread.Start();

            // @Note: THIS IS THE RECEIVED NEW CONNECTION LOOP
            // @Note: should it handle quit game too (the in game quit) ? 
            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                string message = receiveNewConnectionMessage(tcpClient);
                dlog.newConnectionMessageReceived(tcpClient, 1, message);

                int playersNum = Int32.Parse(Util.getValueFrom(message));

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

        }

        private string receiveNewConnectionMessage(TcpClient tcpClient) {
            NetworkStream stream = tcpClient.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);

            return Encoding.ASCII.GetString(buffer, 0, bytesRead);
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

        private void sendToClient(string message, IPEndPoint endPoint)
        {
        }

        static void Main(string[] args)
        {
            new Server().run();
        }
    }

}