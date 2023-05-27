using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks.Sources;
using System.Windows.Markup;

namespace SocketServer
{
    public class ToUDPMessageHandler : PreGameMessageHandler
    {
        private DebugLogger dlog = new DebugLogger();

        public void handle(string roomId, Player player, string message)
        {
            // if the client want toudp then add to that room wait list  
            // if the room's wait list is full 
            GameRoom room = Server.rooms[roomId]; 

            room.toUdpWaitList.Add(player.id, player);
            Console.WriteLine("Current to udp wait list: ");

            int count = 0; 
            foreach(Player p in room.toUdpWaitList.Values)
            {
                string ip = p.endPoint.Address.ToString(); 
                int port = p.endPoint.Port; 
                Console.WriteLine($"{count}:{ip}:{port}");
                count++;
            } 

            if (room.toUdpWaitList.Count == room.playersNum)
            {
                // -> broadcast with message: "allready"
                // TODO: refactor all the message sending to a class 
                TCPClientConnection.broadcast(room.players, "allready");

                // -> convert that tcp room into udp room and remove that tcp room from Dict both tcpClients and toUdpWaitList
                foreach (Player p in room.toUdpWaitList.Values)
                {
                    //p.tcpClient.Close();
                    p.endPoint.Port = 9999;
                    room.udpPlayers.Add(p.id, p);
                    Console.WriteLine($"UDP PLAYER: {p.id}, {p.endPoint.Address}:{p.endPoint.Port}");
                    p.quitListen = true;
                    //p.tcpClient.Close();
                }

                dlog.switchToUDP(roomId);

                room.players.Clear();
                room.toUdpWaitList.Clear();
            }
        }
    }
}