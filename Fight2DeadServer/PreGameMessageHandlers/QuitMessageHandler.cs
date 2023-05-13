using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    public class QuitMessageHandler : PreGameMessageHandler
    {
        private DebugLogger dlog = new DebugLogger();
        public void handle(string roomId, Player player, string message)
        {
            // Check for termination condition
            // TODO: remove the player from room 
            /*
            Console.WriteLine("Remove player with id:{0} in room with rid: {1}", player.id, player.rid);
            tcpRooms[player.rid].Remove(player.id);
            int playerInRoomCount = tcpRooms[player.rid].Count;
            Console.WriteLine("Now the room with rid:{0} have {1} players left", player.rid, playerInRoomCount);

            bool roomHasNoOne = playerInRoomCount <= 0;
            if (roomHasNoOne)
            {
                // TODO: remove room 
                Console.WriteLine("Now the room with rid:{0} has no player at all, let's remove the room", player.rid);
                tcpRooms.Remove(player.rid);
                Console.WriteLine("Now there are {0} rooms exist", tcpRooms.Count);
            }
            break;
            */
        }
    }
}