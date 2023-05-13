using System;
using System.Text.RegularExpressions;

namespace SocketServer {
    public sealed class ListenToServerFactory {

        public delegate void MessageHandlerLambda(string[] tokens);
        private static GameState gameState = GameState.Instance; 

        public static MessageHandlerLambda tempTCPListening()  {
            MessageHandlerLambda messageHandler = (string[] tokens) => {
                // TODO: have a factory for each message type just like in the server
                // have a regex for each message 
                bool isRidPid = Util.getKeyFrom(tokens[0]) == "rid" && Util.getKeyFrom(tokens[1]) == "pid";
                if (isRidPid)
                {
					gameState.RoomId = Int32.Parse(Util.getValueFrom(tokens[0]));
					gameState.PlayerId = Int32.Parse(Util.getValueFrom(tokens[1]));
                    gameState.receiveRidPid = true;
                }

                bool isLobbyReadyMessage = Util.getKeyFrom(tokens[0]) == "pid" && Util.getKeyFrom(tokens[1]) == "stat";
                if(isLobbyReadyMessage)
				{
					int stat = Int32.Parse(Util.getValueFrom(tokens[1]));
                    LobbyGetState.count = 0; 
					gameState.opponentReady = stat == 1;
				}
            };
            return messageHandler;
        }

        public static MessageHandlerLambda tempUDPListening()  {
            MessageHandlerLambda messageHandler = (string[] tokens) => {
                // TODO: 
                Console.WriteLine("TEMP UDP LISTENING");
            };
            return messageHandler;
        }
    } 
}