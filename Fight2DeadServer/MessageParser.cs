using System;

namespace TestSocket
{
    public class MessageParser 
    {
        public static State parse(string message) {
            // RIGHT NOW THERE ARE 2 KINDS OF MESSAGE
            // server message: prefix - "command:{command}"
            // room message: prefix - "rid:{rid}"
            // in room message there are different kinds of message as well: 
            // message from different scene: 
            // lobby, choose character, choose map, game  

            // GOAL: 
            // each time there is a new state, the only thing i need to do is
            // USE FACTORY METHOD DESIGN PATTERN 

            // each state for handle each message gonna be a class 

            Console.WriteLine("About to parse message: " + message);

            bool receiveNewConnection = message.Equals("command:connect");
            bool receivePositionWithId = message.StartsWith("pid:");
            bool receiveRoomPacket = message.StartsWith("rid:");

            bool receiveFromLobby = message.StartsWith("s:l");

            // TODO: figure out how to do this 
            bool receiveChosenCharacterInfo = message.StartsWith("s:ch");
            bool receiveChosenStageInfo = message.StartsWith("stg:");

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
            else if (receiveChosenCharacterInfo)
            {
                Console.WriteLine("This is character info");
                return State.RECEIVE_CHOOSE_CHARACTER_INFO;
            }
            else if (receiveChosenStageInfo)
            {
                return State.RECEIVE_CHOOSE_STAGE_INFO;
            }

            throw new Exception("Message not regconized");
        }

        public static string getValueFrom(string token) {
            return token.Split(':')[1];
        } 
    }
}