using SocketServer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    [SerializeField]
    //player name
    public string Player1_Team1;
    public string Player2_Team1;
    public string Player1_Team2;
    public string Player2_Team2;
    //map name
    public string mapName;
    //define team game object
    public GameObject Team1;
    public GameObject Team2;

    //Player Object array
    public GameObject[] playerObjects;
    //Map Object array
    public GameObject[] mapObjects;
    //-----------------------------
    //switch player
    //bool
    private bool isPlayer1Active_Team1 = true;
    private bool isPlayer2Active_Team1 = false;
    private bool isPlayer1Active_Team2 = true;
    private bool isPlayer2Active_Team2 = false;
    private GameObject p1t1;
    private GameObject p2t1;
    private GameObject p1t2;
    private GameObject p2t2;
    private Vector3 currentPlayerPosition;

    private GameState globalGameState = GameState.Instance;

    void Start()
    {
        Player1_Team1 = convertToCorrectName(globalGameState.chosenCharacters[0]);
        Player2_Team1 = convertToCorrectName(globalGameState.chosenCharacters[1]);
        Player1_Team2 = convertToCorrectName(globalGameState.chosenCharacters[2]);
        Player2_Team2 = convertToCorrectName(globalGameState.chosenCharacters[3]);
        mapName = convertToCorrectMapName(globalGameState.chosenMapName);

        SetTeam1(Player1_Team1);
        SetTeam1(Player2_Team1);

        SetTeam2(Player1_Team2);
        SetTeam2(Player2_Team2);

        SetMap(mapName);

        p1t1 = FindObjectByName(Player1_Team1);
        p2t1 = FindObjectByName(Player2_Team1);
        p1t2 = FindObjectByName(Player1_Team2);
        p2t2 = FindObjectByName(Player2_Team2);

        p2t1.SetActive(false);
        p2t2.SetActive(false);

        globalGameState.camPlayer1 = p1t1;
        globalGameState.camPlayer2 = p1t2;

        globalGameState.p1CharName = Player1_Team1;
        globalGameState.p2CharName = Player1_Team2;

        p1t1.AddComponent<P1ControlScript>();
        p1t2.AddComponent<P2ControlScript>();
    }

    private string convertToCorrectMapName(string chosenMapName)
    {
        // Note ten lai :
        // ngoi lang -> map 2 (menu chua co)
        // toan la nha cao tang -> fourside -> map 3
        // co du quay -> yoshiisland -> map 1
        // thousand sunny -> cuop bien -> map 5
        // lau dai tren may -> Temple -> map 4
        // lau dai tern may mau cut -> Palutena's Shrine -> xai do thanh map 2 @temp
        /*
            private string[] mapName = new string[] {"Yoshi", "Sunny", "Palutena", "Fourside", "Temple" };
         */
        if (chosenMapName.Equals("Yoshi") || chosenMapName.Equals("Yoshi\n"))
        {
            return "Map1";
        }
        else if (chosenMapName.Equals("Fourside") || chosenMapName.Equals("Fourside\n"))
        {
            return "Map3";
        }
        else if (chosenMapName.Equals("Temple") || chosenMapName.Equals("Temple\n"))
        {
            return "Map4";
        }
        else if (chosenMapName.Equals("Palutena") || chosenMapName.Equals("Palutena\n")) // @Temp
        {
            return "Map2";
        }
        else if (chosenMapName.Equals("Sunny") || chosenMapName.Equals("Sunny\n"))
        {
            return "Map5";
        }
        throw new Exception("Map name not recognize: " + chosenMapName);
	}

	private string[] charName = new string[] { "capa", "venom", "sasori", "gaara", "ken", "ryu",
        "link","reborn","jotaro" };
	private string convertToCorrectName(string name)
	{
        if (name.Equals("capa"))
        {
            return "Captain";
        }
        else if (name.Equals("link"))
        {
            return "Link";
        }
        else if (name.Equals("sasori")) // @Temp
        {
            return "Byakuya";
        }
        else if (name.Equals("gaara"))
        {
            return "Gaara";
        }
        else if (name.Equals("jotaro"))
        {
            return "Jotaro";
        }
        else if (name.Equals("venom"))
        {
            return "Venom";
        }
        else if (name.Equals("ken"))
        {
            return "Ken";
        }
        else if (name.Equals("ryu"))
        {
            return "Ryu";
        }
        else if (name.Equals("reborn")) // @Temp
        {
            return "Luffy5th";
        }

        throw new Exception("Name not recognize: " + name);
	}

	private void SetMap(string objectName)
    {
        foreach (GameObject obj in mapObjects)
        {
            if (obj.name == objectName)
            {
                obj.SetActive(true);
                Debug.Log(obj.name + " is now active.");
                return;
            }
        }

        Debug.Log("Object with name " + objectName + " not found in the array.");
    }
    private void SetTeam1(string objectName)
    {
        foreach (GameObject obj in playerObjects)
        {
            if (obj.name == objectName)
            {
                obj.SetActive(true);
                Debug.Log(obj.name + " is now active.");

                obj.transform.parent = Team1.transform;
                Debug.Log(obj.name + " is now a child of " + Team1.name);

                return;
            }
        }

        Debug.Log("Object with name " + objectName + " not found in the array.");
    }

    private void SetTeam2(string objectName)
    {
        foreach (GameObject obj in playerObjects)
        {
            if (obj.name == objectName)
            {
                obj.SetActive(true);
                Debug.Log(obj.name + " is now active.");

                obj.transform.parent = Team2.transform;
                Debug.Log(obj.name + " is now a child of " + Team2.name);

                return;
            }
        }

        Debug.Log("Object with name " + objectName + " not found in the array.");
    }
    private GameObject FindObjectByName(string objectName)
    {
        foreach (GameObject obj in playerObjects)
        {
            if (obj.name == objectName)
            {
                return obj;
            }
        }

        return null;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            SwitchPlayersTeam1();
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {

            SwitchPlayersTeam2();
        }
    }

    void SwitchPlayersTeam1()
    {
        if (isPlayer1Active_Team1)
        {
            currentPlayerPosition = p1t1.transform.position;

            isPlayer1Active_Team1 = false;
            isPlayer2Active_Team1 = true;
            p1t1.SetActive(false);
            p2t1.SetActive(true);
            p2t1.transform.position = currentPlayerPosition;
            Debug.Log("TEAM1: PLAYER 1 SWITCH TO PLAYER 2");
			globalGameState.camPlayer1 = p2t1;
        }
        else if (isPlayer2Active_Team1)
        {
            currentPlayerPosition = p2t1.transform.position;

            isPlayer1Active_Team1 = true;
            isPlayer2Active_Team1 = false;
            p2t1.SetActive(false);
            p1t1.SetActive(true);
            p1t1.transform.position = currentPlayerPosition;
            Debug.Log("TEAM1: PLAYER 2 SWITCH TO PLAYER 1");
			globalGameState.camPlayer1 = p1t1;
        }
    }

    void SwitchPlayersTeam2()
    {
        if (isPlayer1Active_Team2)
        {
            currentPlayerPosition = p1t2.transform.position;

            isPlayer1Active_Team2 = false;
            isPlayer2Active_Team2 = true;
            p1t2.SetActive(false);
            p2t2.SetActive(true);
            p2t2.transform.position = currentPlayerPosition;
            Debug.Log("TEAM2: PLAYER 1 SWITCH TO PLAYER 2");
			globalGameState.camPlayer2 = p2t2;
        }
        else if (isPlayer2Active_Team2)
        {
            currentPlayerPosition = p2t2.transform.position;

            isPlayer1Active_Team2 = true;
            isPlayer2Active_Team2 = false;
            p2t2.SetActive(false);
            p1t2.SetActive(true);
            p1t2.transform.position = currentPlayerPosition;
            Debug.Log("TEAM2: PLAYER 2 SWITCH TO PLAYER 1");
			globalGameState.camPlayer2 = p1t2;
        }
    }
}
