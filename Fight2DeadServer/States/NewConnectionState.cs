using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace TestSocket
{
    public class NewConnectionState : IState
    {
        private ServerConnection connection = ServerConnection.getInstance();
        private ServerGlobalData globalData = ServerGlobalData.getInstance();

        public void serve(string message) {
            // TODO: 
            Console.WriteLine("Connection Received, player id is " + globalData.ClientId);

            ClientInfo client = new ClientInfo(globalData.ClientId, globalData.NewlyAddedClientRemoteIPEndPoint);
            globalData.ClientId++;
            globalData.UnmatchedClients.Add(client);

            playerMatching();
        }

        private void playerMatching() {
            if(globalData.UnmatchedClients.Count >= 2) {
                int lastIndex = globalData.UnmatchedClients.Count - 1;
                List<ClientInfo> clients = new List<ClientInfo> { 
                    globalData.UnmatchedClients[lastIndex -1], 
                    globalData.UnmatchedClients[lastIndex]};

                GameRoom room = new GameRoom(globalData.RoomId, clients);
                globalData.Rooms.Add(room);

                // send packet with rid to both clients
                foreach(ClientInfo c in clients) {
                    string formatedString = string.Format("rid:{0},pid:{1}", globalData.RoomId, c.id);
                    connection.sendToClient(formatedString, c.endPoint);
                }

                globalData.RoomId++;

                // remove the matched clients 
                globalData.UnmatchedClients.RemoveAt(globalData.UnmatchedClients.Count -1);
                globalData.UnmatchedClients.RemoveAt(globalData.UnmatchedClients.Count -1);

                // wrap the clientId part 
                globalData.ClientId = 1;
            } else {
                ClientInfo lastestClient = globalData.UnmatchedClients[globalData.UnmatchedClients.Count -1];
                string formatedString = string.Format("You're id= {0} waiting for another player to match with you!!!", lastestClient.id);
                connection.sendToClient(formatedString, lastestClient.endPoint);
            }
        }
    }
}