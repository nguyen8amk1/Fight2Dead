using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection
{
	class User
	{
		public int id;
		public string email;
		public string passwordHash;
		public User(int id, string email, string passwordHash)
		{
			this.id = id;
			this.email = email;
			this.passwordHash = passwordHash;
		}
	}
}
