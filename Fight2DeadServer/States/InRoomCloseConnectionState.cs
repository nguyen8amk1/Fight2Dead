using System;
using System.Collections.Generic;

namespace TestSocket {
    public class InRoomCloseConnectionState : IRoomState {
        private ServerConnection connection = ServerConnection.getInstance();

        // thoi diem nhan connection truoc khi vao scene matching  
        // -> phai bat quit 

        // Menu -> Matching -> Lobby -> Choose character -> Choose man choi -> Loading Screen -> game 
        // -> starting from CHOOSE CHARACTER scene 
        // we gonna have quit message send if the player close the game 

        public void serve(string message, List<ClientInfo> clients) {
            // receive format: "stg:q,pid:{}"
            Console.WriteLine(message);

            string[] tokens = message.Split(',');
            int pid = Int32.Parse(Util.getValueFrom(tokens[1]));

            // send format: "pid:{},s:q" 
            string formatedMessage = string.Format("pid:{0},s:q", pid);

            // TODO: sau khi tim duoc team (da duoc dua vao room) (sau khi da matching)

            // co 2 truong hop: 
            // nguoi tao phong out 
            // -> xoa me rooms  
            // thong bao toi tat ca nguoi choi 
            // -> phai biet index phong  :)) -> doi qua hashmap :)) 

            // nguoi choi ke out   
            // -> khong xoa room, thong bao den moi nguoi

            // send the name of the map to others;
            connection.sendToEveryOneElse(pid, formatedMessage, clients);
        }
    }  
}