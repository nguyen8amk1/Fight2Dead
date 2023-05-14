using System.Collections.Generic;
using System.Text;

namespace SocketServer {
    public sealed class TCPClientConnection {
        private static DebugLogger dlog = new DebugLogger();
        public static void broadcast(Dictionary<string, Player> tcpPlayers, string message)
        {
            foreach (Player p in tcpPlayers.Values)
            {
                // Send response
                p.tcpClient.Client.Send(Encoding.ASCII.GetBytes(message));
                dlog.messageSent(p.id, 2, message);
            }
        }

        public static void sendToOthers(Dictionary<string, Player> tcpPlayers, Player player, string message)
        {
            foreach (Player p in tcpPlayers.Values)
            {
                if(p == player) {
                    continue;
                }

                // Send response
                p.tcpClient.Client.Send(Encoding.ASCII.GetBytes(message));
                dlog.messageSent(p.id, 2, message);
            }
        }

    }
}