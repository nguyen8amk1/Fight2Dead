namespace SocketServer {
    public sealed class PreGameMessageGenerator {
        // TODO: how many message types 
        private static GameState globalGameState = GameState.Instance; 
        public static string numPlayersMessage(int numPlayers) {
            return "numsPlayer:" + numPlayers;
        }

        public static string chooseMapMessage() {
            return null;
        }

        public static string chooseCharacterMessage() {
            return null;
        }

        public static string lobbyReadyMessage(bool stat) {
            int st = stat ? 1 : 0;
            return $"pid:{globalGameState.PlayerId},stat:{st}";
        }

        public static string toUDPMessage() {
            return "toudp";
        }

    } 
}