using System.Collections.Generic;
using System.Text;
using System;

namespace SocketServer
{
    public class QuitMessageHandler : PreGameMessageHandler
    {
        private DebugLogger dlog = new DebugLogger();
        public void handle(string roomId, Player player, string message)
        {
            Console.WriteLine("Remove player with id:{0} in room with rid: {1}", player.id, player.rid);
            GameRoom room = Server.rooms[roomId];
            room.players.Remove(player.id);
            int playerInRoomCount = room.players.Count;
            Console.WriteLine("Now the room with rid:{0} have {1} players left", player.rid, playerInRoomCount);
            if(room.onlineMode.Equals("LAN")) {
                bool roomHasNoOne = playerInRoomCount <= 0;

                string quitMessage = String.Format("pid:{0},quit", player.id);
                TCPClientConnection.sendToOthers(room.players, player, quitMessage);

                if (roomHasNoOne)
                {
                    Server.rooms.Remove(roomId);
                    Console.WriteLine("Now the room with rid:{0} has no player at all, let's remove the room", player.rid);
                    Console.WriteLine("Now there are {0} rooms exist", Server.rooms.Count);
                }
            } if(room.onlineMode.Equals("GLOBAL")) {
                // Assume this is 1v1 mode 
                string quitMessage = String.Format("pid:{0},quit", player.id);
                TCPClientConnection.sendToOthers(room.players, player, quitMessage);
                Server.rooms.Remove(roomId);
                Console.WriteLine("Now there are {0} rooms exist", Server.rooms.Count);
            }
        }
    }
}