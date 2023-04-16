using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

namespace TestSocket
{
    public class GameRoom
    {
        private List<ClientInfo> clients;
        private UdpClient listener; 
        private int id;

        public GameRoom(int roomId, List<ClientInfo> clients, UdpClient listener) {
            this.clients = clients;
            this.listener = listener;
            this.id = roomId; 
        }

        State currentState = State.NEW;
        public void process(string command) {
            currentState = nextState(command);

            switch (currentState)
            {
                // TODO: handle the lobby scene message

                case State.RECEIVE_POSITION:
                {
                    // send to players 2, player1's pos 
                    // state: receive position
                    string[] tokens = command.Split(':');
                    string[] xy = tokens[2].Split(',');
                    string x = xy[0];
                    string y = xy[1];

                    // TODO: this can be refactor even more 
                    if (command.StartsWith("id:1"))
                    {
                        sendPosToClientWithId(2, x, y);
                    }
                    else if (command.StartsWith("id:2"))
                    {
                        sendPosToClientWithId(1, x, y);
                    }
                    break;
                }
                default:
                    throw new Exception("SOME FUCKING RANDOM STATES IS APPEAR");
            }
        }

        private void sendPosToClientWithId(int id, string x, string y) {
            string formatString = string.Format("{0},{1}", x, y);
            sendToClient(formatString, clients[id-1].endPoint);
        }

        private State nextState(string command) {
            return MessageParser.parse(command);
        }

        private void sendToClient(string message, IPEndPoint endPoint)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            Console.WriteLine("Sending to " + endPoint.Address.ToString() + ":" + endPoint.Port);

            // this is the problem
            listener.Send(bytes, bytes.Length, endPoint.Address.ToString(), endPoint.Port);
        }
    }
}