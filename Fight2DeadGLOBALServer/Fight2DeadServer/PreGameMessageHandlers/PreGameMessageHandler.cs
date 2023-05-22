namespace SocketServer {
    public interface PreGameMessageHandler {
        void handle(string roomId, Player player, string message); 
    }
} 