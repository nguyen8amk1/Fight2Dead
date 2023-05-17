using System;
using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
	public sealed class UDPClientConnection
	{
        private static DebugLogger dlog = new DebugLogger();
        public static void sendToOthers(Dictionary<string, Player> players, int pid, string message)
        {
            foreach (Player p in players.Values)
            {
                if(pid.ToString() == p.id) {
                    continue;
                }

                // Send response
                Console.WriteLine("TODO: actually send the udp message (in UDPClientConnection)");
                //p.tcpClient.Client.Send(Encoding.ASCII.GetBytes(message));
                //dlog.messageSent(p.id, 3, message);
            }
        }
	}
}