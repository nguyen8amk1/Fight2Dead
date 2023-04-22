using System;

namespace TestSocket
{
    public class FromRoomMessageState : IState
    {
        private ServerGlobalData globalData = ServerGlobalData.getInstance();

        public void serve(string message)
        {
            string[] tokens = message.Split(',');
            int rid = Int32.Parse(tokens[0].Split(':')[1]);
            int index = rid - 1;
            message = message.Substring(tokens[0].Length + 1); // remove the rid part from the message

            globalData.Rooms[index].process(message);
        }
    }
}