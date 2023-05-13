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

    public string[] chosenCharacters = new string[4]; 

	private GameState()
    {
        roomId = 0;
        playerId = 0;
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
