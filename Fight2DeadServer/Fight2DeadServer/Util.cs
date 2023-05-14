namespace SocketServer {
    public sealed class Util {
        public static string getValueFrom(string token) {
            return token.Split(':')[1];
        }
        public static string getKeyFrom(string token) {
            return token.Split(':')[0];
        }
    }
}