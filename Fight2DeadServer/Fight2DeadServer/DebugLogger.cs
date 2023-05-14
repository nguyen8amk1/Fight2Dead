using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace SocketServer
{
    /*
        format: 
        new message received event:    "Client {A,B,C..} to Server (Phase {} - {Protocol})" 
        new message send event:        "Server to Client {A,B,C,..} (Phase {} - {Protocol})"

        message received event:    "Client (id {}) to Server (Phase {} - {Protocol})" 
        message send event:        "Server to Client (id {}) (Phase {} - {Protocol})"

        room created event:        "TCP Room (id {}) get created" -> Phase 1 event 
        switch to udp event:       "Move TCP room (id {}) to UDP room (id {})" -> Phase 2 event  
    */

    public sealed class DebugLogger
    {
        private static Dictionary<TcpClient, string> tempNames = new Dictionary<TcpClient, string>();

        public void newConnectionMessageReceived(TcpClient tcpClient, int phase, string message)
        {
            tempNames.Add(tcpClient, generateRandomString());
            string protocol = whatProtocol(phase);
            Console.WriteLine("Client {0} to Server (Phase {1} - {2}): {3}", tempNames[tcpClient], phase, protocol, message);
        }

        public void newConnectionMessageSent(TcpClient tcpClient, int phase, string message)
        {
            string protocol = whatProtocol(phase);
            Console.WriteLine("Server to Client {0} (Phase {1} - {2}): {3}", tempNames[tcpClient], phase, protocol, message);
        }

        public void messageReceived(string id, int phase, string message)
        {
            string protocol = whatProtocol(phase);
            Console.WriteLine("Client (id {0}) to Server (Phase {1} - {2}): {3}", id, phase, protocol, message);
        }

        public void messageSent(string id, int phase, string message)
        {
            string protocol = whatProtocol(phase);
            Console.WriteLine("Server to Client (id {0}) (Phase {1} - {2}): {3}", id, phase, protocol, message);

        }
        public void roomCreated(GameRoom room)
        {
            Console.WriteLine("TCP Room (id {0}) get created", room.id);
        }

        public void switchToUDP(string tcpRoomId)
        {
            Console.WriteLine("Move TCP room (id {0}) to UDP room (id {1})", tcpRoomId, tcpRoomId);
        }

        private string generateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string whatProtocol(int phase)
        {
            string protocol = "TCP";
            if (phase == 3)
            {
                protocol = "UDP";
            }
            return protocol;
        }
    }
}