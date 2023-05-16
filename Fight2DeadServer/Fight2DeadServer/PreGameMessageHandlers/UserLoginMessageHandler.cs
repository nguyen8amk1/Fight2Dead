using SocketServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fight2DeadServer.PreGameMessageHandlers
{
	public class UserLoginMessageHandler : PreGameMessageHandler
	{
		DebugLogger dlog = new DebugLogger();
		public void handle(string roomId, Player player, string message)
		{
			// receive message format: "email:{},password:{}"
			// insert this into the database
            string[] tokens = message.Split(',');
            string email = (Util.getValueFrom(tokens[2]));
            string password = (Util.getValueFrom(tokens[1]));
			dlog.playerLogin(message, 1);

			User user = Server.dbConnection.queryUser(email, password);
			string returnMessage = null;
			//bool loginFail = user == null;
			bool loginFail = false;
			if(loginFail)
			{
				Console.WriteLine("Send to client that No user with this info found");
				// login fail format: 
				returnMessage = "login:fail";
				return;
			}

			Console.WriteLine("Send to client that Login successfully");
			// login success format: 
			returnMessage = "login:success";
			TCPClientConnection.sendToClient(player.tcpClient, returnMessage);
		}
	}
}
