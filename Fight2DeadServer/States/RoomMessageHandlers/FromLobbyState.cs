using System;
using System.Collections.Generic;

namespace GameSocketServer
{
    public class FromLobbyState : IRoomState
    {
        private UDPServerConnection connection = UDPServerConnection.getInstance();
        public void serve(string message, Dictionary<string, ClientInfo> clients, int roomId)
        {
            string[] tokens = message.Split(',');
            int pid = Int32.Parse(Util.getValueFrom(tokens[1]));
            int stat = Int32.Parse(Util.getValueFrom(tokens[2]));

            // @Debug: This is just for debug
            string formatedString = string.Format("pid:{0} va stat:{1}", pid, stat);
            Console.WriteLine(formatedString);

            string formatedMessage = string.Format("pid:{0},stat:{1}", pid, stat);

            connection.sendToEveryOneElse(pid, formatedMessage, clients);
        }
    }
}