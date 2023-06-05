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
    //UI Object array
    public GameObject[] uiObjects;
    //-----------------------------
    //switch player
    //bool
    private bool isPlayer1Active_Team1 = true;
    private bool isPlayer2Active_Team1 = false;
    private bool isPlayer1Active_Team2 = true;
    private bool isPlayer2Active_Team2 = false;
    //define object player
    private GameObject p1t1;
    private GameObject p2t1;
    private GameObject p1t2;
    private GameObject p2t2;
    //define object UI
    private GameObject UI_p1t1;
    private GameObject UI_p2t1;
    private GameObject UI_p1t2;
    private GameObject UI_p2t2;
    private Vector3 currentPlayerPosition;

    public GameObject swtichParticle;
    private GameState globalGameState = GameState.Instance;
    //team win UI
    public GameObject team1win;
    public GameObject team2win;
    void Start()
    {
        Player1_Team1 = convertToCorrectName(globalGameState.chosenCharacters[0]);
        Player2_Team1 = convertToCorrectName(globalGameState.chosenCharacters[1]);
        Player1_Team2 = convertToCorrectName(globalGameState.chosenCharacters[2]);
        Player2_Team2 = convertToCorrectName(globalGameState.chosenCharacters[3]);
        mapName = convertToCorrectMapName(globalGameState.chosenMapName);

        SetTeam1(Player1_Team1, Player1_Team1 + "UI");
        SetTeam1(Player2_Team1, Player2_Team1 + "UI");

        SetTeam2(Player1_Team2, Player1_Team2 + "UI");
        SetTeam2(Player2_Team2, Player2_Team2 + "UI");

        SetMap(mapName);
        //get Object Player
        p1t1 = FindPlayerObjectByName(Player1_Team1);
        p2t1 = FindPlayerObjectByName(Player2_Team1);
        p1t2 = FindPlayerObjectByName(Player1_Team2);
        p2t2 = FindPlayerObjectByName(Player2_Team2);

        p2t1.SetActive(false);
        p2t2.SetActive(false);

        //get Object UI
        UI_p1t1 = FindUIObjectByName(Player1_Team1 + "UI");
        UI_p2t1 = FindUIObjectByName(Player2_Team1 + "UI");
        UI_p1t2 = FindUIObjectByName(Player1_Team2 + "UI");
        UI_p2t2 = FindUIObjectByName(Player2_Team2 + "UI");

        SetUI(UI_p1t1, 1);
        SetUI(UI_p2t1, 1);
        SetUI(UI_p1t2, 2);
        SetUI(UI_p2t2, 2);

        UI_p2t1.SetActive(false);
        UI_p2t2.SetActive(false);

        globalGameState.camPlayer1 = p1t1;
        globalGameState.camPlayer2 = p1t2;

        globalGameState.p1CharName = Player1_Team1;
        globalGameState.p2CharName = Player1_Team2;

        if (globalGameState.numPlayers == 2)
        {
            p1t1.AddComponent<P1ControlScript>();
            p2t1.AddComponent<P1ControlScript>();
            p1t2.AddComponent<P2ControlScript>();
            p2t2.AddComponent<P2ControlScript>();
        }
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
        else if (name.Equals("byakuya"))
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
        else if (name.Equals("luffy5th"))
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
    private void SetTeam1(string playerName, string playerUI)
    {
        foreach (GameObject obj in playerObjects)
        {
            if (obj.name == playerName)
            {
                obj.SetActive(true);
                Debug.Log(obj.name + " is now active.");

                obj.transform.parent = Team1.transform;
                Debug.Log(obj.name + " is now a child of " + Team1.name);
                break;
            }
        }
        foreach (GameObject obj in uiObjects)
        {
            if (obj.name == playerUI)
            {
                obj.SetActive(true);
                Debug.Log(obj.name + " is now active.");

                obj.transform.parent = Team1.transform;
                Debug.Log(obj.name + " is now a child of " + Team1.name);
                return;
            }
        }

    }

    private void SetTeam2(string playerName, string playerUI)
    {
        foreach (GameObject obj in playerObjects)
        {
            if (obj.name == playerName)
            {
                obj.SetActive(true);
                Debug.Log(obj.name + " is now active.");

                obj.transform.parent = Team2.transform;
                Debug.Log(obj.name + " is now a child of " + Team2.name);
                break;
            }
        }

        foreach (GameObject obj in uiObjects)
        {
            if (obj.name == playerUI)
            {
                obj.SetActive(true);
                Debug.Log(obj.name + " is now active.");

                obj.transform.parent = Team2.transform;
                Debug.Log(obj.name + " is now a child of " + Team2.name);
                return;
            }
        }

    }
    private GameObject FindPlayerObjectByName(string objectName)
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

    private GameObject FindUIObjectByName(string objectName)
    {
        foreach (GameObject obj in uiObjects)
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
        if (globalGameState.numPlayers == 4)
        {

        }
        else if (globalGameState.numPlayers == 2)
        {
            if (globalGameState.player1IsBeingControlled)
            {
                if (globalGameState.p1Transformed)
                {
                    SwitchPlayersTeam1();
                    globalGameState.p1Transformed = false;
                }
            }
            if (globalGameState.player2IsBeingControlled)
            {
                if (globalGameState.p2Transformed)
                {
                    SwitchPlayersTeam2();
                    globalGameState.p2Transformed = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (globalGameState.numPlayers == 4)
            {
                if (globalGameState.PlayerId == 1 || globalGameState.PlayerId == 2)
                {
                    // TODO: set some info to send to the other side
                    //SwitchPlayersTeam1(globalGameState.PlayerId == 1);
                }
                if (globalGameState.PlayerId == 3 || globalGameState.PlayerId == 4)
                {
                    // TODO: set some info to send to the other side
                    //SwitchPlayersTeam2(globalGameState.PlayerId == 3);
                }
            }
            else if (globalGameState.numPlayers == 2)
            {
                if (globalGameState.PlayerId == 1)
                {
                    // TODO: set some info to send to the other side
                    SwitchPlayersTeam1();
                }
                if (globalGameState.PlayerId == 2)
                {
                    // TODO: set some info to send to the other side
                    SwitchPlayersTeam2();
                }
            }
        }

        /*
        if (Input.GetKeyDown(KeyCode.RightShift))
        {

            SwitchPlayersTeam2();
        }
        Debug.Log(isPlayer1Active_Team1);
        //auto switch to another player if 1 player left all numberRespawn
        if (p1t1 == null)
        {
            Destroy(UI_p1t1);

            if (p2t1 != null)
                p2t1.SetActive(true);

            if (UI_p2t1 != null)
                UI_p2t1.SetActive(true);
        }
        if (p2t1 == null)
        {
            Destroy(UI_p2t1);
            if (p1t1 != null)
                p1t1.SetActive(true);
            if (UI_p1t1 != null)
                UI_p1t1.SetActive(true);
        }

        if (p1t2 == null)
        {
            Destroy(UI_p1t2);
            if (p2t2 != null)
                p2t2.SetActive(true);
            if (UI_p2t2 != null)
                UI_p2t2.SetActive(true);
        }
        if (p2t2 == null)
        {
            Destroy(UI_p2t2);
            if (p1t2 != null)
                p1t2.SetActive(true);
            if (UI_p1t2 != null)
                UI_p1t2.SetActive(true);
        }

        */
        if (p1t1 == null && p2t1 == null)
        {
            // Debug.Log("TEAM 2 WIN");
            team2win.SetActive(false);

            //Scene transition 
        }
        if (p1t2 == null && p2t2 == null)
        {
            // Debug.Log("TEAM 1 WIN");
            team1win.SetActive(false);

            //Scene transition 
        }
    }
    void SwitchPlayersTeam1()
    {
        if (isPlayer1Active_Team1 && p2t1 != null && p1t1 != null && UI_p2t1 != null && UI_p1t1 != null)
        {
            currentPlayerPosition = p1t1.transform.position;

            isPlayer1Active_Team1 = false;
            isPlayer2Active_Team1 = true;

            p1t1.SetActive(false);
            UI_p1t1.SetActive(false);
            p2t1.SetActive(true);
            UI_p2t1.SetActive(true);

            p2t1.transform.position = currentPlayerPosition;
            currentPlayerPosition.y -= 1.0f;
            Instantiate(swtichParticle, currentPlayerPosition, Quaternion.identity);

            Debug.Log("TEAM1: PLAYER 1 SWITCH TO PLAYER 2");
            globalGameState.camPlayer1 = p2t1;
            globalGameState.currentCharT1 = 2;
            GamePlayerNetworkCommutor.count1 = 0;
            p2t1.GetComponent<P1ControlScript>().initControlScript(Player2_Team1);
        }
        else if (isPlayer2Active_Team1 && p2t1 != null && p1t1 != null && UI_p2t1 != null && UI_p1t1 != null)
        {
            currentPlayerPosition = p2t1.transform.position;
            isPlayer1Active_Team1 = true;
            isPlayer2Active_Team1 = false;

            p2t1.SetActive(false);
            UI_p2t1.SetActive(false);
            p1t1.SetActive(true);
            UI_p1t1.SetActive(true);

            p1t1.transform.position = currentPlayerPosition;
            currentPlayerPosition.y -= 1.0f;
            Instantiate(swtichParticle, currentPlayerPosition, Quaternion.identity);
            Debug.Log("TEAM1: PLAYER 2 SWITCH TO PLAYER 1");
            globalGameState.camPlayer1 = p1t1;
            globalGameState.currentCharT1 = 1;
            GamePlayerNetworkCommutor.count1 = 0;
            GamePlayerNetworkCommutor.count1 = 0;
            p1t1.GetComponent<P1ControlScript>().initControlScript(Player1_Team1);
        }
        else
        {
            return;
        }
    }

    void SwitchPlayersTeam2()
    {
        if (isPlayer1Active_Team2 && p2t2 != null && p1t2 != null && UI_p2t2 != null && UI_p1t2 != null)
        {
            currentPlayerPosition = p1t2.transform.position;

            isPlayer1Active_Team2 = false;
            isPlayer2Active_Team2 = true;

            p1t2.SetActive(false);
            UI_p1t2.SetActive(false);
            p2t2.SetActive(true);
            UI_p2t2.SetActive(true);

            p2t2.transform.position = currentPlayerPosition;
            currentPlayerPosition.y -= 1.0f;
            Instantiate(swtichParticle, currentPlayerPosition, Quaternion.identity);
            Debug.Log("TEAM2: PLAYER 1 SWITCH TO PLAYER 2");
            globalGameState.camPlayer2 = p2t2;
            globalGameState.currentCharT2 = 2;
            p2t2.GetComponent<P2ControlScript>().initControlScript(Player2_Team2);
            GamePlayerNetworkCommutor.count2 = 0;
        }
        else if (isPlayer2Active_Team2 && p2t2 != null && p1t2 != null && UI_p2t2 != null && UI_p1t2 != null)
        {
            currentPlayerPosition = p2t2.transform.position;

            isPlayer1Active_Team2 = true;
            isPlayer2Active_Team2 = false;

            p2t2.SetActive(false);
            UI_p2t2.SetActive(false);
            p1t2.SetActive(true);
            UI_p1t2.SetActive(true);

            p1t2.transform.position = currentPlayerPosition;
            currentPlayerPosition.y -= 1.0f;
            Instantiate(swtichParticle, currentPlayerPosition, Quaternion.identity);
            Debug.Log("TEAM2: PLAYER 2 SWITCH TO PLAYER 1");
            globalGameState.camPlayer2 = p1t2;
            globalGameState.currentCharT2 = 1;
            p1t2.GetComponent<P2ControlScript>().initControlScript(Player1_Team2);
            GamePlayerNetworkCommutor.count2 = 0;
        }
        else
        {
            return;
        }
    }

    void DestroyUI(string uiName)
    {
        for (int i = 0; i < uiObjects.Length; i++)
        {
            if (uiObjects[i].name == uiName)
            {
                Destroy(uiObjects[i]);
                break;
            }
        }
    }

    void SetUI(GameObject parentObject, int team)
    {
        if (parentObject != null)
        {
            //Find child object by name
            Transform childTransform = parentObject.transform.Find("image");

            if (childTransform != null)
            {
                //Get the RectTransform of the child object
                RectTransform childRectTransform = childTransform.GetComponent<RectTransform>();
                if (team == 1)
                    SetUIPositionTeam1(childRectTransform);
                if (team == 2)
                    SetUIPositionTeam2(childRectTransform);
                if (childRectTransform == null)
                {
                    Debug.LogError("Child object does not have a RectTransform component.");
                }
            }
            else
            {
                Debug.LogError("Child object not found with the given name.");
            }
        }
        else
        {
            Debug.LogError("Parent object or child object name is not assigned.");
        }
    }

    void SetUIPositionTeam1(RectTransform childRectTransform)
    {
        if (childRectTransform != null)
        {
            Vector2 newPosition = new Vector2(93.055f, 67.32f);
            childRectTransform.anchoredPosition = newPosition;
        }
    }

    void SetUIPositionTeam2(RectTransform childRectTransform)
    {
        if (childRectTransform != null)
        {
            Vector2 newPosition = new Vector2(400f, 67.32f);
            childRectTransform.anchoredPosition = newPosition;
        }
    }

    /*
void SwitchPlayersTeam1(bool isP1)
{
    if (isP1)
    {
        currentPlayerPosition = p1t1.transform.position;

        //isPlayer1Active_Team1 = false;
        //isPlayer2Active_Team1 = true;
        p1t1.SetActive(false);
        p2t1.SetActive(true);

        p2t1.transform.position = currentPlayerPosition;
        Debug.Log("TEAM1: PLAYER 1 SWITCH TO PLAYER 2");
        globalGameState.camPlayer1 = p2t1;
    }
    else 
    {
        currentPlayerPosition = p2t1.transform.position;
        //isPlayer1Active_Team1 = true;
        //isPlayer2Active_Team1 = false;
        p2t1.SetActive(false);
        p1t1.SetActive(true);
        p1t1.transform.position = currentPlayerPosition;
        Debug.Log("TEAM1: PLAYER 2 SWITCH TO PLAYER 1");
        globalGameState.camPlayer1 = p1t1;
    }
}

void SwitchPlayersTeam2(bool isP3)
{
    if (isP3)
    {
        currentPlayerPosition = p1t2.transform.position;

        //isPlayer1Active_Team2 = false;
        //isPlayer2Active_Team2 = true;
        p1t2.SetActive(false);
        p2t2.SetActive(true);
        p2t2.transform.position = currentPlayerPosition;
        Debug.Log("TEAM2: PLAYER 1 SWITCH TO PLAYER 2");
        globalGameState.camPlayer2 = p2t2;
    }
    else 
    {
        currentPlayerPosition = p2t2.transform.position;

        //isPlayer1Active_Team2 = true;
        //isPlayer2Active_Team2 = false;
        p2t2.SetActive(false);
        p1t2.SetActive(true);
        p1t2.transform.position = currentPlayerPosition;
        Debug.Log("TEAM2: PLAYER 2 SWITCH TO PLAYER 1");
        globalGameState.camPlayer2 = p1t2;
    }
}
    */
}
