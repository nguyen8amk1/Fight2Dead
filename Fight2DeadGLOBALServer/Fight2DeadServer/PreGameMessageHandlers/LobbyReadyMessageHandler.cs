using System.Collections.Generic;
using System.Text;

namespace SocketServer
{
    public class LobbyReadyMessageHandler : PreGameMessageHandler
    {
        private DebugLogger dlog = new DebugLogger();
        public void handle(string roomId, Player player, string message)
        {
            // received message: pid:2,stat:1
            // : gui lai cho thang kia 
            string[] tokens = message.Split(',');
            string pid = Util.getValueFrom(tokens[0]);
            string stat = Util.getValueFrom(tokens[1]);

            string formatedMessage = string.Format("pid:{0},stat:{1}", pid, stat);

            // send to the rest of the game  
            GameRoom room = Server.rooms[roomId];
            TCPClientConnection.sendToOthers(room.players, player, formatedMessage);
        }

    }
}