using System;

namespace TestSocket {
    public class MessageStateFactory {
        public IState createMessageState(string message) {
            bool receiveNewConnection = message.Equals("command:connect");
            bool receiveRoomPacket = message.StartsWith("rid:");

            if (receiveNewConnection) {
                return new NewConnectionState();
            }
            else if (receiveRoomPacket)
            {
                return new FromRoomMessageState();
            }

            throw new Exception("Message not regconized");
        }  

        public IRoomState createMessageRoomState(string message) {
            bool receivePositionWithId = message.StartsWith("pid:");
            bool receiveFromLobby = message.StartsWith("s:l");
            bool receiveChosenCharacterInfo = message.StartsWith("s:ch");
            bool receiveChosenStageInfo = message.StartsWith("stg:");

            if (receiveFromLobby)
            {
                return new FromLobbyState();
            }
            else if (receiveChosenCharacterInfo)
            {
                return new ChooseCharacterState();
            }
            else if (receiveChosenStageInfo)
            {
                return new ChooseStageState();
            }
            else if (receivePositionWithId)
            {
                return new PositionMessageState();
            }

            throw new Exception("Message not regconized");
        }
    }
}