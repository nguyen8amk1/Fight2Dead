using System;
using System.Collections.Generic;

namespace GameSocketServer {
    public class InRoomLANCloseConnectionState : IRoomState {
        private ServerConnection connection = ServerConnection.getInstance();
        private ServerGlobalData globalData = ServerGlobalData.getInstance();

        public void serve(string message, Dictionary<string, ClientInfo> clients, int roomId) {
            // receive format: "lan:quit,owner:true,pid:{}"
            Console.WriteLine(message);

            string[] tokens = message.Split(',');
            int pid = Int32.Parse(Util.getValueFrom(tokens[2]));

            Console.WriteLine("Clients remain: " + clients.Count);
            clients.Remove(pid.ToString());
            Console.WriteLine("Clients after remove: " + clients.Count);

            string v = Util.getValueFrom(tokens[1]);
            bool isRoomOwner = v.Equals("true");

            if(isRoomOwner) {
                string roomGoneMessage = "0000";
                connection.sendToEveryOneElse(pid, roomGoneMessage, clients);
                
                roomShutDown(roomId);
                Console.WriteLine("Room owner gones, so delete roomId: " + roomId);
                Console.WriteLine("How many room have left: " + globalData.Rooms.Count);
            } else {
                string sendFormat = "pid:{0},s:quit";
                string formatedMessage = string.Format(sendFormat, pid);
                connection.sendToEveryOneElse(pid, formatedMessage, clients);
            } 
        }

        private void roomShutDown(int roomId) {
            globalData.removeRoom(roomId);

        }
    }  
}