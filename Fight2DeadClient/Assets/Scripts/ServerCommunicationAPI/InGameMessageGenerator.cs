using System;

namespace SocketServer {
    public class InGameMessageGenerator {
        // TODO: how many message types 
        private static GameState globalGameState = GameState.Instance;
        public static string tempInGameMessage(float playerX, float playerY, int animationState, int currentChar, string dame, string respawn) {
            // format: rid:{0},pid:{1},x:{2},y:{3},{moveLeft},{moveRight};
            // -> compact to only number -> 0,1,2,3
            return $"{globalGameState.RoomId},{globalGameState.PlayerId},{playerX},{playerY},{animationState},{currentChar},{dame},{respawn},0";
        }
    } 
}