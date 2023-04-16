using System;

namespace TestSocket
{
    public class MessageParser 
    {
        public static State parse(string message) {
            bool receiveNewConnection = message.Equals("command:connect");
            bool receivePositionWithId = message.StartsWith("pid:");
            bool receiveRoomPacket = message.StartsWith("rid:");
            bool receiveFromLobby = message.StartsWith("s:");

            if (receiveNewConnection) {
                return State.RECEIVE_NEW_CONNECTION;
            }
            else if (receivePositionWithId)
            {
                return State.RECEIVE_POSITION;
            }
            else if (receiveRoomPacket)
            {
                return State.RECEIVE_ROOM_PACKET;
            }
            else if (receiveFromLobby)
            {
                return State.RECEIVE_FROM_LOBBY;
            }

            throw new Exception("Message not regconized");
        }

        public static string getValueFrom(string token) {
            return token.Split(':')[1];
        } 
    }
}