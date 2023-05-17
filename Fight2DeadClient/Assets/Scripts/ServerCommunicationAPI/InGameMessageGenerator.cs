using System;

namespace SocketServer {
    public class InGameMessageGenerator {
        // TODO: how many message types 
        private static GameState globalGameState = GameState.Instance;
        public static string tempInGameMessage(float playerX, float playerY) {
            return String.Format("rid:{0},pid:{1},x:{2},y:{3}", globalGameState.RoomId, globalGameState.PlayerId, playerX, playerY);
        }
    } 
}