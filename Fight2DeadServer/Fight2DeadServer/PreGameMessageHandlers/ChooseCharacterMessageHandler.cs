using System.Collections.Generic;
using System.Text;
using System;

namespace SocketServer
{
    public class ChooseCharacterMessageHandler : PreGameMessageHandler
    {
        private DebugLogger dlog = new DebugLogger();
        public void handle(string roomId, Player player, string message)
        {
            // received message: pid:2,cn:1
            string[] tokens = message.Split(',');
            int pid = Int32.Parse(Util.getValueFrom(tokens[2]));
            int pn = Int32.Parse(Util.getValueFrom(tokens[1]));
            string charName = Util.getValueFrom(tokens[0]);

            string formatedMessage = string.Format("pid:{0},pn:{1},cn:{2}", pid, pn, charName);

            GameRoom room = Server.rooms[roomId];
            TCPClientConnection.sendToOthers(room.players, player, formatedMessage);
        }

    }
}