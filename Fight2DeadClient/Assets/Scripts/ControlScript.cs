using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;

public class ControlScript : MonoBehaviour
{
	// TODO: pass all of these from the infoCoordinator
	public float speed = 3;
    public float x = -100, y = -100;
    public int id = -1; 
    public string SERVER_IP = "";
    public const int PORT = 8080;
    public UdpClient serverSocket;

	// TODO: implement a selective control system of some kind 

	// Start is called before the first frame update
	void Start()
    {
    }

	// Update is called once per frame
	void Update()
    {
        if(x != -100 && y != -100)
		{
            this.transform.position = new Vector3(x, y, 0);
		}

        // TODO: send position to the server
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
            if (id > -1) { 
				string position = $"id:{id}:{this.transform.position.x},{this.transform.position.y}";
				sendToServer(position);
			}
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            if (id > -1) { 
				string position = $"id:{id}:{this.transform.position.x},{this.transform.position.y}";
				sendToServer(position);
			}
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position += new Vector3(0, speed * Time.deltaTime, 0);
			if (id > -1)
			{

				string position = $"id:{id}:{this.transform.position.x},{this.transform.position.y}";
				sendToServer(position);
			}
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
			if (id > -1)
			{
				string position = $"id:{id}:{this.transform.position.x},{this.transform.position.y}";
				sendToServer(position);
			}
        }
    }

	private void sendToServer(string message)
	{
        Debug.Log("Send to server");
        byte[] bytes = Encoding.ASCII.GetBytes(message);
        serverSocket.Send(bytes, bytes.Length, SERVER_IP, PORT);
	}
}
