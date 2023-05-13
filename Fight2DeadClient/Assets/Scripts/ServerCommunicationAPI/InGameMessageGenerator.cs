using System;

namespace SocketServer {
    public class InGameMessageGenerator {
        // TODO: how many message types 
        private static GameState globalGameState = GameState.Instance;
        public static string tempInGameMessage() {
            return String.Format("rid:{0},pid:{1}", globalGameState.RoomId, globalGameState.PlayerId);
        }
    } 
}