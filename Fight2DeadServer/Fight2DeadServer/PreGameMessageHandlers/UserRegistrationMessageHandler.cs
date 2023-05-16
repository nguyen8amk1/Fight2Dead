using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
	public class UserRegistrationMessageHandler : PreGameMessageHandler
	{
		DebugLogger dlog = new DebugLogger();
		public void handle(string roomId, Player player, string message)
		{
			// receive message format: "email:{},password:{}"
			// insert this into the database
            string[] tokens = message.Split(',');
            string email = (Util.getValueFrom(tokens[2]));
            string password = (Util.getValueFrom(tokens[1]));
			Console.WriteLine("TODO: check if info (email, username) is duplicated");

			Server.dbConnection.insertUser(email, password);
			dlog.playerRegistered(message, 1);

			string returnMessage = null;
			bool registrationSuccess =  true;
			if(registrationSuccess)
			{
				// registration success format: 
				returnMessage = "register:success";
			} else
			{
				returnMessage = "register:fail";
			}
			TCPClientConnection.sendToClient(player.tcpClient, returnMessage);
		}
	}
}
