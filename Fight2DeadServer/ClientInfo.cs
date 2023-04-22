using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace TestSocket {
    public class ClientInfo {
        public int id; 
        public IPEndPoint endPoint;
        public ClientInfo(int id, IPEndPoint endPoint) {
            this.id = id; 
            this.endPoint = endPoint;
        }

        public override string ToString() {
            return string.Format("id:{0}, ip:{1}, port:{2}", id, endPoint.Address, endPoint.Port);            
        }
    }
}