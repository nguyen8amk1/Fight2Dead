using System;
using System.Collections.Generic;

namespace TestSocket
{
    public class ChooseCharacterState : IRoomState
    {
        private ServerConnection connection = ServerConnection.getInstance();

        public void serve(string message, List<ClientInfo> clients)
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