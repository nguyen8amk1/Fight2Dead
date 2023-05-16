using System;

namespace SocketServer {
    public sealed class PreGameMessageGenerator {
        // TODO: how many message types 
        private static GameState globalGameState = GameState.Instance; 
        public static string numPlayersMessage(int numPlayers) {
            return "numsPlayer:" + numPlayers;
        }

        public static string chooseMapMessage(string mapName) {
            return $"pid:{globalGameState.PlayerId},stg:{mapName}";
        }

        public static string chooseCharacterMessage(int pn, string charName) {
            // return $"pid:{globalGameState.PlayerId},cn:{charName}";
            return $"cn:{charName},pn:{pn},pid:{globalGameState.PlayerId}";
        }

        public static string lobbyReadyMessage(bool stat) {
            int st = stat ? 1 : 0;
            return $"pid:{globalGameState.PlayerId},stat:{st}";
        }

        public static string toUDPMessage() {
            return "toudp";
        }

		public static string quitMessage()
		{
            return "quit";
		}

		public static string userRegistrationMessage(string email, string password)
		{
            return $"username:{"tempUsername"},email:{email},password:{password}";
		}

		public static string userLoginMessage(string username, string password)
		{
            return $"username:{username},password:{password}";
		}
	} 
}