
using System.Collections.Generic;

namespace TestSocket
{
    public interface IRoomState {
        void serve(string message, Dictionary<string, ClientInfo> clients, int roomId);
    }
}

    