using System;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace GameSocketServer {
    public class InitialData {
        public string message; 
        public IPEndPoint remoteIPEndPoint;
        public InitialData(string message, IPEndPoint remoteIPEndPoint) {
            this.message = message; 
            this.remoteIPEndPoint = remoteIPEndPoint;
        }
    }
}