using System;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
    public class Client
    {
        // TODO: Wrap all of this into a single class  
        private Thread listenToServerThread;
        public static string rid; 
        public static string pid;

        public Client() {
            ServerCommute.connection = TCPServerConnection.Instance;
        }

        // TODO: move the listen to server function to a lambda 
        public void run()
        {
            // @Note: init 
            string numPlayersMessage = PreGameMessageGenerator.numPlayersMessage(2);
            ServerCommute.connection.sendToServer(numPlayersMessage); // tcp message
            
            listenToServerThread = ServerCommute.connection.createListenToServerThread(ListenToServerFactory.tempTCPListening());
            listenToServerThread.Start();

            Thread.Sleep(5000);
            Console.WriteLine("Send toudp to server");
            string toUdpMessage = PreGameMessageGenerator.toUDPMessage();
            ServerCommute.connection.sendToServer(toUdpMessage); // tcp message

            // @Debug: delay for testing
            Thread.Sleep(5000);

            // @Note: this is the transition
            UDPServerConnection.Instance.inheritPortFrom(TCPServerConnection.Instance);
            TCPServerConnection.Instance.close();
            ServerCommute.connection = UDPServerConnection.Instance;
            listenToServerThread.Abort();

            Console.WriteLine("Started UDP listen to server thread");
            listenToServerThread = ServerCommute.connection.createListenToServerThread(ListenToServerFactory.tempUDPListening());
            listenToServerThread.Start();

            string tempInGameMessage = InGameMessageGenerator.tempInGameMessage(rid, pid);
            ServerCommute.connection.sendToServer(tempInGameMessage); // udp message
        }


        static void Main(string[] args)
        {
            new Client().run();
        }
    }

}