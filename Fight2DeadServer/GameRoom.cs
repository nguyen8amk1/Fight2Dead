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
        private Dictionary<string, ClientInfo> clients;

        private MessageStateFactory factory = new MessageStateFactory();
        private ServerConnection connection = ServerConnection.getInstance();
        private int roomId; 

        public GameRoom(int roomId, Dictionary<string, ClientInfo> clients) {
            this.clients = clients;
            this.roomId = roomId;
        }

        // RIGHT NOW THERE ARE 2 KINDS OF MESSAGE
        // server message: prefix - "command:{command}" -> com:{command}

        // room message: prefix - "rid:{rid}" -> rid:{rid}
        // in room message there are different kinds of message as well: 
        // message from different scene: 
        // lobby, choose character, choose map, game  

        public void process(string message) {
            IRoomState messageState = factory.createMessageRoomState(message);
            messageState.serve(message, clients, roomId);
        }

    }
}
