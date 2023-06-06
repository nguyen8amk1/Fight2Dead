using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MainCharacter : MonoBehaviour 
{
    public GameObject char0, char1, char2, char3, char0_2, char1_2, char2_2, char3_2;
    public static int selectVal = 0;

    private Thread listenToServerThread;
    private GameState globalGameState = GameState.Instance;
    public static int currentPlayer = 1;
    public GameObject characterSelect;
    public GameObject playerPosition;

    private Vector3 startMaker = new Vector3(3.7f, -1.8f, 0);
    private Vector3 endMaker = new Vector3(6.7f, -1.8f, 0);
    private float timingVar = 0;
    private bool enterHit = false;

    // Start is called before the first frame update

    // TODO: when the player close the game send a close connection message to the server 

    void Start()
    {
        // TODO: create a listening thread 
		listenToServerThread = new Thread(new ThreadStart(listenToServer));
		listenToServerThread.Start();
    }

	private void OnApplicationQuit()
	{
	}

	private void listenToServer()
	{
        while (true)
        {
            // received format: "pid:{},s:q"
            // string[] tokens = message.Split(',');
            //int pid = Int32.Parse(Util.getValueFrom(tokens[1]));

            // TODO: DO SOMETHING ELSE HERE 
            // Debug.Log($"Player with id {pid} has close the connection with the game");
        }
	}


	// Update is called once per frame
    /*
	void Update()
        Application.targetFrameRate = 60;    
    }

    public void Char0()
    {
        MainCharacter.selectVal = 0;
    }
    public void Char1()
    {
        MainCharacter.selectVal = 1;
    }
    public void Char2()
    {
        MainCharacter.selectVal = 2;
    }
    public void Char3()
    {
        MainCharacter.selectVal = 3;
    }
    */

    // Update is called once per frame
    void Update()
    {
        Application.targetFrameRate = 60;    
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enterHit = true;

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectVal = (selectVal + 1) % 4; 
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectVal = (selectVal - 1 + 4) % 4; 
        }else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectVal = (selectVal + 2) % 4;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectVal = (selectVal -2 + 4) % 4;
        }

        switch (currentPlayer)
        {
            case 1:
                switch (selectVal)
                {
                    case 0:
                        char0.SetActive(true);
                        char1.SetActive(false);
                        char2.SetActive(false);
                        char3.SetActive(false);
                        break;
                    case 1:
                        char0.SetActive(false);
                        char1.SetActive(true);
                        char2.SetActive(false);
                        char3.SetActive(false);
                        break;
                    case 2:
                        char0.SetActive(false);
                        char1.SetActive(false);
                        char2.SetActive(true);
                        char3.SetActive(false);
                        break;
                    case 3:
                        char0.SetActive(false);
                        char1.SetActive(false);
                        char2.SetActive(false);
                        char3.SetActive(true);
                        break;
                    default:
                        break;
                }
                break;
            case 2:
                switch (selectVal)
                {
                    case 0:
                        char0_2.SetActive(true);
                        char1_2.SetActive(false);
                        char2_2.SetActive(false);
                        char3_2.SetActive(false);
                        break;
                    case 1:
                        char0_2.SetActive(false);
                        char1_2.SetActive(true);
                        char2_2.SetActive(false);
                        char3_2.SetActive(false);
                        break;
                    case 2:
                        char0_2.SetActive(false);
                        char1_2.SetActive(false);
                        char2_2.SetActive(true);
                        char3_2.SetActive(false);
                        break;
                    case 3:
                        char0_2.SetActive(false);
                        char1_2.SetActive(false);
                        char2_2.SetActive(false);
                        char3_2.SetActive(true);
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }

        if(enterHit)
		{
            float speed = 3.0f;
            float t = (float)(timingVar / .5f);
            // Debug.Log("timingvar: " + timingVar + "moveDuration: " + moveDuration);

            currentPlayer = 2;
            selectVal = 0;
            characterSelect.SetActive(true);
            playerPosition.transform.localScale = new Vector3(2.4f, 3.1f, 1);
            playerPosition.transform.position = Vector3.Lerp(startMaker, endMaker, t);

            Debug.Log("Enter Pressed");
            if(t >= 1)
			{
                enterHit = false;
			}

			timingVar += Time.deltaTime*speed;
		}

    }
}