using System;
using System.Collections.Generic;

namespace TestSocket {
    public class CloseConnectionState : IRoomState {
        private ServerConnection connection = ServerConnection.getInstance();
        public void serve(string message, List<ClientInfo> clients) {
            // receive format: "quit,pid:{}"
            Console.WriteLine(message);

            string[] tokens = message.Split(',');
            int pid      = Int32.Parse(Util.getValueFrom(tokens[1]));

            // send format: "pid:{},quit"
            string formatedMessage = string.Format("pid:{0},st:quit", pid);

            // send the name of the map to others;
            connection.sendToEveryOneElse(pid, formatedMessage, clients);
        }
    }  
}