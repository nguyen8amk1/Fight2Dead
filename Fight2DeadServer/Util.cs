namespace GameSocketServer {
    public class Util {
        public static string getValueFrom(string token) {
            return token.Split(':')[1];
        } 
    }
}