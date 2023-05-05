using System.Collections.Generic;
using System;

namespace GameSocketServer
{
    public class PositionMessageState : IRoomState
    {
        private UDPServerConnection connection = UDPServerConnection.getInstance();
        public void serve(string message, Dictionary<string, ClientInfo> clients, int roomId)
        {
            // send to players 2, player1's pos 
            // state: receive position
            string[] tokens = message.Split(':');
            int pid = Int32.Parse(Util.getValueFrom(tokens[1]));
            string[] xy = tokens[2].Split(',');
            string x = xy[0];
            string y = xy[1];

            string formatedString = string.Format("pid:{0},x:{1},y:{2}", pid, x, y);
            connection.sendToEveryOneElse(pid, formatedString, clients);
        }
    }
}