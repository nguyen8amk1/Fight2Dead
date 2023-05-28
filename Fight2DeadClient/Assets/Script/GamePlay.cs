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

    public GameObject swtichParticle;

    void Start()
    {
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
        Debug.Log(isPlayer1Active_Team1);
        //auto switch to another player if 1 player left all numberRespawn
        if (p1t1 == null)
        {
            p2t1.SetActive(true);
        }
        else if (p2t1 == null)
        {
            p1t1.SetActive(true);
        }

        if (p1t2 == null)
        {
            p2t2.SetActive(true);
        }
        else if (p2t2 == null)
        {
            p1t2.SetActive(true);
        }
    }

    void SwitchPlayersTeam1()
    {
        if (isPlayer1Active_Team1 && p2t1 != null && p1t1!=null)
        {
            currentPlayerPosition = p1t1.transform.position;

            isPlayer1Active_Team1 = false;
            isPlayer2Active_Team1 = true;
            p1t1.SetActive(false);
            p2t1.SetActive(true);
            p2t1.transform.position = currentPlayerPosition;
            currentPlayerPosition.y -= 1.0f;
            Instantiate(swtichParticle, currentPlayerPosition, Quaternion.identity);

            Debug.Log("TEAM1: PLAYER 1 SWITCH TO PLAYER 2");
        }
        else if (isPlayer2Active_Team1 && p2t1 != null && p1t1!=null)
        {
            currentPlayerPosition = p2t1.transform.position;

            isPlayer1Active_Team1 = true;
            isPlayer2Active_Team1 = false;
            p2t1.SetActive(false);
            p1t1.SetActive(true);
            p1t1.transform.position = currentPlayerPosition;
            currentPlayerPosition.y -= 1.0f;
            Instantiate(swtichParticle, currentPlayerPosition, Quaternion.identity);
            Debug.Log("TEAM1: PLAYER 2 SWITCH TO PLAYER 1");
        }
        else
        {
            return;
        }
    }

    void SwitchPlayersTeam2()
    {
        if (isPlayer1Active_Team2 && p2t2 != null && p1t2 != null)
        {
            currentPlayerPosition = p1t2.transform.position;

            isPlayer1Active_Team2 = false;
            isPlayer2Active_Team2 = true;
            p1t2.SetActive(false);
            p2t2.SetActive(true);
            p2t2.transform.position = currentPlayerPosition;
            currentPlayerPosition.y -= 1.0f;
            Instantiate(swtichParticle, currentPlayerPosition, Quaternion.identity);
            Debug.Log("TEAM2: PLAYER 1 SWITCH TO PLAYER 2");
        }
        else if (isPlayer2Active_Team2 && p2t2 != null && p1t2 != null)
        {
            currentPlayerPosition = p2t2.transform.position;

            isPlayer1Active_Team2 = true;
            isPlayer2Active_Team2 = false;
            p2t2.SetActive(false);
            p1t2.SetActive(true);
            p1t2.transform.position = currentPlayerPosition;
            currentPlayerPosition.y -= 1.0f;
            Instantiate(swtichParticle, currentPlayerPosition, Quaternion.identity);
            Debug.Log("TEAM2: PLAYER 2 SWITCH TO PLAYER 1");
        }
        else
        {
            return;
        }
    }
}
