using SocketServer;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public sealed class GameState
{
    private static GameState instance = null;
    private static readonly object padlock = new object();
    private int roomId;
    private int playerId;

    public int numPlayers = 0;
    private string[] characterNames = new string[4];
    public int charNameCount = 0;
    public string onlineMode = "";
    public string isRoomOwner = "false";

    public bool receiveRidPid = false;

    public string player1Name = "NoName";
    public string player2Name = "NoName";
    public string player3Name = "NoName";
    public string player4Name = "NoName";
    public bool opponentReady = false;

    // character 

    // map 
    public bool hostPlayerMapChosen = false;
    public bool opponentMapChosen = false;
    public bool lobby_P1Quit = false;
    public bool lobby_P2Quit = false;

    public bool lobbyP1Ready = false;
    public bool lobbyP2Ready = false;
    public bool lobbyP3Ready = false;
    public bool lobbyP4Ready = false;

    public Dictionary<string, int> scenesOrder = new Dictionary<string, int>();
    public bool loginSuccess = false;
    public Player[] playersPosition = new Player[4] {
        new Player(0, 0),    
        new Player(0, 0),    
        new Player(0, 0),    
        new Player(0, 0)    
	};

	public string username;
	public bool someoneChooseMap;
	//public string chosenMapName = "Yoshi";
    //public string[] chosenCharacters = new string[4] {"capa", "venom", "link", "ryu"};
	public string chosenMapName = null;
    public string[] chosenCharacters = new string[4] {null, null, null, null};

	public bool player1IsChosen_2v2 = false;
	public bool player3IsChosen_2v2 = false;

    public int[] charactersState = new int[] { 0, 0, 0, 0 };

    // Animation variables 
	public bool player1IsBeingControlled;
	public int player1State;

	public bool player2IsBeingControlled;
	public int player2State;

	public bool sendP1Info = false;
	public bool sendP2Info = false;

	public string playerMessage;

	public GameObject camPlayer1 = null;
	public GameObject camPlayer2 = null;

	private GameState()
    {

        roomId = 0;
        playerId = 0;


        scenesOrder.Add("PRESS_ANY_KEY", 0);
        scenesOrder.Add("LOGIN/REGISTER", 1);
        scenesOrder.Add("MENU", 2);
        scenesOrder.Add("MATCHING", 3);
        scenesOrder.Add("LOBBY", 4);
        scenesOrder.Add("CHARACTER_SELECT", 5);
        scenesOrder.Add("MAP_SELECT", 6);
        scenesOrder.Add("LOADING_SCREEN", 7);
        scenesOrder.Add("MAP3", 8);
    }

    public static GameState Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new GameState();
                }
                return instance;
            }
        }
    }

    public int RoomId
    {
        get { return roomId; }
        set { roomId = value; }
    }

    public int PlayerId
    {
        get { return playerId; }
        set { playerId = value; }
    }

    // Add character name to the list
    public void addCharacterName(int index, string name)
    {
        characterNames[index] = name;
        charNameCount++;
    }

    // Get list of character names
}
