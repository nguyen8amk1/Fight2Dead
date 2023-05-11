using System;

namespace SocketServer {
    public class InGameMessageGenerator {
        // TODO: how many message types 
        public static string tempInGameMessage(string rid, string pid) {
            return String.Format("rid:{0},pid:{1}", rid, pid);
        }
    } 
}