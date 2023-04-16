using System;

namespace TestSocket
{
    public class MessageParser 
    {
        public static State parse(string message) {
            bool receiveNewConnection = message.Equals("command:connect");
            bool receivePositionWithId = message.StartsWith("id:");

            if (receiveNewConnection) {
                return State.RECEIVE_NEW_CONNECTION;
            }
            else if (receivePositionWithId)
            {
                return State.RECEIVE_POSITION;
            }

            throw new Exception("Message not regconized");
        }
    }
}