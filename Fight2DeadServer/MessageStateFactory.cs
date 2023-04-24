using System;

namespace TestSocket {
    public class MessageStateFactory {
        public IState createMessageState(string message) {
            bool receiveNewConnection = message.Equals("command:connect");
            bool receiveRoomPacket = message.StartsWith("rid:");
            bool receiveQuitGameMessage = message.StartsWith("quit");

            if (receiveNewConnection) {
                return new NewConnectionState();
            }
            else if (receiveRoomPacket)
            {
                return new FromRoomMessageState();
            }
            else if (receiveQuitGameMessage)
            {
                return new CloseConnectionWhenMatchingState();
            }

            throw new Exception("Message not regconized");
        }  

        public IRoomState createMessageRoomState(string message) {
            bool receiveQuitGameMessage = message.StartsWith("quit");
            bool receivePositionWithId = message.StartsWith("pid:");
            bool receiveFromLobby = message.StartsWith("s:l");
            bool receiveChosenCharacterInfo = message.StartsWith("s:ch");
            bool receiveChosenStageInfo = message.StartsWith("stg:");

            // TODO: we should remove the start with part as well 

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
            else if (receiveQuitGameMessage)
            {
                return new InRoomCloseConnectionState();
            }

            throw new Exception("Message not regconized");
        }
    }
}