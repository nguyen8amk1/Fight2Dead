using System.Collections.Generic;

namespace TestSocket
{
    public interface IState {
        void serve(string message);
    }
    
    public interface IRoomState {
        void serve(string message, Dictionary<string, ClientInfo> clients, int roomId);
    }
}
