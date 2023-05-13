using System;
using System.Threading;
using System.Net.Sockets;

namespace SocketServer
{
    public interface IServerConnection {
        void sendToServer(string message);
        Thread createListenToServerThread(ListenToServerFactory.MessageHandlerLambda messageHandler);
        void close();
    } 
}