using System.Collections.Generic;
using System.Text;

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
            if (room.toUdpWaitList.Count == room.playersNum)
            {
                // -> broadcast with message: "allready"

                // TODO: refactor all the message sending to a class 
                TCPClientConnection.broadcast(room.tcpPlayers, "allready");

                // -> convert that tcp room into udp room and remove that tcp room from Dict both tcpClients and toUdpWaitList
                foreach (Player p in room.toUdpWaitList.Values)
                {
                    room.udpPlayers.Add(p.id, p.endPoint);
                    p.quitListen = true; 
                }

                dlog.switchToUDP(roomId);

                room.tcpPlayers.Clear();
                room.toUdpWaitList.Clear();

            }
        }
    }
}