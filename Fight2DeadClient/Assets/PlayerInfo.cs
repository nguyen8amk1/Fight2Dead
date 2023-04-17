using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerInfo 
{
    // TODO: this class will be a singleton contains all the infos about the client: 
    // including: playerid, roomid
    private static PlayerInfo instance = null;
    private static readonly object padlock = new object();
    private int roomId;
    private int playerId;

    private PlayerInfo()
    {
        roomId = 0;
        playerId = 0;
    }

    public static PlayerInfo Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                {
                    instance = new PlayerInfo();
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
}
