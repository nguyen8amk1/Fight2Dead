using System;
using System.Collections.Generic;

namespace TestSocket {
    public class ChooseStageState : IRoomState {
        private ServerConnection connection = ServerConnection.getInstance();
        public void serve(string message, List<ClientInfo> clients) {
            // receive format: "stg:{},pid:{}"
            // send format: "pid:{},mn:{}"

            // get the name of the map 
            string[] tokens = message.Split();
            string mapName  = Util.getValueFrom(tokens[0]);
            int pid      = Int32.Parse(Util.getValueFrom(tokens[1]));
            string formatedMessage = string.Format("pid:{0},mn:{1}", pid, mapName);

            // send the name of the map to others;
            connection.sendToEveryOneElse(pid, formatedMessage, clients);
        }
    }  
}