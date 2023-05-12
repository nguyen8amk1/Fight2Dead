using System;

namespace SocketServer {
    public sealed class MessageHandlerFactory {
        public PreGameMessageHandler whatPreGameMessage(string message) {
            if(message.StartsWith("pid:")) {
                return new LobbyReadyMessageHandler();
            }

            if (message == "toudp")
            {
                return new ToUDPMessageHandler();
            }
            if (message == "quit")
            {
                return new QuitMessageHandler();
            }
            throw new Exception(String.Format("Unrecognize message!!! - {0}", message));
        }
    }
}