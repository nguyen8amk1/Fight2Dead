using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameState
{
    private static GameState instance = null;
    private static readonly object padlock = new object();
    private int roomId;
    private int playerId;

    public int numPlayers = 2;
    private string[] characterNames = new string[4];
    public int charNameCount = 0;
    public string onlineMode = "GLOBAL";
    public string isRoomOwner = "false";

    public bool receiveRidPid = false;

    public string player1Name = "NoName";
    public string player2Name = "NoName";
    public bool opponentReady = false;

    // character 
    public string[] chosenCharacters = new string[4];

    // map 
	public bool hostPlayerMapChosen = false;
	public bool opponentMapChosen = false;
	public bool lobby_P1Quit = false;
	public bool lobby_P2Quit = false;

    public Dictionary<string, int> scenesOrder = new Dictionary<string, int>();

	private GameState()
    {
        roomId = 0;
        playerId = 0;

        scenesOrder.Add("PRESS_ANY_KEY", 0);
        scenesOrder.Add("MENU", 1);
        scenesOrder.Add("MATCHING", 2);
        scenesOrder.Add("LOBBY", 3);
        scenesOrder.Add("CHARACTER_SELECT", 4);
        scenesOrder.Add("MAP_SELECT", 5);
        scenesOrder.Add("LOADING_SCREEN", 6);
        scenesOrder.Add("MAP3", 7);
        //scenesOrder.Add(8, "mainMenu");
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
