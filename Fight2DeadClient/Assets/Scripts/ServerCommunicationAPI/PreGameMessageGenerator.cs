namespace SocketServer {
    public sealed class PreGameMessageGenerator {
        // TODO: how many message types 
        public static string numPlayersMessage(int numPlayers) {
            return "numsPlayer:" + numPlayers;
        }

        public static string chooseMapMessage() {
            return null;
        }

        public static string chooseCharacterMessage() {
            return null;
        }

        public static string chooseLobbyReadyMessage() {
            return null;
        }

        public static string toUDPMessage() {
            return "toudp";
        }

    } 
}