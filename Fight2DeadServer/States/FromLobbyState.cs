using System;
using System.Collections.Generic;

namespace TestSocket
{
    public class FromLobbyState : IRoomState
    {
        private ServerConnection connection = ServerConnection.getInstance();
        public void serve(string message, List<ClientInfo> clients)
        {
            string[] tokens = message.Split(',');
            int pid = Int32.Parse(Util.getValueFrom(tokens[1]));
            int stat = Int32.Parse(Util.getValueFrom(tokens[2]));

            string formatedString = string.Format("pid:{0} va stat:{1}", pid, stat);
            Console.WriteLine(formatedString);

            string formatedMessage = string.Format("pid:{0},stat:{1}", pid, stat);

            connection.sendToEveryOneElse(pid, formatedMessage, clients);
        }
    }
}