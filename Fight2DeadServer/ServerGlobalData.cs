using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace TestSocket
{
    public sealed class ServerGlobalData
    {
        // @Remind: this class is only for single thread processing only 
        // need to find some other way to do multhreading 

        // single threaded data 
        private int clientId = 1;
        private int roomId = 1;
        private List<ClientInfo> unmatchedClients = new List<ClientInfo>();
        private List<GameRoom> rooms = new List<GameRoom>();
        private IPEndPoint newlyAddedClientRemoteIPEndPoint;

        private static ServerGlobalData instance = null;

        private ServerGlobalData()
        {
        }

        public static ServerGlobalData getInstance()
        {
            if (instance == null)
            {
                instance = new ServerGlobalData();
            }
            return instance;
        }

        // Public properties to access and modify the fields
        public int ClientId
        {
            get
            {
                return clientId;
            }
            set
            {
                clientId = value;
            }
        }

        public int RoomId
        {
            get
            {
                return roomId;
            }
            set
            {
                roomId = value;
            }
        }

        public List<ClientInfo> UnmatchedClients
        {
            get
            {
                return unmatchedClients;
            }
            set
            {
                unmatchedClients = value;
            }
        }

        public List<GameRoom> Rooms
        {
            get
            {
                return rooms;
            }
            set
            {
                rooms = value;
            }
        }

        public void removeLastUnmatchedClient() {
            int lastIndex = unmatchedClients.Count - 1;
            unmatchedClients.RemoveAt(lastIndex);
        }

        public IPEndPoint NewlyAddedClientRemoteIPEndPoint
        {
            get
            {
                return newlyAddedClientRemoteIPEndPoint;
            }
            set
            {
                newlyAddedClientRemoteIPEndPoint = value;
            }
        }
    }
}