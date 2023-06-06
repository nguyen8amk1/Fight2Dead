using System.Threading;

namespace SocketServer {
    public sealed class ServerCommute {
        public static IServerConnection connection = TCPServerConnection.Instance;
        public static Thread listenToServerThread;
    }
}