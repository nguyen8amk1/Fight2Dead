using System;
using System.Collections.Generic;

namespace GameSocketServer
{
    public class ChooseCharacterState : IRoomState
    {
        private UDPServerConnection connection = UDPServerConnection.getInstance();

        public void serve(string message, Dictionary<string, ClientInfo> clients, int roomId)
        {
            // format: $"s:ch,pid:{pid},ch:{charName}";
            string[] tokens = message.Split(',');
            int pid = Int32.Parse(Util.getValueFrom(tokens[1]));
            string charName = Util.getValueFrom(tokens[2]);

            string formatedMessage = string.Format("pid:{0},cn:{1}", pid, charName);

            connection.sendToEveryOneElse(pid, formatedMessage, clients);
        }
    }
}