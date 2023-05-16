using System;

namespace SocketServer {
    public sealed class MessageHandlerFactory {
        public PreGameMessageHandler whatPreGameMessage(string message) {
            string[] tokens = message.Split(',');

            bool isLobbyMessage = 
                Util.getKeyFrom(tokens[0]) == "pid" && 
                Util.getKeyFrom(tokens[1]) == "stat";

            if(isLobbyMessage) {
                return new LobbyReadyMessageHandler();
            }

            bool isChooseCharacterMessage = 
                // Util.getKeyFrom(tokens[0]) == "pid" && 
                // Util.getKeyFrom(tokens[1]) == "cn";
                Util.getKeyFrom(tokens[0]) == "cn" && 
                Util.getKeyFrom(tokens[1]) == "pn" && 
                Util.getKeyFrom(tokens[2]) == "pid";

            if(isChooseCharacterMessage) {
                return new ChooseCharacterMessageHandler();
            }

            bool isChooseMapMessage = 
                Util.getKeyFrom(tokens[0]) == "pid" && 
                Util.getKeyFrom(tokens[1]) == "stg";

            if(isChooseMapMessage) {
                return new ChooseMapMessageHandler();
            }

            bool isUserRegistrationMessage = 
                Util.getKeyFrom(tokens[0]) == "email" && 
                Util.getKeyFrom(tokens[1]) == "password";

            if(isUserRegistrationMessage) {
				return new UserRegistrationMessageHandler();
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