using System;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace GameSocketServer
{
    public class Server
    {
        private UDPServerConnection connection = UDPServerConnection.getInstance(); 
        private ServerGlobalData globalData = ServerGlobalData.getInstance();
        private MessageStateFactory factory = new MessageStateFactory();

        public Server()
        {
        }

        public void go()
        {
            // TODO: the conversion from TCP to UDP will happen here.
            while (true)
            {
                InitialData data = connection.receiveData();
                string message = data.message; 
                globalData.NewlyAddedClientRemoteIPEndPoint = data.remoteIPEndPoint;

                Console.WriteLine(message);

                IState messageState = factory.createMessageState(message);
                messageState.serve(message);
            }
        }

        public static void Main(string[] args)
        {
            new Server().go();
        }
    }

}
