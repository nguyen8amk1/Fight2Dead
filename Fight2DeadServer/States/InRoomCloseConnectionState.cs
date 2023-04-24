using System;
using System.Collections.Generic;

namespace TestSocket {
    public class InRoomCloseConnectionState : IRoomState {
        private ServerConnection connection = ServerConnection.getInstance();
        private ServerGlobalData globalData = ServerGlobalData.getInstance();

        // thoi diem nhan connection truoc khi vao scene matching  
        // -> phai bat quit 

        // Menu -> Matching -> Lobby -> Choose character -> Choose man choi -> Loading Screen -> game 
        // -> starting from CHOOSE CHARACTER scene 
        // we gonna have quit message send if the player close the game 

        public void serve(string message, Dictionary<string, ClientInfo> clients, int roomId) {
            // receive format: "quit,pid:{}"
            Console.WriteLine(message);

            string[] tokens = message.Split(',');
            int pid = Int32.Parse(Util.getValueFrom(tokens[1]));

            // TODO: FOR NOW LET'S BUILD LAN MODE 
            // @Idea: Should i use this as another process and run it just like when i'm testing 
            // That should be good, since there literally no need to change 
            // so i can keep going with this approach 
            // so the only thing differents is the IP address 
            
            // Trong che do choi LAN 
            // co 2 truong hop: 
            // nguoi tao phong out 
            // -> xoa me rooms  
            /*
                removeRoom(roomId);
                globalData.removeRoom(roomId);
                // thong bao toi tat ca nguoi choi 
                string formatedMessage = string.Format("000000", pid);
                connection.sendToEveryOneElse(pid, formatedMessage, clients);
            */

            // GLOBAL 
            // @Note: Trong che do choi Global thi y chang o duoi 
            // nguoi choi ke out   
            Console.WriteLine("Clients remain: " + clients.Count);
            clients.Remove(pid.ToString());
            Console.WriteLine("Clients after remove: " + clients.Count);
            // send format: "pid:{},s:q" 

            bool noOneInRoom = clients.Count == 0;
            if(noOneInRoom) {
                globalData.removeRoom(roomId);
                Console.WriteLine("No one in room, so remove room with id: " + roomId);
                Console.WriteLine("How many room have left: " + globalData.Rooms.Count);
            } else {
                string formatedMessage = string.Format("pid:{0},s:quit", pid);
                connection.sendToEveryOneElse(pid, formatedMessage, clients);
            }
        }
    }  
}