using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

// all the scenes we have to go through: 
// Menu -> Matching -> Lobby -> Choose character -> Choose man choi -> Loading Screen -> game 

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
                case State.RECEIVE_FROM_LOBBY: {
                    // TODO: may be refactor this into a general function 

                    string[] tokens = command.Split(',');
                    int pid = Int32.Parse(MessageParser.getValueFrom(tokens[1]));
                    int stat = Int32.Parse(MessageParser.getValueFrom(tokens[2]));

                    string formatedString = string.Format("pid:{0} va stat:{1}", pid, stat);
                    Console.WriteLine(formatedString);

                    string formatedMessage = string.Format("pid:{0},stat:{1}", pid, stat);

                    sendToEveryOneElse(pid, formatedMessage);
                } 
                break;

                case State.RECEIVE_CHOOSE_CHARACTER_INFO: {
                    // format: $"s:ch,pid:{pid},ch:{charName}";
                    string[] tokens = command.Split(',');
                    int pid = Int32.Parse(MessageParser.getValueFrom(tokens[1]));
                    string charName = MessageParser.getValueFrom(tokens[2]);

                    string formatedMessage = string.Format("pid:{0},cn:{1}", pid, charName);

                    sendToEveryOneElse(pid, formatedMessage);
                } 
                break;

                case State.RECEIVE_CHOOSE_STAGE_INFO: {
                    // TODO: handle room choice for each player
                } 
                break;

                // @FIXME: NOT WORKING AT THE MOMENT 
                case State.RECEIVE_POSITION:
                {
                    // send to players 2, player1's pos 
                    // state: receive position
                    string[] tokens = command.Split(':');
                    string[] xy = tokens[2].Split(',');
                    string x = xy[0];
                    string y = xy[1];

                    string formatedString = string.Format("{0},{1}", x, y);

                    sendToEveryOneElse(1, formatedString);
                    break;
                }
                default:
                    throw new Exception("SOME FUCKING RANDOM STATES IS APPEAR");
            }
        }

        private void sendToEveryOneElse(int pid, string message) {
            foreach(ClientInfo c in clients) {
                if(c.id == pid) 
                    continue;
                sendToClient(message, c.endPoint);
            }
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