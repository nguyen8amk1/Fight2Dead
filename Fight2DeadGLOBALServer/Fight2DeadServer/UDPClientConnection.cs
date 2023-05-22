using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace SocketServer
{
	public sealed class UDPClientConnection
	{
        private static DebugLogger dlog = new DebugLogger();
        public static void sendToOthers(Dictionary<string, Player> players, UdpClient listener, int pid, string message)
        {
            foreach (Player p in players.Values)
            {
                if(pid.ToString() == p.id) {
                    continue;
                }

                // Send response
                //Console.WriteLine("TODO: actually send the udp message (in UDPClientConnection)");
                byte[] bytes = Encoding.ASCII.GetBytes(message);
                //Console.WriteLine("Sending to " + p.endPoint.Address.ToString() + ":" + p.endPoint.Port);
				string status = $"time:{DateTime.Now.ToString("HH:mm:ss tt")},p{p.id}:{message}";

                /*
                // @Test 
                using (StreamWriter sw = File.AppendText(Server.serversendpath))
                {
                    sw.WriteLine(status);
                }
                */

                listener.Send(bytes, bytes.Length, p.endPoint.Address.ToString(), p.endPoint.Port);
                //p.tcpClient.Client.Send(Encoding.ASCII.GetBytes(message));
                //dlog.messageSent(p.id, 3, message);
            }
        }
	}
}