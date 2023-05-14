using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
    public class Player {
        public string rid;
        public string id;
        public TcpClient tcpClient;
        public GameRoom room;
        public IPEndPoint endPoint;
        public bool quitListen = false; 

        public Player(string id, TcpClient tcpClient) {
            this.id = id;
            this.tcpClient = tcpClient;
        }
    }
}