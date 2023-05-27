using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System;

namespace SocketServer
{
    public sealed class PlayerMatcher
    {
        private List<Player> unmatchedPlayers = new List<Player>();
        private int roomId = 1;
        private DebugLogger dlog = new DebugLogger();

        public GameRoom match(List<Player> unmatchedPlayers, int playersNum)
        {
            if (unmatchedPlayers.Count >= playersNum)
            {
                Dictionary<string, Player> clients = new Dictionary<string, Player>();
                string tail = "";
                for (int i = 0; i < playersNum; i++)
                {
                    int lastIndex = unmatchedPlayers.Count - i - 1;
                    // Console.WriteLine("last index: " + (lastIndex).ToString());
                    Player player = unmatchedPlayers[lastIndex];
                    player.rid = roomId.ToString();
                    TcpClient client = player.tcpClient;

                    string m = $"p{i+1}:{player.username},";
                    tail += m;
                }

                // remove last char 
                tail = tail.Remove(tail.Length - 1, 1);

                // Create thread for all players at once 
                for (int i = 0; i < playersNum; i++)
                {
                    int lastIndex = unmatchedPlayers.Count - 1;
                    // Console.WriteLine("last index: " + (lastIndex).ToString());
                    Player player = unmatchedPlayers[lastIndex];
                    player.rid = roomId.ToString();
                    player.endPoint = (IPEndPoint)player.tcpClient.Client.RemoteEndPoint;
                    TcpClient client = player.tcpClient;

                    // @Note: send back the rid,pid:
                    string mess = string.Format("rid:{0},pid:{1},{2}", player.rid, player.id, tail);

                    client.Client.Send(Encoding.ASCII.GetBytes(mess));

                    dlog.newConnectionMessageSent(player.tcpClient, 1, mess);


                    //remove the matched players from unmatch list 
                    int index = unmatchedPlayers.Count - 1;
                    // Console.WriteLine("index: " + index);
                    clients.Add(unmatchedPlayers[index].id, unmatchedPlayers[index]);
                    unmatchedPlayers.RemoveAt(index);
                }

                GameRoom room = new GameRoom();

                room.id = roomId.ToString();
                room.playersNum = playersNum;
                room.players = clients;

                roomId++;

                return room;
            }
            return null;
        }
    }
}