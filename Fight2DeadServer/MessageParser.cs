using System;

namespace TestSocket
{
    public class MessageParser 
    {
        public static State parse(string message) {
            bool receiveNewConnection = message.Equals("command:connect");
            bool receivePositionWithId = message.StartsWith("pid:");
            bool receiveRoomPacket = message.StartsWith("rid:");

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

            throw new Exception("Message not regconized");
        }
    }
}