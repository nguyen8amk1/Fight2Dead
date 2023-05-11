using System;

namespace SocketServer {
    public sealed class ListenToServerFactory {
        public delegate void MessageHandlerLambda(string[] tokens);
        public static MessageHandlerLambda tempTCPListening()  {
            MessageHandlerLambda messageHandler = (string[] tokens) => {
                // TODO: 
                Console.WriteLine("TEMP TCP LISTENING");
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