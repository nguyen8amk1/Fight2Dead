using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnection
{
	class MySQLDatabaseConnection
	{
		private string server;  
		private string database;  
		private string uid;  
		private string password;  
		private int port;  

		private MySqlConnection connection;

		public MySQLDatabaseConnection(string server, string database, string uid, string password, int port)
		{
			this.server = server; 
			this.database = database; 
			this.uid = uid; 
			this.password = password; 
			this.port = port;

			setupConnection();
		}

		public MySQLDatabaseConnection(string filePath)
		{
			loadCredentialFromFile(filePath);
			setupConnection();
		}

		private void loadCredentialFromFile(string filePath)
		{
			try
			{
				string[] lines = File.ReadAllLines(filePath);

				server = lines[0];
				database = lines[1];
				uid = lines[2];
				password = lines[3];
				port = Int32.Parse(lines[4].Trim());
			} catch(Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private void setupConnection()
		{
			// TODO: setup the connection
			string connectionString = $"Server={server};Port={port};Database={database};UID={uid};Pwd={password};Pooling=false;Character Set=utf8;SSL Mode=None";
			connection = new MySqlConnection(connectionString);
			try
			{
				connection.Open();
			} catch (MySqlException e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public void close()
		{
			connection.Close();
		}

		// @Security: sql injection 
		public bool insertUser(string email, string passwordHash)
		{
			// TODO: update the id of the sql to auto increment 
			string insertCommand = $"INSERT INTO User(email, password_hash) values ('{email}', '{passwordHash}')";
			MySqlCommand cmd = new MySqlCommand(insertCommand, connection);
			int value = cmd.ExecuteNonQuery();

			Console.WriteLine("Insert Value: {0}", value);
			return false;
		}

		public User queryUser(string email, string passwordHash)
		{
			Console.WriteLine("Do the query here ");
			string query = $"SELECT * FROM User WHERE email='{email}' AND password_hash='{passwordHash}'";
			MySqlCommand cmd = new MySqlCommand(query, connection);
			MySqlDataReader rdr = cmd.ExecuteReader();
			Console.WriteLine($"{rdr.GetName(0),-4} {rdr.GetName(1),-10} {rdr.GetName(2),10}");

			if(!rdr.HasRows)
				return null;

			rdr.Read();
			return new User(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2));
		}
	}
}
