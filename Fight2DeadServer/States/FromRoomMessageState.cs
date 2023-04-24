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
            message = message.Substring(tokens[0].Length + 1); // remove the rid part from the message

            // @Debug: Iterate and print key value pairs 
            foreach(var item in globalData.Rooms) {
                Console.WriteLine("Key: " + item.Key + ", Value: " + item.Value);
            }

            // FIXME: index string is not equal to the key of room  
            globalData.Rooms[rid.ToString()].process(message);
        }
    }
}