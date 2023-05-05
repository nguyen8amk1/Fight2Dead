using System;
using System.Collections.Generic;

namespace GameSocketServer {
    public class CloseConnectionWhenMatchingState : IState {
        private ServerConnection connection = ServerConnection.getInstance();
        private ServerGlobalData globalData = ServerGlobalData.getInstance();

        public void serve(string message) {
            // receive format: "quit"
            string formatedString = string.Format("Player {0} don't want to match any more", globalData.ClientId-1);
            Console.WriteLine(formatedString);
            globalData.removeLastUnmatchedClient();
            globalData.ClientId -= 1;
        }
    }  
}