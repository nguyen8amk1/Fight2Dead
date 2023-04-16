using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;


namespace TestSocket
{
    public class Server
    {
        private const int listeningPort = 8080;
        private UdpClient listener = new UdpClient(listeningPort);
        private List<ClientInfo> unmatchedClients = new List<ClientInfo>();
        private State currentState = State.NEW;
        private int clientId = 1;
        private int roomId = 1;

        private List<GameRoom> rooms = new List<GameRoom>();

        // TODO: PLAYER MATCHING
        // con che do 2 nguoi choi, 4 nguoi choi 
        // hien tai dang lam che do global
        // list tong nguoi choi 
        // TINH HINH HIEN TAI: 
        // 1 phong choi duy nhat 
        // 2 nguoi choi

        // Goal: 
        // hien tai chi la che do 2 nguoi 
        // hien tai chi co 1 phong 
        // neu du 2 nguoi roi -> luu 2 nguoi vao phong 

        // moi phong la 1 thread rieng 
        // nguoi choi an nut online -> cho nguoi choi 2 
        // tim duoc nguoi choi con lai -> ca 2 cung vao lobby -> 2 nguoi dc luu vao phong rieng 

        // so this server is only for player matching into room 

        // STEPS
        // luu tat ca nguoi connect vao list 
        // thuc hien matching  (dung bat ki thuat toan nao)
        // lay ra 2 nguoi trong list dua vao room 
        // sau do chay room trong thread rieng 

        // REFACTOR: 
        // move this into a room  
        // tao client moi thi van la server 
        // game room bat dau tu lobby -> game (nhan ready nay kia, chon nhan vat, position)
        // -> van la single thread

        public Server()
        {
        }

        public void go()
        {
            while (true)
            {
                Console.WriteLine("Server is listening...");
                IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] bytes = listener.Receive(ref remoteIPEndPoint);
                string command = Encoding.ASCII.GetString(bytes);
                
                currentState = nextState(command);

                switch (currentState)
                {
                    case State.RECEIVE_NEW_CONNECTION:
                    {
                        Console.WriteLine("Connection Received, player id is " + clientId);
                        ClientInfo client = new ClientInfo(clientId, remoteIPEndPoint);
                        clientId++;
                        unmatchedClients.Add(client);

                        playerMatching();
                    }
                    break;
                    case State.RECEIVE_ROOM_PACKET: {
                        // get roomid 
                        // index = roomid -1 
                        // remove the room part in the command string 
                        Console.WriteLine("Receive Room packet");
                        // int rid = ;
                        // int index = rid - 1;
                        // rooms[index].process(command);
                    } 
                    break;
                    default:
                        throw new Exception("SOME FUCKING RANDOM STATES IS APPEAR");
                }

                Console.WriteLine(currentState);
            }
        }

        private void playerMatching() {
            if(unmatchedClients.Count >= 2) {
                int lastIndex = unmatchedClients.Count - 1;
                List<ClientInfo> clients = new List<ClientInfo> { unmatchedClients[lastIndex -1], unmatchedClients[lastIndex]};
                GameRoom room = new GameRoom(roomId, clients, listener);
                rooms.Add(room);

                // send packet with rid to both clients
                string formatedString = string.Format("rid:{0},pid{1}", roomId, clients[0].id);
                sendToClient("rid:" + roomId, clients[0].endPoint);
                formatedString = string.Format("rid:{0},pid{1}", roomId, clients[1].id);
                sendToClient("rid:" + roomId, clients[1].endPoint);

                roomId++;

                // remove the matched clients 
                unmatchedClients.RemoveAt(unmatchedClients.Count -1);
                unmatchedClients.RemoveAt(unmatchedClients.Count -1);

            } else {
                ClientInfo lastestClient = unmatchedClients[unmatchedClients.Count -1];
                string formatedString = string.Format("You're id= {0} waiting for another player to match with you!!!", lastestClient.id);
                sendToClient(formatedString, lastestClient.endPoint);
            }
        }

        private State nextState(string command) {
            return MessageParser.parse(command);
        }

        private void sendToClient(string message, IPEndPoint endPoint)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            Console.WriteLine("Sending to " + endPoint.Address.ToString() + ":" + endPoint.Port);
            listener.Send(bytes, bytes.Length, endPoint.Address.ToString(), endPoint.Port);
        }
        public static void Main(string[] args)
        {
            new Server().go();
        }
    }

}
