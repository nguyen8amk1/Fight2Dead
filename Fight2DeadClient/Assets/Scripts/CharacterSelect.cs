using SocketServer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    private string player = "jotaro";
    private string p = "p3";

    [Header("Switch Mode")]
    [SerializeField] public int textMode = 1;
    [SerializeField] public int Mode = 1;
    [Header("1 VS 1 Mode Player ID")]
    public int twoPlayerID = 1;
    [Header("2 VS 2 Mode Player ID")]
    public int fourPlayerID = 1;
    public GameObject[] p1Char, p2Char, p3Char, p4Char;
    public GameObject P1, P2, P3, P4;
    public GameObject P1_icon, P1_1_icon, P2_icon, P2_1_icon, P3_icon, P2_2_icon, P4_icon;
    public GameObject pointer0, pointer1, pointer2, pointer3, pointer4, pointer5, pointer6, pointer7,
        pointer8;
    public static int selectVal = 0;
    public static int currentID = 1;
    private Vector3 startMaker = new Vector3(-4.8f, -2.7f, 0);
    private Vector3 endMaker = new Vector3(-7.5f, -2.7f, 0);
    private Vector3 startMaker_1 = new Vector3(4.9f, -2.7f, 0);
    private Vector3 endMaker_1 = new Vector3(7.5f, -2.7f, 0);
    private float timingVar = 0, timingVar_1 = 0, timingVarP1 = 0, timingVarP3 = 0, timingVarSpawn = 0;
    private float speed = 3.0f;
    private bool enterHitP1 = false, enterHitP2 = false;
    private int enterCount1 = 0, enterCount2 = 0;

    string[] charName = new string[] { "capa", "venom", "sasori", "gaara", "ken", "ryu",
        "link","reborn","jotaro" };

    private bool P1Log1 = false, P1Log2 = false, P2Log1 = false, P2Log2 = false;
    private GameState globalGameState = GameState.Instance;

    private bool P1Log = false, P2Log = false, P3Log = false, P4Log = false;
    private bool p1EnterHit = false, p2EnterHit = false, p3EnterHit = false, p4EnterHit = false;
    //private bool vhit = false;
	//private bool bhit = false;

	private int char1Count = 0;
	private int char2Count = 0;
	private int char3Count = 0;
	private int char4Count = 0;

	// Start is called before the first frame update
	void Start()
    {
        Application.targetFrameRate = 60;
        if (globalGameState.numPlayers == 2)
        {
            textMode = 1;
            Mode = 1;
            twoPlayerID = globalGameState.PlayerId;
        } 
        if (globalGameState.numPlayers == 4)
        {
            //Debug.Log("TODO: handle 4 players id in choose character");
            textMode = 2;
            Mode = 2;
            fourPlayerID = globalGameState.PlayerId;
        } 
    }

	private void OnApplicationQuit()
	{
		Debug.Log("Send quit message from Character Select");
		string quitMessage = PreGameMessageGenerator.quitMessage();
		ServerCommute.connection.sendToServer(quitMessage);
	}
    public void Char0()
    {
        selectVal = 0;
    }
    public void Char1()
    {
        selectVal = 1;
    }
    public void Char2()
    {
        selectVal = 2;
    }
    public void Char3()
    {
        selectVal = 3;
    }
    public void Char4()
    {
        selectVal = 4;
    }
    public void Char5()
    {
        selectVal = 5;
    }
    public void Char6()
    {
        selectVal = 6;
    }
    public void Char7()
    {
        selectVal = 7;
    }
    public void Char8()
    {
        selectVal = 8;
    }

    public void SpawnPlayer(string player, string character)
    {

        if (player == "p1")
        {
            P1.SetActive(true);
            for (int i = 0; i < charName.Length; i++)
            {
                if (character == charName[i])
                {
                    p1Char[i].SetActive(true);
                }

            }
            float t = (float)(timingVarSpawn / .5f);
            P1.transform.localScale = new Vector3(3f, 3.5f, 1);
            P1.transform.position = Vector3.Lerp(startMaker, endMaker, t);
            timingVarSpawn += Time.deltaTime * speed;
            if(t >= 1.0f)
			{
				char1Count = 1;
			}
        }
        else if (player == "p2")
        {
            Debug.Log($"Here we set p2 as {character}");
            P2.SetActive(true);
            for (int i = 0; i < charName.Length; i++)
            {
                if (character == charName[i])
                {
                    p2Char[i].SetActive(true);
                }

            }
        }
        else if (player == "p3")
        {
            P3.SetActive(true);
            for (int i = 0; i < charName.Length; i++)
            {
                if (character == charName[i])
                {
                    p3Char[i].SetActive(true);
                }

            }
            float t = (float)(timingVarSpawn / .5f);
            P3.transform.localScale = new Vector3(3f, 3.5f, 1);
            P3.transform.position = Vector3.Lerp(startMaker_1, endMaker_1, t);
            timingVarSpawn += Time.deltaTime * speed;
            if(t >= 1.0f)
			{
				char3Count = 1;
			}
        }
        else if (player == "p4")
        {
            Debug.Log($"Here we set p4 as {character}");
            P4.SetActive(true);
            for (int i = 0; i < charName.Length; i++)
            {
                if (character == charName[i])
                {
                    p4Char[i].SetActive(true);
                }

            }
        }
    }
    // Update is called once per frame
    void Update()
    {
		if(globalGameState.onlineMode.Equals("LAN"))
		{
			if(globalGameState.lobby_P1Quit)
			{
				Debug.Log("TODO: remove the P1 on screen");
			}

			if(globalGameState.lobby_P2Quit)
			{
				Debug.Log("TODO: remove the P2 on screen");
			}
		}
		else if(globalGameState.onlineMode.Equals("GLOBAL"))
		{
			if (globalGameState.lobby_P1Quit ||
				globalGameState.lobby_P2Quit)
			{
				Debug.Log("Go back to menu");
				Util.toSceneWithIndex(globalGameState.scenesOrder["MENU"]);
			}
		}
        if (textMode == 1)
        {
            P1_icon.SetActive(true);
            P1_1_icon.SetActive(true);
            P2_1_icon.SetActive(true);
            P2_2_icon.SetActive(true);
        }
        if (textMode == 2)
        {
            P1_icon.SetActive(true);
            P2_icon.SetActive(true);
            P3_icon.SetActive(true);
            P4_icon.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (twoPlayerID == 1)
            {
                enterHitP1 = true;
                enterCount1++;
            }
            if (twoPlayerID == 2)
            {
                enterHitP2 = true;
                enterCount2++;
            }
            if (fourPlayerID == 1)
            {
                p1EnterHit = true;
            }
            if (fourPlayerID == 2)
            {
                p2EnterHit = true;
            }
            if (fourPlayerID == 3)
            {
                p3EnterHit = true;
            }
            if (fourPlayerID == 4)
            {
                p4EnterHit = true;
            }
        }


        /*
        if (bhit)
        {
            SpawnPlayer(player2, character2);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            bhit = true;
        }
        */


        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectVal = (selectVal + 1) % 9;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectVal = (selectVal - 1 + 9) % 9;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectVal = (selectVal - 3 + 9) % 9;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectVal = (selectVal + 3) % 9;
        }

        if (selectVal == 0)
        {
            pointer0.SetActive(true);
        }
        else pointer0.SetActive(false);

        if (selectVal == 1)
        {
            pointer1.SetActive(true);
        }
        else pointer1.SetActive(false);

        if (selectVal == 2)
        {
            pointer2.SetActive(true);
        }
        else pointer2.SetActive(false);

        if (selectVal == 3)
        {
            pointer3.SetActive(true);
        }
        else pointer3.SetActive(false);

        if (selectVal == 4)
        {
            pointer4.SetActive(true);
        }
        else pointer4.SetActive(false);

        if (selectVal == 5)
        {
            pointer5.SetActive(true);
        }
        else pointer5.SetActive(false);

        if (selectVal == 6)
        {
            pointer6.SetActive(true);
        }
        else pointer6.SetActive(false);

        if (selectVal == 7)
        {
            pointer7.SetActive(true);
        }
        else pointer7.SetActive(false);

        if (selectVal == 8)
        {
            pointer8.SetActive(true);
        }
        else pointer8.SetActive(false);

        if (Mode == 0)
        { currentID = 0; }

        if (Mode == 1)
        {
            fourPlayerID = 0;
            if (twoPlayerID == 1 && enterCount1 == 0)
            {
                P1.SetActive(true);
                for (int i = 0; i < p1Char.Length; i++)
                {
                    if (selectVal == i)
                        p1Char[i].SetActive(true);
                    else p1Char[i].SetActive(false);
                }
            }

			if (globalGameState.PlayerId == 1)
			{
				if (globalGameState.chosenCharacters[2] != null && char3Count == 0)
				{
                    Debug.Log($"p3 {globalGameState.chosenCharacters[2]} spawn on to the screen");
					SpawnPlayer("p3", globalGameState.chosenCharacters[2]);
					//char3Count = 1;
				}
				else if (globalGameState.chosenCharacters[3] != null && char4Count == 0)
				{
                    Debug.Log($"p4 {globalGameState.chosenCharacters[3]} spawn on to the screen");
					SpawnPlayer("p4", globalGameState.chosenCharacters[3]);
                    char4Count = 1;
				}
			}
			else if (globalGameState.PlayerId == 2)
			{
				if (globalGameState.chosenCharacters[0] != null && char1Count == 0)
				{
                    Debug.Log($"p1 {globalGameState.chosenCharacters[0]} spawn on to the screen");
					SpawnPlayer("p1", globalGameState.chosenCharacters[0]);
					//char1Count = 1;
				}
				else if (globalGameState.chosenCharacters[1] != null && char2Count == 0)
				{
                    Debug.Log($"p2 {globalGameState.chosenCharacters[1]} spawn on to the screen");
					SpawnPlayer("p2", globalGameState.chosenCharacters[1]);
					char2Count = 1;
				}
			}


            if (enterCount1 == 1)
            {
                if (!P1Log1)
                {
                    Debug.Log("P1_char1: " + charName[selectVal]);
                    globalGameState.chosenCharacters[0] = charName[selectVal];
                    globalGameState.charNameCount++;

                    string message = PreGameMessageGenerator.chooseCharacterMessage(1, charName[selectVal]);
                    ServerCommute.connection.sendToServer(message);
                    P1Log1 = true;
                }
                float t = (float)(timingVar / .5f);
                P1.transform.localScale = new Vector3(3f, 3.5f, 1);
                P1.transform.position = Vector3.Lerp(startMaker, endMaker, t);
                if (t >= 1)
                {
                    enterHitP1 = false;
                }
                timingVar += Time.deltaTime * speed;
                P2.SetActive(true);
                for (int i = 0; i < p2Char.Length; i++)
                {
                    if (selectVal == i)
                        p2Char[i].SetActive(true);
                    else p2Char[i].SetActive(false);
                }
            }
            else if (enterCount1 == 2)
            {
                if (!P1Log2)
                {
                    Debug.Log("P1_char2: " + charName[selectVal]);
                    globalGameState.chosenCharacters[1] = charName[selectVal];
                    globalGameState.charNameCount++;

                    string message = PreGameMessageGenerator.chooseCharacterMessage(2, charName[selectVal]);
                    ServerCommute.connection.sendToServer(message);
                    P1Log2 = true;
                }
            }

            if (twoPlayerID == 2 && enterCount2 == 0)
            {
                currentID = 2;
                P3.SetActive(true);
                for (int i = 0; i < p3Char.Length; i++)
                {
                    if (selectVal == i)
                        p3Char[i].SetActive(true);
                    else p3Char[i].SetActive(false);
                }
            }
            if (enterCount2 == 1)
            {
                if (!P2Log1)
                {
                    Debug.Log("P2_char1: " + charName[selectVal]);
                    globalGameState.chosenCharacters[2] = charName[selectVal];
                    globalGameState.charNameCount++;

                    string message = PreGameMessageGenerator.chooseCharacterMessage(3, charName[selectVal]);
                    ServerCommute.connection.sendToServer(message);
                    P2Log1 = true;
                }
                float t = (float)(timingVar_1 / .5f);
                P3.transform.localScale = new Vector3(3f, 3.5f, 1);
                P3.transform.position = Vector3.Lerp(startMaker_1, endMaker_1, t);
                if (t >= 1)
                {
                    enterHitP2 = false;
                }
                timingVar_1 += Time.deltaTime * speed;
                P4.SetActive(true);
                for (int i = 0; i < p4Char.Length; i++)
                {
                    if (selectVal == i)
                        p4Char[i].SetActive(true);
                    else p4Char[i].SetActive(false);
                }
            }
            else if (enterCount2 == 2)
            {
                if (!P2Log2)
                {
                    Debug.Log("P2_char2: " + charName[selectVal]);
                    globalGameState.chosenCharacters[3] = charName[selectVal];
                    globalGameState.charNameCount++;

                    string message = PreGameMessageGenerator.chooseCharacterMessage(4, charName[selectVal]);
                    ServerCommute.connection.sendToServer(message);
                    P2Log2 = true;
                }
            }
        }

        if (Mode == 2)
        {
            twoPlayerID = 0;
            if (fourPlayerID == 1)
            {
                P1.SetActive(true);
                for (int i = 0; i < p1Char.Length; i++)
                {
                    if (selectVal == i)
                        p1Char[i].SetActive(true);
                    else p1Char[i].SetActive(false);
                }
            }

            if (p1EnterHit)
            {
                float t = (float)(timingVarP1 / .5f);
                P1.transform.localScale = new Vector3(3f, 3.5f, 1);
                P1.transform.position = Vector3.Lerp(startMaker, endMaker, t);
                if (t >= 1)
                {
                    p1EnterHit = false;
                }
                timingVarP1 += Time.deltaTime * speed;
                if (!P1Log)
                {
                    Debug.Log("P1: " + charName[selectVal]);
                    globalGameState.chosenCharacters[0] = charName[selectVal];
                    globalGameState.charNameCount++;

                    string message = PreGameMessageGenerator.chooseCharacterMessage(1, charName[selectVal]);
                    ServerCommute.connection.sendToServer(message);
                    P1Log = true;
                }
            }
            if (fourPlayerID == 2)
            {
                P2.SetActive(true);
                for (int i = 0; i < p2Char.Length; i++)
                {
                    if (selectVal == i)
                        p2Char[i].SetActive(true);
                    else p2Char[i].SetActive(false);
                }
            }
            if (p2EnterHit)
            {
                if (!P2Log)
                {
                    Debug.Log("P2: " + charName[selectVal]);
                    globalGameState.chosenCharacters[1] = charName[selectVal];
                    globalGameState.charNameCount++;
                    string message = PreGameMessageGenerator.chooseCharacterMessage(2, charName[selectVal]);
                    ServerCommute.connection.sendToServer(message);
                    P2Log = true;
                }
            }

            if (fourPlayerID == 3)
            {
                currentID = 2;
                P3.SetActive(true);
                for (int i = 0; i < p3Char.Length; i++)
                {
                    if (selectVal == i)
                        p3Char[i].SetActive(true);
                    else p3Char[i].SetActive(false);
                }
            }
            if (p3EnterHit)
            {
                float t = (float)(timingVarP3 / .5f);
                P3.transform.localScale = new Vector3(3f, 3.5f, 1);
                P3.transform.position = Vector3.Lerp(startMaker_1, endMaker_1, t);
                if (t >= 1)
                {
                    p3EnterHit = false;
                }
                timingVarP3 += Time.deltaTime * speed;
                if (!P3Log)
                {
                    Debug.Log("P3: " + charName[selectVal]);
                    globalGameState.chosenCharacters[2] = charName[selectVal];
                    globalGameState.charNameCount++;
                    string message = PreGameMessageGenerator.chooseCharacterMessage(3, charName[selectVal]);
                    ServerCommute.connection.sendToServer(message);
                    P3Log = true;
                }
            }

            if (fourPlayerID == 4)
            {
                currentID = 2;
                P4.SetActive(true);
                for (int i = 0; i < p4Char.Length; i++)
                {
                    if (selectVal == i)
                        p4Char[i].SetActive(true);
                    else p4Char[i].SetActive(false);
                }
            }

            if (p4EnterHit)
            {
                if (!P4Log)
                {
                    Debug.Log("P4: " + charName[selectVal]);
                    globalGameState.chosenCharacters[3] = charName[selectVal];
                    globalGameState.charNameCount++;

                    string message = PreGameMessageGenerator.chooseCharacterMessage(4, charName[selectVal]);
                    ServerCommute.connection.sendToServer(message);
                    P4Log = true;
                }
            }

            // TODO: handle 2v2 character update RIGHT HERE, modify the below somehow
			if (globalGameState.PlayerId == 1)
			{
				if (globalGameState.chosenCharacters[1] != null && char2Count == 0)
				{
                    Debug.Log($"p2 {globalGameState.chosenCharacters[1]} spawn on to the screen");
					SpawnPlayer("p2", globalGameState.chosenCharacters[1]);
					char2Count = 1;
				}
				if (globalGameState.chosenCharacters[2] != null && char3Count == 0)
				{
                    Debug.Log($"p3 {globalGameState.chosenCharacters[2]} spawn on to the screen");
					SpawnPlayer("p3", globalGameState.chosenCharacters[2]);
					//char3Count = 1;
				}
				if (globalGameState.chosenCharacters[3] != null && char4Count == 0)
				{
                    Debug.Log($"p4 {globalGameState.chosenCharacters[3]} spawn on to the screen");
					SpawnPlayer("p4", globalGameState.chosenCharacters[3]);
                    char4Count = 1;
				}
			}
			else if (globalGameState.PlayerId == 2)
			{
				if (globalGameState.chosenCharacters[0] != null && char1Count == 0)
				{
                    Debug.Log($"p1 {globalGameState.chosenCharacters[0]} spawn on to the screen");
					SpawnPlayer("p1", globalGameState.chosenCharacters[0]);
				}
				if (globalGameState.chosenCharacters[2] != null && char3Count == 0)
				{
                    Debug.Log($"p3 {globalGameState.chosenCharacters[2]} spawn on to the screen");
					SpawnPlayer("p3", globalGameState.chosenCharacters[2]);
					//char3Count = 1;
				}
				if (globalGameState.chosenCharacters[3] != null && char4Count == 0)
				{
                    Debug.Log($"p4 {globalGameState.chosenCharacters[3]} spawn on to the screen");
					SpawnPlayer("p4", globalGameState.chosenCharacters[3]);
                    char4Count = 1;
				}
			}
			else if (globalGameState.PlayerId == 3)
			{
				if (globalGameState.chosenCharacters[1] != null && char2Count == 0)
				{
                    Debug.Log($"p2 {globalGameState.chosenCharacters[1]} spawn on to the screen");
					SpawnPlayer("p2", globalGameState.chosenCharacters[1]);
					char2Count = 1;
				}
				if (globalGameState.chosenCharacters[0] != null && char1Count == 0)
				{
                    Debug.Log($"p1 {globalGameState.chosenCharacters[0]} spawn on to the screen");
					SpawnPlayer("p1", globalGameState.chosenCharacters[0]);
				}
				if (globalGameState.chosenCharacters[3] != null && char4Count == 0)
				{
                    Debug.Log($"p4 {globalGameState.chosenCharacters[3]} spawn on to the screen");
					SpawnPlayer("p4", globalGameState.chosenCharacters[3]);
                    char4Count = 1;
				}
			}
			else if (globalGameState.PlayerId == 4)
			{
				if (globalGameState.chosenCharacters[1] != null && char2Count == 0)
				{
                    Debug.Log($"p2 {globalGameState.chosenCharacters[1]} spawn on to the screen");
					SpawnPlayer("p2", globalGameState.chosenCharacters[1]);
					char2Count = 1;
				}
				if (globalGameState.chosenCharacters[0] != null && char1Count == 0)
				{
                    Debug.Log($"p1 {globalGameState.chosenCharacters[0]} spawn on to the screen");
					SpawnPlayer("p1", globalGameState.chosenCharacters[0]);
				}
				if (globalGameState.chosenCharacters[2] != null && char3Count == 0)
				{
                    Debug.Log($"p3 {globalGameState.chosenCharacters[2]} spawn on to the screen");
					SpawnPlayer("p3", globalGameState.chosenCharacters[2]);
					//char3Count = 1;
				}
			}
        }

        if(globalGameState.charNameCount >= 4)
		{
			Debug.Log("all the chosen characters");
            foreach(string name in globalGameState.chosenCharacters)
			{
                Debug.Log(name);
			}
            Util.toNextScene();
		}
    }
}

