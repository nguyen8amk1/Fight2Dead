using System;

namespace SocketServer {
    public sealed class PreGameMessageGenerator {
        // TODO: how many message types 
        private static GameState globalGameState = GameState.Instance; 
        public static string matchingSceneMessage(int numPlayers) {
            return $"numsPlayer:{numPlayers},username:{globalGameState.username}\n";
        }

        public static string chooseMapMessage(string mapName) {
            return $"pid:{globalGameState.PlayerId},stg:{mapName}\n";
        }

        public static string chooseCharacterMessage(int pn, string charName) {
            // return $"pid:{globalGameState.PlayerId},cn:{charName}";
            return $"cn:{charName},pn:{pn},pid:{globalGameState.PlayerId}\n";
        }

        public static string lobbyReadyMessage(bool stat) {
            int st = stat ? 1 : 0;
            return $"pid:{globalGameState.PlayerId},stat:{st}\n";
        }

        public static string toUDPMessage() {
            return "toudp\n";
        }

		public static string quitMessage()
		{
            return "quit\n";
		}

		public static string userRegistrationMessage(string email, string username, string password)
		{
            return $"email:{email},username:{username},password:{password}\n";
		}

		public static string userLoginMessage(string username, string password)
		{
            return $"username:{username},password:{password}\n";
		}
	} 
}