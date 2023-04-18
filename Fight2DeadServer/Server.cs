using System;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace TestSocket
{
    public class Server
    {
        private ServerConnection connection = ServerConnection.getInstance(); 
        private ServerGlobalData globalData = ServerGlobalData.getInstance();
        private MessageStateFactory factory = new MessageStateFactory();

        public Server()
        {
        }

        public void go()
        {
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
