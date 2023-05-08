using System;
using System.Collections.Generic;

namespace GameSocketServer
{
    public class ChooseCharacterState : IRoomState
    {
        private ServerConnection connection = ServerConnection.getInstance();

        public void serve(string message, Dictionary<string, ClientInfo> clients, int roomId)
        {
            // format: $"cn:{},pn:{},pid:{}";
            string[] tokens = message.Split(',');
            int pid = Int32.Parse(Util.getValueFrom(tokens[2]));
            int pn = Int32.Parse(Util.getValueFrom(tokens[1]));
            string charName = Util.getValueFrom(tokens[0]);

            string formatedMessage = string.Format("pid:{0},pn:{1},cn:{2}", pid, pn, charName);

            connection.sendToEveryOneElse(pid, formatedMessage, clients);
        }
    }
}