
using System.Collections.Generic;

namespace GameSocketServer
{
    public interface IRoomState {
        void serve(string message, Dictionary<string, ClientInfo> clients, int roomId);
    }
}

    