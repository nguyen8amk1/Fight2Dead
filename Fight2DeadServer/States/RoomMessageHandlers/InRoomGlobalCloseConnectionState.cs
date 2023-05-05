using System;
using System.Collections.Generic;

namespace GameSocketServer {
    public class InRoomGlobalCloseConnectionState : IRoomState {
        private ServerConnection connection = ServerConnection.getInstance();
        private ServerGlobalData globalData = ServerGlobalData.getInstance();

        // -> When quit:   
            // if not the room owner quits -> the others still plays as normal
            // if the room owner quits -> the room is shutdown 
            // TODO: How do we know if the message is from the room owner or not ?   
            // -> Message format ?? -> when what format: .....  ?? 

        public void serve(string message, Dictionary<string, ClientInfo> clients, int roomId) {
            // receive format: "gbl:quit,pid:{}"
            Console.WriteLine(message);

            string[] tokens = message.Split(',');
            int pid = Int32.Parse(Util.getValueFrom(tokens[1]));

            Console.WriteLine("Clients remain: " + clients.Count);
            clients.Remove(pid.ToString());
            Console.WriteLine("Clients after remove: " + clients.Count);

            // send format: "pid:{},s:q" 

            bool noOneInRoom = clients.Count == 0;
            if(noOneInRoom) {
                roomShutDown(roomId);
                Console.WriteLine("No one in room, so remove room with id: " + roomId);
                Console.WriteLine("How many room have left: " + globalData.Rooms.Count);
            } else {
                // Info others a player is get out of the room
                string formatedMessage = string.Format("pid:{0},s:quit", pid);
                connection.sendToEveryOneElse(pid, formatedMessage, clients);
            }
        }

        private void roomShutDown(int roomId) {
            globalData.removeRoom(roomId);

        }
    }  
}