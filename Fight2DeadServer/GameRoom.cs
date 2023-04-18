using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

// all the scenes we have to go through: 
// Menu -> Matching -> Lobby -> Choose character -> Choose man choi -> Loading Screen -> game 

namespace TestSocket
{
    public class GameRoom
    {
        private List<ClientInfo> clients;

        private MessageStateFactory factory = new MessageStateFactory();
        private ServerConnection connection = ServerConnection.getInstance();

        public GameRoom(int roomId, List<ClientInfo> clients) {
            this.clients = clients;
        }

        public void process(string message) {
            IRoomState messageState = factory.createMessageRoomState(message);
            messageState.serve(message, clients);
        }

    }
}