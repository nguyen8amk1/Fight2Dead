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
            // : gui lai cho thang kia 
            /*
            string[] tokens = message.Split(',');
            string pid = Util.getValueFrom(tokens[0]);
            string cn = Util.getValueFrom(tokens[1]);

            string formatedMessage = string.Format("pid:{0},cn:{1}", pid, cn);

            // send to the rest of the game  
            GameRoom room = Server.rooms[roomId];
            TCPClientConnection.sendToOthers(room.tcpPlayers, player, formatedMessage);
            */

            string[] tokens = message.Split(',');
            int pid = Int32.Parse(Util.getValueFrom(tokens[2]));
            int pn = Int32.Parse(Util.getValueFrom(tokens[1]));
            string charName = Util.getValueFrom(tokens[0]);

            string formatedMessage = string.Format("pid:{0},pn:{1},cn:{2}", pid, pn, charName);

            GameRoom room = Server.rooms[roomId];
            TCPClientConnection.sendToOthers(room.tcpPlayers, player, formatedMessage);
            // connection.sendToEveryOneElse(pid, formatedMessage, clients);
        }

    }
}