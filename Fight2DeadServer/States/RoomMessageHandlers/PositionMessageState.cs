using System.Collections.Generic;

namespace GameSocketServer
{
    public class PositionMessageState : IRoomState
    {
        private ServerConnection connection = ServerConnection.getInstance();
        public void serve(string message, Dictionary<string, ClientInfo> clients, int roomId)
        {
            // send to players 2, player1's pos 
            // state: receive position
            string[] tokens = message.Split(':');
            string[] xy = tokens[2].Split(',');
            string x = xy[0];
            string y = xy[1];

            string formatedString = string.Format("{0},{1}", x, y);

            connection.sendToEveryOneElse(1, formatedString, clients);
        }
    }
}